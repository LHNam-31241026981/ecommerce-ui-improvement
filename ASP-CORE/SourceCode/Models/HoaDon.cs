using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Models;

[Index(nameof(Reference), IsUnique = true)]
public class HoaDon
{
    [Key]
    public int MaHoaDon { get; set; }

    public Guid Reference { get; set; }

    [Precision(18, 2)]
    public decimal TongTien { get; set; }

    public DateTime NgayLap { get; set; }

    [MaxLength(450)]
    public string? CustomerId { get; set; }

    public List<CTHoaDon> ChiTietHoaDons { get; set; } = [];

    public ShippingDetail ShippingDetail { get; set; } = null!;
}
