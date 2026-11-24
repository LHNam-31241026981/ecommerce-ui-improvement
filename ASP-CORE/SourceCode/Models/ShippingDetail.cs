using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmora.Models;

public class ShippingDetail
{
    public int Id { get; set; }

    [ForeignKey(nameof(HoaDonObj))]
    public int MaHoaDon { get; set; }

    public HoaDon HoaDonObj { get; set; } = null!;

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(254)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(25)]
    public string Mobile { get; set; } = string.Empty;
}
