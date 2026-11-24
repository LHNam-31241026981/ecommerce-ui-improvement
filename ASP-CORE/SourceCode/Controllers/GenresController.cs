using Filmora.Data;
using Filmora.Models;
using Filmora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Controllers;

[Authorize(Roles = "Admin")]
public class GenresController(ApplicationDbContext db, ILogger<GenresController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
        View(await db.Genres.AsNoTracking().OrderBy(genre => genre.Name).ToListAsync(cancellationToken));

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var genre = await db.Genres.FindAsync([id], cancellationToken);
        if (genre is null)
            return NotFound();

        return View((genre, await MovieCountAsync(id, cancellationToken)));
    }

    [HttpGet]
    public IActionResult Create() => View(new GenreInput());

    [HttpPost, ValidateAntiForgeryToken]
    public Task<IActionResult> Create(GenreInput input, CancellationToken cancellationToken) =>
        SaveAsync(new Genre(), input, nameof(Create), cancellationToken);

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var genre = await db.Genres.FindAsync([id], cancellationToken);
        return genre is null ? NotFound() : View(new GenreInput { Id = id, Name = genre.Name });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GenreInput input, CancellationToken cancellationToken)
    {
        var genre = await db.Genres.FindAsync([id], cancellationToken);
        return genre is null ? NotFound() : await SaveAsync(genre, input, nameof(Edit), cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var genre = await db.Genres.FindAsync([id], cancellationToken);
        if (genre is null)
            return NotFound();

        return View((genre, await MovieCountAsync(id, cancellationToken)));
    }

    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var genre = await db.Genres.FindAsync([id], cancellationToken);
        if (genre is null)
            return NotFound();

        var movieCount = await MovieCountAsync(id, cancellationToken);
        if (movieCount > 0)
        {
            ModelState.AddModelError("", "Hãy chuyển các phim sang thể loại khác trước khi xóa.");
            return View(nameof(Delete), (genre, movieCount));
        }

        db.Genres.Remove(genre);
        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (DbUpdateException exception)
        {
            logger.LogWarning(exception, "Không thể xóa thể loại {GenreId}", id);
            ModelState.AddModelError("", "Không thể xóa thể loại. Hãy kiểm tra các phim đang dùng thể loại này.");
            return View(nameof(Delete), (genre, await MovieCountAsync(id, cancellationToken)));
        }

        TempData["Success"] = "Đã xóa thể loại.";
        return RedirectToAction(nameof(Index));
    }

    private Task<int> MovieCountAsync(int genreId, CancellationToken cancellationToken) =>
        db.Movies.CountAsync(movie => movie.GenreId == genreId, cancellationToken);

    private async Task<IActionResult> SaveAsync(Genre genre, GenreInput input, string viewName, CancellationToken cancellationToken)
    {
        input.Id = genre.Id;
        if (!ModelState.IsValid)
            return View(viewName, input);

        var name = input.Name!.Trim();
        var otherNames = await db.Genres.AsNoTracking().Where(item => item.Id != genre.Id)
            .Select(item => item.Name).ToListAsync(cancellationToken);
        if (otherNames.Contains(name, StringComparer.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(nameof(input.Name), "Thể loại này đã tồn tại.");
            return View(viewName, input);
        }

        var isNew = genre.Id == 0;
        genre.Name = name;
        if (isNew)
            db.Genres.Add(genre);

        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (DbUpdateException exception)
        {
            logger.LogWarning(exception, "Không thể lưu thể loại {GenreId}", genre.Id);
            ModelState.AddModelError("", "Không thể lưu thể loại. Hãy kiểm tra tên rồi thử lại.");
            return View(viewName, input);
        }

        TempData["Success"] = isNew ? "Đã thêm thể loại." : "Đã cập nhật thể loại.";
        return RedirectToAction(nameof(Index));
    }
}
