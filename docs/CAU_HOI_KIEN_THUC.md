# Câu hỏi kiến thức Buổi 2

**Sinh viên:** Lê Hoàng Nam — MSSV 31241026981

Câu trả lời dựa trên `index.html` và `style.css` hiện tại. Bảy câu của đề nằm ở mục 12, trang 21; bảng nộp bài dẫn "phần 5" là tham chiếu không khớp.

## Câu 1. Những phần tử HTML5 nào được dùng và vì sao

`header` chứa nhận diện và điều hướng đầu trang; `nav` chứa liên kết "Sản phẩm" và "Cách chọn"; `main` bao nội dung chính. `section#san-pham` nhóm danh sách có tiêu đề; mỗi `article.product-card` là một sản phẩm có thể đọc như đơn vị nội dung riêng. `footer` ghi nguồn và giới hạn bản mẫu.

`details/summary` tạo phần mở rộng mà không cần JavaScript. `ul.product-spec` và `li` biểu thị danh sách quy cách không đòi hỏi thứ tự. `div` vẫn được dùng cho nhóm trình bày không cần ý nghĩa nội dung riêng như `.product-image` và `.product-content`.

## Câu 2. Margin khác padding như thế nào

Padding nằm bên trong biên; margin nằm bên ngoài phần tử. `.product-content` dùng padding để tên, quy cách, giá và nút không sát viền. `.product-price` dùng `margin-top` để tách vùng giá khỏi quy cách.

Liên kết mua hàng dùng `padding: 10px 12px` kết hợp `min-height: 44px`: padding mở rộng khoảng đệm quanh chữ; chiều cao tối thiểu giữ vùng tương tác đủ cao. `box-sizing: border-box` làm chiều rộng đã khai báo bao gồm padding và border, thuận tiện khi giữ nội dung trong thẻ.

## Câu 3. Flexbox dùng ở đâu và tác dụng của justify-content, align-items

Flexbox được dùng tại `.header-inner`, `.site-header nav`, `.product-spec`, `summary` và `.product-buy-link`. Đây là các nhóm sắp nội dung theo một chiều là chủ yếu.

Trong `summary`, `justify-content: space-between` tách nhãn và biểu tượng về hai phía trên trục chính; `align-items: center` căn theo trục phụ. Khi `flex-direction` là `row`, trục chính theo chiều ngang và trục phụ theo chiều dọc. `.product-spec` dùng `gap` và `flex-wrap: wrap` để giữ khoảng cách, cho phép xuống hàng khi thiếu chỗ.

## Câu 4. Grid khác Flexbox và vì sao dùng Grid cho danh sách

Flexbox chủ yếu phân bố nội dung theo một trục trong một lần bố trí. Grid mô tả cấu trúc hàng và cột, phù hợp với danh sách nhiều thẻ có các cột rõ ràng.

`.product-grid` dùng `grid-template-columns: repeat(4, minmax(0, 1fr))` để chia đều bốn cột sau khi trừ khoảng cách `gap`. Media query chuyển thành hai hoặc một cột. Flexbox xử lý quy cách và nút bên trong từng thẻ, còn Grid xử lý vị trí các thẻ.

## Câu 5. Nếu xóa display grid thì điều gì xảy ra

Nếu chỉ bỏ `display: grid` ở `.product-grid`, `div` trở về cách hiển thị mặc định. Các `article` dạng khối thường xếp dọc theo luồng; `grid-template-columns` không còn tạo lưới. Nội dung và ảnh vẫn tồn tại, nhưng bố cục bốn hoặc hai cột không còn được tạo bởi quy tắc đó.

Có thể tạm bỏ chọn thuộc tính này trong công cụ phát triển trình duyệt, quan sát rồi bật lại để kiểm chứng, không cần xóa sản phẩm hoặc viết lại HTML.

## Câu 6. Vì sao ảnh cần alt

`alt` cung cấp văn bản thay thế khi ảnh không hiển thị và hỗ trợ công cụ đọc màn hình. Ví dụ ảnh `combo-6.svg` được mô tả "Minh họa Combo 6 Hũ Yến Đủ Vị với hũ yến nắp vàng và bao bì xanh". Nội dung nêu sản phẩm và tính minh họa, thay vì chỉ lặp tên tệp.

Thêm `alt` không biến SVG thành ảnh chụp thật và không xác nhận chất lượng hàng hóa. Nó cũng không thay thế kiểm tra toàn bộ khả năng tiếp cận.

## Câu 7. Một đề xuất AI đã được kiểm tra hoặc điều chỉnh

Đề xuất mở rộng liên kết mua hàng được giới hạn ở HTML/CSS: thêm `.product-buy-link`, giữ URL, dữ liệu và không tạo chức năng đặt đơn. Trước sửa, chiều cao đo được là 19,1875 px ở 1366/768 px và 20,796875 px ở 390 px; sau sửa đều là 44 px.

Việc đổi nhãn dùng `details[open]`, không thêm JavaScript chỉ để thực hiện thao tác này. Grid, Flexbox và sửa lỗi giãn thẻ được phân loại là phần kế thừa Buổi 1. ChatGPT hỗ trợ thực hiện và kiểm thử; số đo, ảnh được lưu để đối chiếu, không ghi thành thao tác sinh viên tự làm hoàn toàn không có AI.

## Chuẩn bị sửa mã trực tiếp

| Yêu cầu | Vị trí cần sửa | Ví dụ và kết quả dự kiến |
|---|---|---|
| Tăng khoảng cách trong thẻ | `.product-content` | `padding: 26px;` làm nội dung cách biên trong xa hơn; có thể tăng chiều cao hoặc làm chữ xuống dòng |
| Đổi màu giá chính | `.product-price strong` | Thêm `color: #a12c20;` chỉ đổi màu giá chính |
| Đổi sang ba cột | `.product-grid` tại ngưỡng cần sửa | `grid-template-columns: repeat(3, minmax(0, 1fr));` tạo ba cột ở ngưỡng đó |
| Đổi khoảng cách menu | `.site-header nav` | `gap: 24px;` thay khoảng cách giữa các liên kết |
| Sửa mô tả ảnh | `alt` của `img` tương ứng | Nội dung thay thế đổi, hình đang tải giữ nguyên |

Trước khi sửa cần xác định media query đang có hiệu lực, dự đoán rồi chạy lại trang. Những ví dụ luyện thao tác này không phải thay đổi đã áp dụng nếu mã cuối không chứa chúng.
