using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Filmora.ViewModels;

public class MovieInput : IValidatableObject
{
    [Display(Name = "Tên phim")]
    [Required(ErrorMessage = "Vui lòng nhập tên phim.")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Tên phim phải từ 3 đến 200 ký tự.")]
    public string? Title { get; set; }

    [Display(Name = "Tóm tắt")]
    [Required(ErrorMessage = "Vui lòng nhập tóm tắt.")]
    [StringLength(1500, MinimumLength = 11, ErrorMessage = "Tóm tắt phải từ 11 đến 1.500 ký tự.")]
    public string? Summary { get; set; }

    [Display(Name = "Ngày phát hành")]
    [Required(ErrorMessage = "Vui lòng chọn ngày phát hành.")]
    [DataType(DataType.Date)]
    public DateOnly? ReleaseDate { get; set; }

    [Display(Name = "Thể loại")]
    [Required(ErrorMessage = "Vui lòng chọn thể loại.")]
    public int? GenreId { get; set; }

    [Display(Name = "Giá vé (đồng)")]
    [Required(ErrorMessage = "Vui lòng nhập giá vé.")]
    [Range(typeof(decimal), "5001", "999999999", ErrorMessage = "Giá vé phải từ 5.001 đến 999.999.999 đồng.")]
    public decimal? Price { get; set; }

    [Display(Name = "Điểm đánh giá")]
    [Required(ErrorMessage = "Vui lòng chọn điểm đánh giá.")]
    [Range(1, 5, ErrorMessage = "Điểm đánh giá phải từ 1 đến 5.")]
    public int? Rated { get; set; }

    [Display(Name = "Ảnh poster")]
    public IFormFile? Poster { get; set; }

    [BindNever]
    public SelectList Genres { get; set; } = new(Array.Empty<object>());

    [BindNever]
    public string? CurrentPoster { get; set; }

    [BindNever]
    public int Id { get; set; }

    [BindNever]
    public DateOnly? ExistingReleaseDate { get; set; }

    public static DateOnly TodayInVietnam => DateOnly.FromDateTime(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Price is decimal price && decimal.Truncate(price) != price)
            yield return new ValidationResult("Giá vé phải là số nguyên đồng.", [nameof(Price)]);

        if (Title?.Trim().Length < 3)
            yield return new ValidationResult("Tên phim cần ít nhất 3 ký tự có nội dung.", [nameof(Title)]);

        if (Summary?.Trim().Length < 11)
            yield return new ValidationResult("Tóm tắt cần ít nhất 11 ký tự có nội dung.", [nameof(Summary)]);
    }
}
