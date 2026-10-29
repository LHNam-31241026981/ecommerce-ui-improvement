# Báo cáo kiểm thử Buổi 2

**Sinh viên:** Lê Hoàng Nam — MSSV 31241026981  
**Ngày:** 29/10/2026  
**Trình duyệt:** Chrome 156.0.8078.48  
**Phạm vi:** Khu vực bốn thẻ sản phẩm trên bản mã nguồn cục bộ; ChatGPT hỗ trợ thao tác kiểm thử tự động và thu thập số đo.

## 1. Căn cứ đối chiếu

Bản trước lấy từ commit `8e68a7bf8146fa6d111cdc8d210d14163d4d9ae7`. Bản sau bảo toàn bốn sản phẩm, giá, quy cách, hình và URL. Dữ liệu gốc: [trước](minh-chung/before_results.json), [sau](minh-chung/after_results.json), [kiểm tra URL bên ngoài](minh-chung/link_results.json).

Ảnh trước/sau sử dụng cùng môi trường, chiều rộng và trạng thái. Đây là kết quả của các điều kiện đã thử, không phải kết luận cho mọi trình duyệt hoặc mọi thiết bị.

## 2. Vấn đề và cách sửa

| Vấn đề | Nguyên nhân | Điều chỉnh | Kiểm tra lại |
|---|---|---|---|
| Chữ phụ nhỏ | Quy tắc cỡ chữ 8–13 px trong thẻ | Mục 7 CSS đặt quy cách, đơn giá và mô tả 14 px; nhóm sản phẩm 10 px | Đo computed style ở ba chiều rộng |
| Nhãn mở thông tin không đổi | Một chuỗi cố định trong `summary` | `.summary-closed`, `.summary-open` đổi theo `[open]` | Khi mở hiển thị "Thu gọn" |
| Vùng liên kết mua hàng thấp | Chữ inline-block không có chiều cao tối thiểu | `.product-buy-link` dùng inline-flex, padding và min-height 44 px | Sau sửa đo được 44 px |
| Quy cách chưa là danh sách | Đoạn văn chứa span | Chuyển thành ul/li, bỏ kiểu mặc định rồi dùng Flexbox | Sau sửa DOM là UL |

Thay đổi vùng liên kết mua hàng được tách riêng để giải thích tác động của CSS: URL và nội dung không đổi, chỉ thay vùng tương tác. ChatGPT hỗ trợ hiện thực và đo kiểm; không mô tả đây là thao tác sinh viên thực hiện không có AI.

## 3. Số đo trước và sau

| Chỉ số | 1366 px trước → sau | 768 px trước → sau | 390 px trước → sau |
|---|---|---|---|
| Số cột | 4 → 4 | 2 → 2 | 1 → 1 |
| Chiều rộng nội dung cuộn | 1366 → 1366 px | 768 → 768 px | 390 → 390 px |
| Chữ quy cách | 11 → 14 px | 12 → 14 px | 12 → 14 px |
| Chữ đơn giá mỗi hũ | 12 → 14 px | 12 → 14 px | 12 → 14 px |
| Chữ mô tả khi mở | 12 → 14 px | 12 → 14 px | 13 → 14 px |
| Chữ nhóm sản phẩm | 8 → 10 px | 8 → 10 px | 9 → 10 px |
| Chiều cao liên kết mua | 19,1875 → 44 px | 19,1875 → 44 px | 20,796875 → 44 px |
| Nhãn khi mở | Xem thông tin → Thu gọn | Xem thông tin → Thu gọn | Xem thông tin → Thu gọn |

Chiều rộng nội dung cuộn bằng viewport trong cả hai bản nên không ghi nhận tràn ngang ở các lần đo này. Bố cục 4–2–1 cột được giữ sau khi tăng chữ.

## 4. Kiểm tra hành vi

| Phép thử | Kết quả ở cả ba chiều rộng | Căn cứ |
|---|---|---|
| HTML mở và CSS áp dụng | Đạt | Tiêu đề đúng, cssLoaded true |
| Bốn ảnh tải đủ | Đạt | complete true, kích thước tự nhiên 800 × 600 px |
| Mở thẻ đầu tiên bằng chuột | Đạt | open true và nhãn Thu gọn |
| Tab đến summary | Đạt | 6 lần Tab ở 1366/768; 5 lần ở 390 từ điểm bắt đầu bộ kiểm tra |
| Enter mở/đóng | Đạt | keyboardOpened và keyboardClosed true |
| Focus hiển thị | Đạt | focus-visible khớp, outline solid 3 px |
| Hover trên nút thông tin | Đạt | Nền đổi thành rgb(13, 48, 38) |
| Chỉ thẻ được mở tăng chiều cao | Đạt | Ba thẻ còn lại giữ chiều cao |
| Lỗi trình duyệt được bộ kiểm tra thu thập | Không ghi nhận | errors rỗng ở ba lần chạy |

Chiều cao thẻ đầu tiên đóng → mở sau sửa:

- 1366 px: 530,21875 → 671,5 px; ba thẻ còn lại giữ 530,21875 px.
- 768 px: 582,625 → 723,90625 px; ba thẻ còn lại giữ 582,625 px.
- 390 px: 560,875 → 702,15625 px; ba thẻ còn lại giữ 560,875 px.

Các hàng phía dưới vẫn dịch chuyển theo chiều cao hàng phía trên; đó là luồng bố cục bình thường.

## 5. Liên kết

Năm URL gồm trang shop và bốn sản phẩm trả HTTP 200 khi kiểm tra GET, không chuyển sang URL khác theo dữ liệu đã lưu. Liên kết thẻ đầu giữ target _blank và href đúng sản phẩm Combo 6. Kết quả xác nhận khả năng truy cập trang tại thời điểm thử, không xác nhận tạo đơn, thanh toán hoặc giao dịch thành công. Không thực hiện giao dịch trên website tham khảo.

## 6. Ảnh minh chứng

- [Trước 1366 px](minh-chung/before_1366_products.png) và [sau 1366 px](minh-chung/after_1366_products.png).
- [Sau 1366 px toàn trang](minh-chung/after_1366_closed.png).
- [Sau 768 px toàn trang](minh-chung/after_768_closed.png) và [khung nhìn 768 px](minh-chung/after_768_viewport.png).
- [Sau 390 px toàn trang](minh-chung/after_390_closed.png).
- [Bàn phím ở 390 px](minh-chung/after_390_keyboard.png) và [khung nhìn focus](minh-chung/after_390_focus_viewport.png).

## 7. Giới hạn kiểm chứng

Chưa thử bằng trình đọc màn hình thực tế, Safari, mọi thiết bị hoặc nhóm người dùng đại diện. Bổ sung tên sản phẩm trong HTML không đồng nghĩa đã xác nhận mọi tiêu chí tiếp cận. Kiểm tra bản GitHub Pages sau cập nhật là bước riêng với kết quả cục bộ này.
