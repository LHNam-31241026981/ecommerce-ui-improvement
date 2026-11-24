namespace Filmora.ViewModels;

public record CartLineViewModel(int MovieId, string Title, decimal Price, int Quantity)
{
    public decimal Subtotal => Price * Quantity;
}

public record CartViewModel(IReadOnlyList<CartLineViewModel> Lines, IReadOnlyList<int> UnavailableMovieIds)
{
    public bool CanCheckout => Lines.Count > 0 && UnavailableMovieIds.Count == 0;
    public int Count => Lines.Sum(line => line.Quantity);
    public decimal Total => Lines.Sum(line => line.Subtotal);
}
