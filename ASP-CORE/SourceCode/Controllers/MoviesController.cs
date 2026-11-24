using Filmora.Data;
using Filmora.Models;
using Filmora.Services;
using Filmora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Controllers;

public class MoviesController(ApplicationDbContext db, PosterStorage posters, ILogger<MoviesController> logger) : Controller
{
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> Index(string? searchString, int? genreId, CancellationToken cancellationToken)
    {
        searchString = searchString?.Trim();
        var movies = db.Movies.AsNoTracking().Include(movie => movie.Genre).AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
            movies = movies.Where(movie => movie.Title.ToLower().Contains(searchString.ToLower()));
        if (genreId.HasValue)
            movies = movies.Where(movie => movie.GenreId == genreId.Value);

        return View(new CatalogViewModel(
            await movies.OrderBy(movie => movie.ReleaseDate).ThenBy(movie => movie.Title).ToListAsync(cancellationToken),
            await GenreOptionsAsync(genreId, cancellationToken),
            searchString,
            genreId));
    }

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var movie = await FindWithGenreAsync(id, cancellationToken);
        return movie is null ? NotFound() : View(movie);
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Manage(CancellationToken cancellationToken) =>
        View(await db.Movies.AsNoTracking().Include(movie => movie.Genre)
            .OrderBy(movie => movie.Title).ToListAsync(cancellationToken));

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken) =>
        View(new MovieInput { Genres = await GenreOptionsAsync(null, cancellationToken) });

    [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public Task<IActionResult> Create(MovieInput input, CancellationToken cancellationToken) =>
        SaveAsync(new Movie(), input, nameof(Create), cancellationToken);

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var movie = await FindWithGenreAsync(id, cancellationToken);
        if (movie is null)
            return NotFound();

        return View(new MovieInput
        {
            Id = movie.Id,
            Title = movie.Title,
            Summary = movie.Summary,
            ReleaseDate = movie.ReleaseDate,
            ExistingReleaseDate = movie.ReleaseDate,
            GenreId = movie.GenreId,
            Price = movie.Price,
            Rated = movie.Rated,
            CurrentPoster = movie.PicturePath,
            Genres = await GenreOptionsAsync(movie.GenreId, cancellationToken)
        });
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, MovieInput input, CancellationToken cancellationToken)
    {
        var movie = await db.Movies.FindAsync([id], cancellationToken);
        return movie is null ? NotFound() : await SaveAsync(movie, input, nameof(Edit), cancellationToken);
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var movie = await FindWithGenreAsync(id, cancellationToken);
        return movie is null ? NotFound() : View(movie);
    }

    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var movie = await db.Movies.Include(item => item.Genre)
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (movie is null)
            return NotFound();

        db.Movies.Remove(movie);
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
            logger.LogWarning(exception, "Không thể xóa phim {MovieId}", id);
            ModelState.AddModelError("", "Không thể xóa phim vì có dữ liệu liên quan hoặc lỗi lưu trữ.");
            return View(nameof(Delete), movie);
        }

        posters.DeleteUpload(movie.PicturePath);
        TempData["Success"] = "Đã xóa phim.";
        return RedirectToAction(nameof(Manage));
    }

    private Task<Movie?> FindWithGenreAsync(int id, CancellationToken cancellationToken) =>
        db.Movies.AsNoTracking().Include(movie => movie.Genre)
            .SingleOrDefaultAsync(movie => movie.Id == id, cancellationToken);

    private async Task<SelectList> GenreOptionsAsync(int? selectedId, CancellationToken cancellationToken) =>
        new(await db.Genres.AsNoTracking().OrderBy(genre => genre.Name).ToListAsync(cancellationToken),
            nameof(Genre.Id), nameof(Genre.Name), selectedId);

    private async Task<IActionResult> SaveAsync(Movie movie, MovieInput input, string viewName, CancellationToken cancellationToken)
    {
        input.Id = movie.Id;
        input.ExistingReleaseDate = movie.Id == 0 ? null : movie.ReleaseDate;
        if (input.ReleaseDate is DateOnly releasedOn && releasedOn <= MovieInput.TodayInVietnam
            && releasedOn != input.ExistingReleaseDate)
            ModelState.AddModelError(nameof(input.ReleaseDate), "Ngày phát hành mới phải sau hôm nay.");

        input.CurrentPoster = movie.PicturePath;
        if (input.GenreId is int genreId && !await db.Genres.AnyAsync(genre => genre.Id == genreId, cancellationToken))
            ModelState.AddModelError(nameof(input.GenreId), "Thể loại đã chọn không tồn tại.");

        string? newPoster = null;
        if (ModelState.IsValid && input.Poster is not null)
        {
            try
            {
                newPoster = await posters.SaveAsync(input.Poster, cancellationToken);
            }
            catch (InvalidDataException exception)
            {
                ModelState.AddModelError(nameof(input.Poster), exception.Message);
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                logger.LogWarning(exception, "Không thể lưu ảnh poster");
                ModelState.AddModelError(nameof(input.Poster), "Không thể lưu ảnh poster. Vui lòng thử lại.");
            }
        }

        if (ModelState.IsValid)
        {
            var isNew = movie.Id == 0;
            var previousPoster = movie.PicturePath;
            movie.Title = input.Title!.Trim();
            movie.Summary = input.Summary!.Trim();
            movie.ReleaseDate = input.ReleaseDate!.Value;
            movie.GenreId = input.GenreId!.Value;
            movie.Price = input.Price!.Value;
            movie.Rated = input.Rated!.Value;
            movie.PicturePath = newPoster ?? previousPoster;
            if (isNew)
                db.Movies.Add(movie);

            try
            {
                await db.SaveChangesAsync(cancellationToken);
                if (newPoster is not null)
                    posters.DeleteUpload(previousPoster);

                TempData["Success"] = isNew ? "Đã thêm phim." : "Đã cập nhật phim.";
                return RedirectToAction(nameof(Manage));
            }
            catch (DbUpdateConcurrencyException)
            {
                posters.DeleteUpload(newPoster);
                return NotFound();
            }
            catch (DbUpdateException exception)
            {
                posters.DeleteUpload(newPoster);
                logger.LogWarning(exception, "Không thể lưu phim {MovieId}", movie.Id);
                ModelState.AddModelError("", "Không thể lưu phim. Hãy kiểm tra lại thể loại rồi thử lại.");
            }
            catch (OperationCanceledException) when (newPoster is not null)
            {
                await CleanCanceledUploadAsync(newPoster);
                throw;
            }
        }

        input.Genres = await GenreOptionsAsync(input.GenreId, cancellationToken);
        return View(viewName, input);
    }

    private async Task CleanCanceledUploadAsync(string newPoster)
    {
        // Cancellation can arrive after the database commits. Keep a referenced poster.
        try
        {
            if (!await db.Movies.AsNoTracking().AnyAsync(item => item.PicturePath == newPoster))
                posters.DeleteUpload(newPoster);
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Không thể kiểm tra ảnh sau khi hủy lưu phim");
        }
    }
}
