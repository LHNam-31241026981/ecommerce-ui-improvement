using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using Filmora.Models;

namespace Filmora.Services;

public class OrderEmailService(IConfiguration configuration, ILogger<OrderEmailService> logger)
{
    public async Task SendConfirmationAsync(HoaDon order)
    {
        var address = configuration["Email:Address"];
        var password = configuration["Email:AppPassword"];
        if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        try
        {
            var vietnamese = CultureInfo.GetCultureInfo("vi-VN");
            var shipping = order.ShippingDetail;
            var body = new StringBuilder()
                .AppendLine($"Rạp Mở đã nhận đơn hàng #{order.MaHoaDon:D6}.")
                .AppendLine()
                .AppendLine("Phim trong đơn");
            foreach (var line in order.ChiTietHoaDons)
            {
                var subtotal = line.DonGia * line.SoLuong;
                body.AppendLine($"- {line.TenPhim} × {line.SoLuong}   {subtotal.ToString("#,0.##", vietnamese)} ₫");
            }

            body.AppendLine()
                .AppendLine($"Tổng cộng: {order.TongTien.ToString("#,0.##", vietnamese)} ₫")
                .AppendLine($"Người nhận: {shipping.Name}")
                .AppendLine($"Địa chỉ: {shipping.Address}")
                .AppendLine($"Điện thoại: {shipping.Mobile}");

            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(address.Trim(), password.Replace(" ", string.Empty))
            };
            using var message = new MailMessage(address.Trim(), shipping.Email)
            {
                Subject = $"Rạp Mở – Xác nhận đơn #{order.MaHoaDon:D6}",
                Body = body.ToString(),
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(8));
            await client.SendMailAsync(message, timeout.Token);
        }
        catch (Exception exception) when (exception is SmtpException or FormatException or ArgumentException or OperationCanceledException)
        {
            logger.LogWarning("Email confirmation for order {OrderId} failed ({FailureType}).",
                order.MaHoaDon, exception.GetType().Name);
        }
    }
}
