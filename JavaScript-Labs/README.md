# Bài thực hành Chương 4 JavaScript

**Lê Hoàng Nam** · MSSV **31241026981**  
**Học phần** Phát triển ứng dụng thương mại điện tử (INF509027)  
**Lớp học phần** 26C1INF50902702  
**Giảng viên** TS. Nguyễn Mạnh Tuấn

[Portfolio và bài thực hành trên GitHub Pages](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/JavaScript-Labs/)

| Bài thực hành | Mục tiêu | Tài liệu và tệp khởi tạo |
| --- | --- | --- |
| [Lab 08 – Language Foundations](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/JavaScript-Labs/Lab08/) | Nhúng script, kiểm tra Console, dùng chuỗi, ngày, toán học, mảng, hàm và đối tượng. | [JavaScript 1 Language Foundations.zip](https://drive.google.com/open?id=1XB1qiBgI_10gKjS-u5W80bXW_RmkBUJS&usp=drive_fs) |
| [Lab 09 – DOM and Events](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/JavaScript-Labs/Lab09/) | Duyệt, sửa cây DOM, xử lý sự kiện, gỡ lỗi và kiểm tra biểu mẫu. | [JavaScript 2 DOM and Events.zip](https://drive.google.com/open?id=1XCcMEyka2eSWfMETrutBkacMRrFTJoIH&usp=drive_fs) |
| [Lab 10 – jQuery](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/JavaScript-Labs/Lab10/) | Thực hành bộ chọn, sự kiện, hiệu ứng và xử lý GET bất đồng bộ qua jqXHR. | [JavaScript 3 jQuery.zip](https://drive.google.com/open?id=1X9DkdzSjvI6Xby5ddlg3Am_btxldu76v&usp=drive_fs) |

## Cấu trúc

```text
JavaScript-Labs/
├── index.html         Portfolio và liên kết đến ba Lab
├── assets/            CSS dùng chung, CSS portfolio và ảnh đại diện LN
├── Lab08/             Exercise01–Exercise11 và tài nguyên Lab 08
├── Lab09/             Exercise01–Exercise08, ảnh và trang kết quả form
├── Lab10/             Exercise01–Exercise10, jQuery, ảnh và dữ liệu GET
└── README.md           Thông tin và hướng dẫn
```

## Cách chạy

Mở portfolio trên Pages, chọn Lab và chọn Exercise cần kiểm tra. Để xem mã, mở từng thư mục `ExerciseXX` trong repository. Muốn chạy cục bộ, đứng tại thư mục gốc repository và chạy `python -m http.server 8000`, sau đó mở `http://localhost:8000/JavaScript-Labs/`. Lab 10 dùng HTTP để thực hiện GET, nên hãy chạy qua máy chủ cục bộ hoặc GitHub Pages.

- Ở Exercise 8.1 và 8.4, tắt JavaScript cho trang, tải lại rồi bật lại để so sánh. Exercise 8.5 tạo thông báo và lỗi có chủ đích khi người xem kích hoạt phần minh họa Console.
- Ở Exercise 9.8, thử gửi khi để trống trường, rồi điền đủ bốn trường và gửi lại. Form dùng dữ liệu thử và trang xác nhận tĩnh.
- Ở Exercise 10.9 và 10.10, bấm tải dữ liệu. Đường dẫn dịch vụ gốc trong giáo trình dùng HTTP. Bản Pages phục vụ bản sao JSON 53 thành phố cùng origin qua HTTPS, để trình duyệt thực hiện `$.get()` thật. Exercise 10.10 có thêm tình huống phản hồi sai định dạng để quan sát `.fail()` và `.always()`.

Các bài Lab dùng tài liệu và tệp khởi tạo tương ứng từ *Fundamentals of Web Development*, ấn bản 2, Randy Connolly và Ricardo Hoar. Tài nguyên đi kèm được dùng ở những Exercise cần đến.

## Phạm vi hỗ trợ của AI

Codex hỗ trợ đọc đề, triển khai mã từ tệp khởi tạo, kiểm thử bằng trình duyệt, ghi ảnh kết quả và soạn bản gợi ý cho phần giới thiệu và tự đánh giá. Các lượt chạy được ghi nhận trong báo cáo là lượt kiểm thử có AI hỗ trợ. Sinh viên cần tự chạy lại, đọc mã và diễn đạt hai đoạn trên portfolio theo trải nghiệm của mình trước khi nộp.
