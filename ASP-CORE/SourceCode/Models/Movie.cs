using System.ComponentModel.DataAnnotations;

namespace Filmora.Models;

public class Movie
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = "";
    [MaxLength(1500)]
    public string Summary { get; set; } = "";
    public DateOnly ReleaseDate { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
    public decimal Price { get; set; }
    public int Rated { get; set; }
    [MaxLength(255)]
    public string? PicturePath { get; set; }
}
