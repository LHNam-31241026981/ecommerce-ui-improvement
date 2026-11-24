using System.Buffers.Binary;

namespace Filmora.Services;

public sealed class PosterStorage(IWebHostEnvironment environment, ILogger<PosterStorage> logger)
{
    private const string UploadFolder = "uploads/posters";
    private const string UploadPrefix = "~/" + UploadFolder + "/";
    private const string Fallback = "~/images/posters/default.webp";
    private const long MaxBytes = 4 * 1024 * 1024;

    public string DisplayPath(string? path)
    {
        if (path is not null && path.StartsWith("~/", StringComparison.Ordinal)
            && environment.WebRootFileProvider.GetFileInfo(path[2..]).Exists)
            return path;

        return Fallback;
    }

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length < 12 || file.Length > MaxBytes)
            throw new InvalidDataException("Ảnh phải có dung lượng tối đa 4 MB và chứa dữ liệu hợp lệ.");

        var data = new byte[(int)file.Length];
        await using var input = file.OpenReadStream();
        await input.ReadExactlyAsync(data, cancellationToken);
        var extension = DetectExtension(data);
        if (extension is null)
            throw new InvalidDataException("Vui lòng chọn ảnh JPG, PNG hoặc WebP hợp lệ và đầy đủ.");

        var directory = Path.Combine(environment.WebRootPath, UploadFolder);
        Directory.CreateDirectory(directory);
        var name = $"{Guid.NewGuid():N}{extension}";
        var path = UploadPrefix + name;
        try
        {
            await using var output = new FileStream(Path.Combine(directory, name), FileMode.CreateNew, FileAccess.Write);
            await output.WriteAsync(data, cancellationToken);
            return path;
        }
        catch
        {
            DeleteUpload(path);
            throw;
        }
    }

    public void DeleteUpload(string? path)
    {
        if (path is null || !path.StartsWith(UploadPrefix, StringComparison.Ordinal))
            return;

        var name = path[UploadPrefix.Length..];
        if (!Guid.TryParseExact(Path.GetFileNameWithoutExtension(name), "N", out _)
            || Path.GetFileName(name) != name || Path.GetExtension(name) is not (".jpg" or ".png" or ".webp"))
            return;

        try
        {
            File.Delete(Path.Combine(environment.WebRootPath, UploadFolder, name));
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            logger.LogWarning(exception, "Không thể dọn ảnh đã tải lên {Poster}", name);
        }
    }

    private static string? DetectExtension(ReadOnlySpan<byte> data)
    {
        if (data.StartsWith(new byte[] { 0xff, 0xd8 }) && HasCompleteJpeg(data))
            return ".jpg";
        if (data.StartsWith(new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a }) && HasCompletePng(data[8..]))
            return ".png";
        if (data[..4].SequenceEqual("RIFF"u8) && data.Slice(8, 4).SequenceEqual("WEBP"u8) && HasCompleteWebp(data))
            return ".webp";

        return null;
    }

    private static bool HasCompleteJpeg(ReadOnlySpan<byte> data)
    {
        if (data[^2] != 0xff || data[^1] != 0xd9)
            return false;

        var position = 2;
        var hasFrame = false;
        while (position < data.Length - 5 && data[position++] == 0xff)
        {
            while (position < data.Length - 5 && data[position] == 0xff)
                position++;
            var marker = data[position++];
            var size = BinaryPrimitives.ReadUInt16BigEndian(data[position..]);
            if (size < 2 || size > data.Length - position - 2)
                return false;

            if (marker is 0xc0 or 0xc1 or 0xc2)
            {
                if (size < 11
                    || BinaryPrimitives.ReadUInt16BigEndian(data[(position + 3)..]) == 0
                    || BinaryPrimitives.ReadUInt16BigEndian(data[(position + 5)..]) == 0)
                    return false;
                hasFrame = true;
            }

            if (marker == 0xda)
                return hasFrame && size >= 8 && position + size < data.Length - 2;

            position += size;
        }

        return false;
    }

    private static bool HasCompletePng(ReadOnlySpan<byte> chunks)
    {
        var hasHeader = false;
        var hasPixels = false;
        while (chunks.Length >= 12)
        {
            var size = BinaryPrimitives.ReadUInt32BigEndian(chunks);
            if (size > chunks.Length - 12)
                return false;

            var length = (int)size;
            var type = chunks.Slice(4, 4);
            if (!HasValidPngChecksum(chunks.Slice(4, length + 4),
                BinaryPrimitives.ReadUInt32BigEndian(chunks[(length + 8)..])))
                return false;

            if (!hasHeader)
            {
                if (!type.SequenceEqual("IHDR"u8) || length != 13
                    || BinaryPrimitives.ReadUInt32BigEndian(chunks[8..]) == 0
                    || BinaryPrimitives.ReadUInt32BigEndian(chunks[12..]) == 0)
                    return false;
                hasHeader = true;
            }
            else if (type.SequenceEqual("IDAT"u8) && length > 0)
            {
                hasPixels = true;
            }
            else if (type.SequenceEqual("IEND"u8))
            {
                return hasPixels && length == 0 && chunks.Length == 12;
            }

            chunks = chunks[(length + 12)..];
        }

        return false;
    }

    private static bool HasValidPngChecksum(ReadOnlySpan<byte> data, uint expected)
    {
        uint checksum = uint.MaxValue;
        foreach (var value in data)
        {
            checksum ^= value;
            for (var bit = 0; bit < 8; bit++)
                checksum = (checksum >> 1) ^ ((checksum & 1) != 0 ? 0xedb88320u : 0u);
        }

        return ~checksum == expected;
    }

    private static bool HasCompleteWebp(ReadOnlySpan<byte> data)
    {
        if (BinaryPrimitives.ReadUInt32LittleEndian(data[4..]) != data.Length - 8)
            return false;

        return HasWebpChunks(data[12..], allowAnimation: true);
    }

    private static bool HasWebpChunks(ReadOnlySpan<byte> chunks, bool allowAnimation)
    {
        var hasPixels = false;
        while (chunks.Length >= 8)
        {
            var size = BinaryPrimitives.ReadUInt32LittleEndian(chunks[4..]);
            if (size > chunks.Length - 8)
                return false;

            var length = (int)size;
            var paddedLength = length + (length & 1);
            if (paddedLength > chunks.Length - 8)
                return false;

            var type = chunks[..4];
            var payload = chunks.Slice(8, length);
            if (type.SequenceEqual("VP8 "u8) && length >= 10 && payload.Slice(3, 3).SequenceEqual(new byte[] { 0x9d, 0x01, 0x2a }))
                hasPixels = true;
            if (type.SequenceEqual("VP8L"u8) && length >= 5 && payload[0] == 0x2f)
                hasPixels = true;
            if (allowAnimation && type.SequenceEqual("ANMF"u8) && length >= 16)
                hasPixels |= HasWebpChunks(payload[16..], allowAnimation: false);

            chunks = chunks[(8 + paddedLength)..];
        }

        return hasPixels && chunks.IsEmpty;
    }
}
