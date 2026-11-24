# Rạp Mở

Bài thực hành cá nhân ASP.NET Core MVC của Lê Hoàng Nam (31241026981), học phần Phát triển ứng dụng TMĐT. Website minh họa quy trình xem lịch phim, chọn vé và lưu đơn hàng. Phim, poster, tài khoản và thông tin giao hàng trong minh chứng là dữ liệu giả lập. Website xác nhận đơn hàng mẫu, chưa tích hợp cổng thanh toán.

## Công nghệ và chức năng

- ASP.NET Core MVC và Razor trên .NET 10, ASP.NET Core Identity, EF Core 10 và SQL Server.
- Danh mục công khai có tìm theo tên và lọc thể loại. Admin được thêm, xem, sửa và xóa phim, thể loại. Mỗi phim tham chiếu một thể loại có thật. Phim đã phát hành có thể giữ ngày phát hành cũ khi sửa.
- Poster JPG, PNG hoặc WebP có dung lượng tối đa 4 MB. Tên lưu được tạo tự động. Nếu thiếu tệp, website dùng poster mặc định. Bộ dữ liệu mẫu gồm sáu phim và năm thể loại. Poster mẫu được tạo bằng AI.
- Giỏ hàng Session hỗ trợ thêm, đổi số lượng và xóa. Giá lấy từ cơ sở dữ liệu. Nếu nội dung giỏ hoặc giá thay đổi từ lúc mở Checkout, người mua xem lại tổng mới trước khi xác nhận.
- Mỗi lần đặt hàng ghi hóa đơn, các dòng chi tiết và thông tin giao hàng. Mã tham chiếu duy nhất chống ghi trùng khi gửi lại cùng biểu mẫu. Dòng chi tiết giữ tên và đơn giá tại thời điểm đặt. Email Gmail SMTP là tùy chọn cho đơn hàng.

## Cấu trúc

| Đường dẫn | Nội dung |
| --- | --- |
| `SourceCode/Controllers`, `Views`, `ViewModels` | Danh mục, quản trị, giỏ hàng và đặt hàng |
| `SourceCode/Models`, `Data/Migrations` | Mô hình dữ liệu và migration SQL Server |
| `SourceCode/Services` | Session, lưu poster, xác thực và thông báo |
| `SourceCode/wwwroot` | CSS, thư viện giao diện và poster |
| `Report/` | Báo cáo Word và ảnh kiểm thử thực tế |

## Chạy trên Windows

Cài [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) và [SQL Server Express LocalDB](https://learn.microsoft.com/vi-vn/sql/database-engine/configure-windows/sql-server-express-localdb). Ở thư mục `ASP-CORE`, chạy các lệnh PowerShell dưới đây. Thay hai giá trị minh họa bằng email thử nghiệm và mật khẩu của riêng bạn. Mật khẩu cần ít nhất 6 ký tự và có chữ hoa, chữ thường, chữ số và ký tự đặc biệt.

```powershell
cd SourceCode
dotnet restore
dotnet build
dotnet user-secrets set "Admin:Email" "admin@example.invalid"
dotnet user-secrets set "Admin:Password" "<MAT_KHAU_RIENG>"
dotnet run -- --prepare-db
dotnet run
```

Mở [http://localhost:5187](http://localhost:5187) và dùng tài khoản vừa tạo để vào "Quản lý phim" hoặc "Thể loại". Lệnh `--prepare-db` áp dụng migration và thêm dữ liệu mẫu vào cơ sở dữ liệu danh mục rỗng khi bật `Demo:PopulateCatalog`. Nếu muốn nhập toàn bộ từ biểu mẫu, đặt cấu hình này thành `false` trước khi chuẩn bị cơ sở dữ liệu mới. Đăng ký tài khoản khách trong Development cung cấp liên kết xác nhận thử nghiệm ngay trên màn hình khi chưa cấu hình đủ Gmail và App Password.

Mặc định Development dùng cơ sở dữ liệu `RapMo_31241026981` trên LocalDB. Để chuyển sang SQL Server khác, thiết lập `ConnectionStrings__DefaultConnection` cho môi trường chạy. Xem cấu trúc giá trị trong `appsettings.example.json`. Đặt thông tin thật trong biến môi trường hoặc User Secrets. Khi dùng Gmail SMTP, cấu hình `Email__Address` và `Email__AppPassword` từ môi trường tương ứng. Production cần email thật để xác nhận tài khoản đăng ký.

## Đóng gói triển khai

Từ `SourceCode`, tạo bản publish bằng `dotnet publish -c Release -o ./publish`. Với IIS, triển khai nội dung trong `publish` cùng `web.config` vào một hosting hỗ trợ .NET 10 và SQL Server. Chuẩn bị cơ sở dữ liệu bằng lệnh `dotnet Filmora.dll --prepare-db` sau khi cấu hình môi trường, đồng thời cấp quyền ghi cho thư mục `wwwroot/uploads/posters`. LocalDB chỉ dùng phát triển trên Windows. Kết quả kiểm thử và trạng thái hosting nằm trong báo cáo.

## Tài liệu

[1](https://learn.microsoft.com/aspnet/core/security/authentication/identity?view=aspnetcore-10.0) ASP.NET Core Identity · [2](https://learn.microsoft.com/ef/core/managing-schemas/migrations/) EF Core Migrations · [3](https://learn.microsoft.com/aspnet/core/fundamentals/app-state?view=aspnetcore-10.0) Session và trạng thái ứng dụng.
