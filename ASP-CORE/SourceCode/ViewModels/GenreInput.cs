using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Filmora.ViewModels;

public class GenreInput
{
    [BindNever]
    public int Id { get; set; }

    [Display(Name = "Tên thể loại")]
    [Required(ErrorMessage = "Vui lòng nhập tên thể loại.")]
    [StringLength(80, ErrorMessage = "Tên thể loại tối đa 80 ký tự.")]
    public string? Name { get; set; }
}
