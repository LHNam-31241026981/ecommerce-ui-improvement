using System.Text.Json;
using Filmora.Data;
using Filmora.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Services;

public class CartService(IHttpContextAccessor contextAccessor, ApplicationDbContext context)
{
    private const string CartKey = "Cart.Items";
    private const string CheckoutKey = "Cart.Checkout";
    private const string CompletedKey = "Cart.Completed";

    public const int MaxQuantity = 99;

    private ISession Session => contextAccessor.HttpContext!.Session;

    public async Task<int> CountAsync()
    {
        var items = await ReadItemsAsync();
        return items.Values.Sum();
    }

    public async Task<CartViewModel> GetAsync(CancellationToken cancellationToken = default)
    {
        var items = await ReadItemsAsync(cancellationToken);
        if (items.Count == 0)
        {
            return new CartViewModel([], []);
        }

        var ids = items.Keys.ToArray();
        var movies = await context.Movies.AsNoTracking()
            .Where(movie => ids.Contains(movie.Id))
            .Select(movie => new { movie.Id, movie.Title, movie.Price })
            .ToDictionaryAsync(movie => movie.Id, cancellationToken);

        var lines = items
            .Where(item => movies.ContainsKey(item.Key))
            .Select(item => new CartLineViewModel(item.Key, movies[item.Key].Title, movies[item.Key].Price, item.Value))
            .ToList();

        var unavailableIds = ids.Where(id => !movies.ContainsKey(id)).ToList();
        return new CartViewModel(lines, unavailableIds);
    }

    public async Task<bool> AddAsync(int movieId, int quantity, CancellationToken cancellationToken)
    {
        if (quantity is < 1 or > MaxQuantity ||
            !await context.Movies.AnyAsync(movie => movie.Id == movieId, cancellationToken))
        {
            return false;
        }

        var items = await ReadItemsAsync(cancellationToken);
        var combined = items.GetValueOrDefault(movieId) + quantity;
        if (combined > MaxQuantity)
        {
            return false;
        }

        items[movieId] = combined;
        await SaveItemsAsync(items, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(int movieId, int quantity, CancellationToken cancellationToken)
    {
        var items = await ReadItemsAsync(cancellationToken);
        if (quantity is < 1 or > MaxQuantity || !items.TryGetValue(movieId, out var previous))
        {
            return false;
        }

        if (previous != quantity)
        {
            items[movieId] = quantity;
            await SaveItemsAsync(items, cancellationToken);
        }

        return true;
    }

    public async Task RemoveAsync(int movieId, CancellationToken cancellationToken)
    {
        var items = await ReadItemsAsync(cancellationToken);
        if (items.Remove(movieId))
        {
            await SaveItemsAsync(items, cancellationToken);
        }
    }

    public async Task<Guid> GetOrCreateReferenceAsync(CancellationToken cancellationToken)
    {
        await Session.LoadAsync(cancellationToken);
        if (Guid.TryParse(Session.GetString(CheckoutKey), out var reference))
        {
            return reference;
        }

        reference = Guid.NewGuid();
        Session.SetString(CheckoutKey, reference.ToString("D"));
        await Session.CommitAsync(cancellationToken);
        return reference;
    }

    public bool MatchesPendingReference(Guid reference) =>
        reference != Guid.Empty &&
        Guid.TryParse(Session.GetString(CheckoutKey), out var pendingReference) &&
        pendingReference == reference;

    public bool HasCompletedReference(Guid reference) =>
        ReadCompletedReferences().Contains(reference);

    public async Task CompleteAsync(Guid reference)
    {
        if (MatchesPendingReference(reference))
        {
            Session.Remove(CartKey);
            Session.Remove(CheckoutKey);
        }

        var completed = ReadCompletedReferences();
        if (!completed.Contains(reference))
        {
            completed.Add(reference);
            Session.SetString(CompletedKey, JsonSerializer.Serialize(completed.TakeLast(12)));
        }

        await Session.CommitAsync();
    }

    private async Task<Dictionary<int, int>> ReadItemsAsync(CancellationToken cancellationToken = default)
    {
        await Session.LoadAsync(cancellationToken);
        var json = Session.GetString(CartKey);
        return json is null ? [] : JsonSerializer.Deserialize<Dictionary<int, int>>(json) ?? [];
    }

    private List<Guid> ReadCompletedReferences()
    {
        var json = Session.GetString(CompletedKey);
        return json is null ? [] : JsonSerializer.Deserialize<List<Guid>>(json) ?? [];
    }

    private async Task SaveItemsAsync(Dictionary<int, int> items, CancellationToken cancellationToken)
    {
        Session.SetString(CartKey, JsonSerializer.Serialize(items));
        Session.Remove(CheckoutKey);
        await Session.CommitAsync(cancellationToken);
    }
}
