# Bài tập cá nhân Buổi 2 HTML5 và CSS3

**Sinh viên:** Lê Hoàng Nam  
**MSSV:** 31241026981  

## 1. Nội dung và phạm vi kế thừa

Bài tiếp tục phát triển khu vực bốn thẻ sản phẩm Yến Lam trong repository Buổi 1. Bản đối chiếu là commit `8e68a7bf8146fa6d111cdc8d210d14163d4d9ae7`. Tên, quy cách, hình SVG và giá tham khảo của bốn sản phẩm được giữ nguyên.

HTML5 có ngữ nghĩa, CSS riêng, Flexbox, Grid, hover, focus, bố cục 4–2–1 cột và việc sửa lỗi giãn đồng loạt các thẻ đã có từ Buổi 1. Những thành phần này được kế thừa, không tính lại thành phần mới của Buổi 2. `THUYET_MINH.md` giữ vai trò tài liệu lịch sử Buổi 1.

## 2. Các cải tiến Buổi 2

| Vấn đề | Điều chỉnh | Căn cứ kiểm tra |
|---|---|---|
| Chữ phụ nhỏ | Quy cách, đơn giá mỗi hũ và mô tả tăng lên 14 px; điều chỉnh màu chữ và khoảng cách dòng | Computed style ở 1366, 768, 390 px |
| Nhãn không đổi khi mô tả đã mở | `.summary-closed`, `.summary-open` thay đổi theo `details[open]` | Khi mở hiển thị "Thu gọn" |
| Các thao tác chưa nêu tên sản phẩm | Bổ sung tên bằng `.sr-only` trong nhãn và liên kết | HTML có ngữ cảnh riêng của từng thẻ |
| Liên kết mua hàng có vùng tương tác nhỏ | Thêm `.product-buy-link`, padding, `min-height: 44px`, hover và focus | Chiều cao trước khoảng 19,19–20,80 px, sau là 44 px |
| Quy cách là đoạn văn chứa `span` | Chuyển sang `ul.product-spec` và `li`; dùng Flexbox | DOM sau sửa là `UL` |

CSS mới nằm ở mục `7. Buổi 2` cuối `style.css`. Liên kết CSS dùng `style.css?v=3`. Bài không sử dụng Bootstrap, Tailwind hoặc JavaScript; `details/summary` thực hiện mở/đóng bằng HTML nguyên bản.

Bổ sung sau kiểm tra bản công bố: khai báo `favicon.svg` trong phần `head` để trình duyệt tải đúng biểu tượng tab; khắc phục yêu cầu mặc định `/favicon.ico` trả 404. Biểu tượng dùng lại nét nhận diện ở phần đầu trang.

## 3. Cách chạy

1. Giải nén toàn bộ thư mục mã nguồn.
2. Mở `index.html` bằng Chrome hoặc Edge; có thể dùng Live Server của VS Code để chạy qua HTTP.
3. Giữ `style.css` và bốn tệp SVG cùng cấp với `index.html`. Dự án hiện không sử dụng thư mục `images/`.
4. Bấm "Xem thông tin" để mở và "Thu gọn" để đóng. Có thể dùng Tab đến điều khiển rồi Enter để mở/đóng.
5. Liên kết "Đến trang đặt mua" mở website tham khảo trong thẻ mới và cần Internet. Bản mẫu không tạo đơn hàng.

```text
index.html
style.css
combo-6.svg
combo-10.svg
combo-12.svg
premium-gift.svg
favicon.svg
README.md
AI_USAGE.md
THUYET_MINH.md
docs/
  BAO_CAO_KIEM_THU.md
  CAU_HOI_KIEN_THUC.md
  minh-chung/
    before_results.json
    after_results.json
    link_results.json
    các ảnh PNG minh chứng
```

## 4. Kết quả kiểm thử

ChatGPT hỗ trợ kiểm thử tự động trên Chrome 156.0.8078.48 ngày 29/10/2026 và lưu dữ liệu đo. Kết quả không được trình bày thành thao tác kiểm thử thủ công không có hỗ trợ của sinh viên.

