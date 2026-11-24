using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Filmora.Data;
using Filmora.Models;
using Filmora.Services;
using Filmora.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Controllers;

[AutoValidateAntiforgeryToken]
public class CartController(
    ApplicationDbContext context,
    CartService cart,
    OrderEmailService email,
    IDataProtectionProvider dataProtection) : Controller
{
    private readonly IDataProtector previewProtector = dataProtection.CreateProtector("Filmora.CheckoutPreview.v1");

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
        View(await cart.GetAsync(cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Add(
        int movieId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid || !await cart.AddAsync(movieId, quantity, cancellationToken))
        {
            TempData["CartError"] = "Không thể thêm phim. Kiểm tra phim còn bán và số lượng không vượt quá 99.";
        }
        else
        {
            TempData["CartSuccess"] = "Đã thêm phim vào giỏ hàng.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(
        int movieId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid || !await cart.UpdateAsync(movieId, quantity, cancellationToken))
        {
            TempData["CartError"] = "Chọn số lượng từ 1 đến 99 cho phim còn trong giỏ.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int movieId, CancellationToken cancellationToken)
    {
        await cart.RemoveAsync(movieId, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Checkout(CancellationToken cancellationToken)
    {
        var currentCart = await cart.GetAsync(cancellationToken);
        if (!currentCart.CanCheckout)
        {
            return RedirectToAction(nameof(Index));
        }

        var reference = await cart.GetOrCreateReferenceAsync(cancellationToken);
        return View(new CheckoutViewModel
        {
            Reference = reference,
            PreviewToken = previewProtector.Protect(CreatePreviewDigest(reference, currentCart)),
            Email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            Cart = currentCart
        });
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(
        CheckoutViewModel model,
        CancellationToken cancellationToken)
    {
        await HttpContext.Session.LoadAsync(cancellationToken);
        if (cart.HasCompletedReference(model.Reference))
        {
            return RedirectToAction(nameof(Completed), new { reference = model.Reference });
        }

        if (!cart.MatchesPendingReference(model.Reference))
        {
            TempData["CartError"] = "Giỏ hàng đã thay đổi. Kiểm tra lại số lượng trước khi đặt hàng.";
            return RedirectToAction(nameof(Index));
        }

        if (await OrderExistsAsync(model.Reference, cancellationToken))
        {
            return await CompleteOrderAsync(model.Reference);
        }

        model.Cart = await cart.GetAsync(cancellationToken);
        if (!model.Cart.CanCheckout)
        {
            TempData["CartError"] = "Giỏ hàng đã thay đổi. Vui lòng kiểm tra lại phim còn bán.";
            return RedirectToAction(nameof(Index));
        }

        var digest = CreatePreviewDigest(model.Reference, model.Cart);
        if (!MatchesPreview(model.PreviewToken, digest))
        {
            model.PreviewToken = previewProtector.Protect(digest);
            ModelState.Remove(nameof(CheckoutViewModel.PreviewToken));
            ModelState.AddModelError("", "Đơn hàng đã thay đổi. Kiểm tra lại nội dung và tổng tiền, rồi bấm xác nhận lần nữa.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var order = new HoaDon
        {
            Reference = model.Reference,
            CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NgayLap = DateTime.UtcNow,
            TongTien = model.Cart.Total,
            ChiTietHoaDons = model.Cart.Lines.Select(line => new CTHoaDon
            {
                MaMovie = line.MovieId,
                TenPhim = line.Title,
                SoLuong = line.Quantity,
                DonGia = line.Price
            }).ToList(),
            ShippingDetail = new ShippingDetail
            {
                Name = model.Name.Trim(),
                Address = model.Address.Trim(),
                Email = model.Email.Trim(),
                Mobile = model.Mobile.Trim()
            }
        };

        context.HoaDons.Add(order);
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            // Two valid requests for one cart can reach SaveChanges concurrently.
            if (await OrderExistsAsync(model.Reference, cancellationToken))
            {
                return await CompleteOrderAsync(model.Reference);
            }

            var refreshedCart = await cart.GetAsync(cancellationToken);
            if (!refreshedCart.CanCheckout)
            {
                TempData["CartError"] = "Một phim vừa ngừng bán. Kiểm tra lại giỏ hàng trước khi đặt.";
                return RedirectToAction(nameof(Index));
            }

            throw;
        }

        var confirmation = await CompleteOrderAsync(order.Reference);
        await email.SendConfirmationAsync(order);
        return confirmation;
    }

    [HttpGet]
    public async Task<IActionResult> Completed(Guid reference, CancellationToken cancellationToken)
    {
        await HttpContext.Session.LoadAsync(cancellationToken);
        if (!cart.HasCompletedReference(reference))
        {
            return NotFound();
        }

        var order = await context.HoaDons.AsNoTracking()
            .Where(order => order.Reference == reference)
            .Select(order => new CheckoutCompletedViewModel(order.MaHoaDon, order.TongTien))
            .SingleOrDefaultAsync(cancellationToken);

        return order is null ? NotFound() : View(order);
    }

    private bool MatchesPreview(string? token, string digest)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        try
        {
            return string.Equals(previewProtector.Unprotect(token), digest, StringComparison.Ordinal);
        }
        catch (CryptographicException)
        {
            return false;
        }
    }

    private static string CreatePreviewDigest(Guid reference, CartViewModel currentCart)
    {
        var lines = currentCart.Lines.OrderBy(line => line.MovieId);
        var snapshot = JsonSerializer.SerializeToUtf8Bytes(new { Reference = reference, Lines = lines });
        return Convert.ToHexString(SHA256.HashData(snapshot));
    }

    private Task<bool> OrderExistsAsync(Guid reference, CancellationToken cancellationToken) =>
        context.HoaDons.AnyAsync(order => order.Reference == reference, cancellationToken);

    private async Task<IActionResult> CompleteOrderAsync(Guid reference)
    {
        await cart.CompleteAsync(reference);
        return RedirectToAction(nameof(Completed), new { reference });
    }
}
