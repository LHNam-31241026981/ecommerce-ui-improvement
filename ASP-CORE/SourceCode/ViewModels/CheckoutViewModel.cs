using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Filmora.ViewModels;

public class CheckoutViewModel
{
    public Guid Reference { get; set; }
    public string? PreviewToken { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Nhập họ và tên người nhận.")]
    [StringLength(100, ErrorMessage = "Họ và tên tối đa 100 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ giao hàng")]
    [Required(ErrorMessage = "Nhập địa chỉ giao hàng.")]
    [StringLength(300, ErrorMessage = "Địa chỉ tối đa 300 ký tự.")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Nhập email nhận xác nhận đơn.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(254, ErrorMessage = "Email tối đa 254 ký tự.")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Nhập số điện thoại người nhận.")]
    [RegularExpression(@"\+?(?=(?:[^0-9]*[0-9]){9,15}$)[0-9][0-9 .()\-]*[0-9]", ErrorMessage = "Nhập số điện thoại hợp lệ, ví dụ 0901234567.")]
    [StringLength(25, ErrorMessage = "Số điện thoại tối đa 25 ký tự.")]
    public string Mobile { get; set; } = string.Empty;

    [BindNever]
    public CartViewModel Cart { get; set; } = new([], []);
}

public record CheckoutCompletedViewModel(int OrderNumber, decimal Total);