| Chiều rộng | Số cột | Tràn ngang trước/sau | Chiều cao liên kết mua trước → sau |
|---:|---:|---|---:|
| 1366 px | 4 | Không / Không | 19,1875 → 44 px |
| 768 px | 2 | Không / Không | 19,1875 → 44 px |
| 390 px | 1 | Không / Không | 20,796875 → 44 px |

Cả ba lần chạy xác nhận: bốn ảnh tải đủ 800 × 600 px; CSS được áp dụng; mở bằng chuột và mở/đóng bằng Enter thành công; có focus 3 px; hover đổi nền. Chỉ thẻ được mở tăng chiều cao. Bộ kiểm tra không ghi nhận lỗi trình duyệt. Năm URL bên ngoài gồm trang shop và bốn trang sản phẩm trả HTTP 200 khi kiểm tra GET; không thử đặt hàng hoặc thanh toán.

- [Báo cáo kiểm thử](docs/BAO_CAO_KIEM_THU.md).
- [Bảy câu trả lời và hướng dẫn sửa mã trực tiếp](docs/CAU_HOI_KIEN_THUC.md).
- [Ảnh trước](docs/minh-chung/before_1366_products.png) và [ảnh sau](docs/minh-chung/after_1366_products.png).
- [Giao diện sau 1366 px](docs/minh-chung/after_1366_closed.png), [768 px](docs/minh-chung/after_768_closed.png), [390 px](docs/minh-chung/after_390_closed.png).
- [Minh chứng bàn phím trên điện thoại](docs/minh-chung/after_390_keyboard.png).
- [Dữ liệu trước](docs/minh-chung/before_results.json), [sau](docs/minh-chung/after_results.json), [kiểm tra URL](docs/minh-chung/link_results.json).

## 5. Đối chiếu sản phẩm cần nộp

| Yêu cầu | Tệp hoặc vị trí |
|---|---|
| HTML kế thừa Buổi 1 và CSS riêng | `index.html`, `style.css` |
| Ảnh trước và sau | `docs/minh-chung/before_1366_products.png`, `after_1366_products.png` |
| Ảnh khu vực hoàn thiện | Các ảnh `after_*_closed.png` |
| Báo cáo kiểm thử | `docs/BAO_CAO_KIEM_THU.md` |
| Nhật ký AI | Mục Buổi 2 trong `AI_USAGE.md` |
| Bảy câu hỏi và luyện sửa mã | `docs/CAU_HOI_KIEN_THUC.md` |
| Mã nguồn và README | Repository hiện có, đường dẫn bên dưới |

## 6. Sử dụng AI

ChatGPT hỗ trợ đọc đề, đối chiếu bài cũ, sửa mã, kiểm thử Chrome, lưu minh chứng và biên tập tài liệu. Nhật ký phân biệt phần kế thừa, phần mới và các phép đo thực tế. Sinh viên cần hiểu các câu trả lời và tự thực hành lại để giải thích mã khi giảng viên yêu cầu; không ghi nhận thay người học việc đã thành thạo hoặc đã thao tác không có AI.

## 7. Repository và nguồn tham khảo

- Repository tiếp tục sử dụng: [LHNam-31241026981/ecommerce-ui-improvement](https://github.com/LHNam-31241026981/ecommerce-ui-improvement).
- GitHub Pages: [Giao diện Yến Lam](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/).
- Đề Buổi 2 và báo cáo Buổi 1 do người học cung cấp là tài liệu nền. Giá sản phẩm kế thừa khảo sát ngày 17/10/2026, không phải báo giá mới ngày 29/10/2026 hoặc dữ liệu cập nhật tự động.
- Nguồn sản phẩm: [trang shop](https://yenlamnest.vn/shop/), [Combo 6 hũ](https://yenlamnest.vn/product/combo-6-hu-yen-du-vi-150ml/), [Combo 10 hũ](https://yenlamnest.vn/product/combo-10-hu-yen-du-vi-150ml/), [Combo 12 hũ](https://yenlamnest.vn/product/combo-12-hu-yen-du-vi-150ml/), [Premium Gift](https://yenlamnest.vn/product/set-qua-tang-premium-gift-10h-150ml/).
