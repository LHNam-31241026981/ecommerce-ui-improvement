using System.ComponentModel.DataAnnotations;

namespace Filmora.Models;

public class Genre
{
    public int Id { get; set; }
    [MaxLength(80)]
    public string Name { get; set; } = "";
}
