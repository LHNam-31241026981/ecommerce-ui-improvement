using Filmora.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Filmora.ViewModels;

public record CatalogViewModel(
    IReadOnlyList<Movie> Movies,
    SelectList Genres,
    string? SearchString,
    int? GenreId);
