using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Models;

public class CTHoaDon
{
    [Key]
    public int MaChiTietHoaDon { get; set; }

    [ForeignKey(nameof(HoaDonObj))]
    public int MaHoaDon { get; set; }

    public HoaDon HoaDonObj { get; set; } = null!;

    [ForeignKey(nameof(MovieObj))]
    public int? MaMovie { get; set; }

    public Movie? MovieObj { get; set; }

    [MaxLength(200)]
    public string TenPhim { get; set; } = string.Empty;

    public int SoLuong { get; set; }

    [Precision(18, 2)]
    public decimal DonGia { get; set; }
}
