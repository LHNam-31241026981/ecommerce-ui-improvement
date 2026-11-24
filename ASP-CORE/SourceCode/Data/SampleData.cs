using Filmora.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Data;

public static class SampleData
{
    public static async Task InitializeAsync(IServiceProvider services, IConfiguration config)
    {
        var database = services.GetRequiredService<ApplicationDbContext>();
        if (config.GetValue<bool>("Demo:PopulateCatalog")
            && !await database.Movies.AnyAsync()
            && !await database.Genres.AnyAsync())
        {
            var drama = new Genre { Name = "Tâm lý" };
            var noir = new Genre { Name = "Trinh thám" };
            var scienceFiction = new Genre { Name = "Viễn tưởng" };
            var documentary = new Genre { Name = "Tài liệu" };
            var romance = new Genre { Name = "Tình cảm" };
            var today = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(7));

            database.Movies.AddRange(
                Film("Qua miền sương", "Một nhiếp ảnh gia trở lại Đà Lạt để tìm người từng chụp cùng cô cuộn phim cuối cùng. Mỗi khuôn hình đưa cô gần hơn tới câu chuyện gia đình mình.", drama, 65000, 5, 15, "qua-mien-suong"),
                Film("Hẻm lúc nửa đêm", "Một chiếc ô bỏ quên dẫn nữ phóng viên vào con hẻm nơi người ta nhớ mọi tiếng động nhưng cố quên chuyện xảy ra đêm qua.", noir, 75000, 4, 21, "hem-nua-dem"),
                Film("Đường về quỹ đạo", "Trong vùng muối trắng của một vệ tinh xa, kỹ sư liên lạc tìm được tín hiệu đến từ con tàu mà cô đã tiễn đi nhiều năm trước.", scienceFiction, 95000, 5, 33, "quy-dao"),
                Film("Theo con nước", "Qua một mùa biển, bộ phim theo chân những người chèo thuyền rời bờ trước bình minh và trở về khi ánh sáng vừa tắt.", documentary, 55000, 4, 37, "theo-con-nuoc"),
                Film("Nhà có hai cửa", "Hai người từng sống trong cùng một căn nhà gặp nhau trong ngày dỡ bỏ nó. Họ có một buổi trưa để nói điều đã bỏ lỡ.", romance, 70000, 4, 44, "hai-cua"),
                Film("Chuyến xe qua đèo", "Chuyến xe cuối ngày dừng lại giữa đèo vì sương xuống. Sáu người xa lạ bắt đầu kể cho nhau nghe về nơi họ đang trở về.", drama, 60000, 3, 52, "chuyen-xe"));
            await database.SaveChangesAsync();

            Movie Film(string title, string summary, Genre genre, decimal price, int rating, int days, string image) =>
                new()
                {
                    Title = title, Summary = summary, Genre = genre, Price = price,
                    Rated = rating, ReleaseDate = today.AddDays(days),
                    PicturePath = $"~/images/posters/{image}.webp"
                };
        }

        var email = config["Admin:Email"];
        var password = config["Admin:Password"];
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
            return;
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("Phải cấu hình cả Admin:Email và Admin:Password.");

        var roles = services.GetRequiredService<RoleManager<IdentityRole>>();
        var users = services.GetRequiredService<UserManager<IdentityUser>>();
        if (!await roles.RoleExistsAsync("Admin"))
            Verify(await roles.CreateAsync(new IdentityRole("Admin")));

        var admin = await users.FindByEmailAsync(email);
        if (admin is null)
        {
            admin = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            Verify(await users.CreateAsync(admin, password));
        }
        else if (!await users.CheckPasswordAsync(admin, password))
        {
            throw new InvalidOperationException("Tài khoản quản trị có sẵn. Admin:Password phải khớp mật khẩu hiện tại.");
        }

        if (!await users.IsInRoleAsync(admin, "Admin"))
            Verify(await users.AddToRoleAsync(admin, "Admin"));
    }

    private static void Verify(IdentityResult result)
    {
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(" ", result.Errors.Select(error => error.Description)));
    }
}
