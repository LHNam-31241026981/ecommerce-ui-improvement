# E-commerce UI Improvement - Yến Lam

## 1. Giới thiệu

Bài tập cải tiến thẻ sản phẩm tại website Yến Lam. Một trang HTML/CSS có bốn sản phẩm mẫu, chuẩn hóa khung ảnh, vị trí tên, quy cách, giá và nút thao tác. Hình minh họa được tự thiết kế bằng SVG; giá là dữ liệu tham khảo từ website được khảo sát ngày 17/10/2026.

## 2. Website tham khảo

- Tên: Yến Lam.
- Trang cửa hàng: https://yenlamnest.vn/shop/
- Khu vực cải tiến: thẻ sản phẩm trong danh sách sản phẩm.
- Căn cứ: ảnh trên trang shop có chiều cao khác nhau tại bố cục đã khảo sát, làm tên và giá giữa các thẻ chưa đồng đều.

## 3. Chức năng

- Hiển thị bốn sản phẩm, mỗi thẻ có ảnh minh họa, tên, số hũ, dung tích, tổng giá và giá tương đương mỗi hũ.
- Nút "Xem thông tin" mở/đóng mô tả bằng phần tử details/summary của HTML. Chỉ thẻ được bấm tăng chiều cao; các thẻ cùng hàng không bị kéo giãn theo.
- Liên kết "Đến trang đặt mua" dẫn sang trang sản phẩm thật, không tạo đơn trong bản mẫu.
- Bố cục bốn cột trên màn hình rộng, hai cột trên màn hình trung bình và một cột ở điện thoại từ 520 px trở xuống.
- Có nhãn ảnh, thứ tự tiêu đề, trạng thái focus và hỗ trợ giảm chuyển động.

## 4. Công nghệ

- HTML5: header, nav, main, section, article, details, summary, footer.
- CSS3: Grid, Flexbox, aspect-ratio, object-fit và media query.
- SVG: bốn hình minh họa gốc, dùng như ảnh local.
- Không dùng JavaScript, backend, cơ sở dữ liệu, đăng nhập hoặc thanh toán.

## 5. Cách chạy

1. Giải nén toàn bộ thư mục yen-lam-ui.
2. Mở index.html bằng trình duyệt Chrome hoặc Edge.
3. Giữ nguyên vị trí style.css và thư mục images/ để trang nạp đủ giao diện.
4. Nhấn "Xem thông tin" để mở/đóng mô tả. Liên kết ra website nguồn cần Internet; giao diện chính và ảnh chạy trực tiếp từ các file local.

Cấu trúc:

- index.html
- style.css
- images/combo-6.svg
- images/combo-10.svg
- images/combo-12.svg
- images/premium-gift.svg
- README.md
- AI_USAGE.md
- THUYET_MINH.md

## 6. Cải tiến và kiểm tra

- Khung ảnh được thống nhất theo tỷ lệ 4:3, object-fit: cover.
- Tên có khoảng cao tối thiểu để giá và nút được căn đều.
- Số hũ, dung tích và đơn giá tương đương tách rõ khỏi tên.
- Nút thao tác luôn hiển thị và có nội dung mở rộng thật bằng HTML.
- Kiểm tra trình duyệt tại độ rộng 390, 430, 768, 1024 và 1280 px: ảnh tải đủ, không ghi nhận tràn ngang. Ở 1280 px, bốn khu vực giá cùng tọa độ theo chiều dọc.
- Đã thử mở mô tả bằng chuột và phím Enter, xác nhận có nội dung và liên kết đúng sản phẩm.
- Điều chỉnh sau kiểm tra: tại 430 px, hai cột khiến thông tin phụ nhỏ; đã nâng ngưỡng một cột từ 420 lên 520 px. Đồng thời tăng đơn giá mỗi hũ từ 9 lên 11 px ở khoảng màn hình trung bình để dễ đọc.
- Điều chỉnh từ lỗi do người dùng phát hiện: mở "Xem thông tin" từng khiến cả bốn thẻ cùng hàng tăng chiều cao. Đã đổi align-items: stretch thành align-items: start ở .product-grid. Kiểm tra lại tại 1280, 768 và 390 px: chỉ thẻ được mở tăng chiều cao, các thẻ khác giữ nguyên chiều cao; không ghi nhận tràn ngang. Các hàng phía dưới vẫn dịch chuyển tự nhiên theo chiều cao của hàng phía trên.
- Đây là kiểm tra do ChatGPT hỗ trợ; sinh viên cần tự mở lại và hiểu những thành phần được giải thích trong bài.

## 7. Hỗ trợ từ AI

Gemini hỗ trợ phân tích website; ChatGPT hỗ trợ kiểm chứng, đề xuất bố cục, tạo HTML/CSS và SVG, kiểm tra responsive và ghi nhật ký. Chi tiết tại AI_USAGE.md. Nội dung lựa chọn, vấn đề và giải pháp nằm trong THUYET_MINH.md.

## Nguồn dữ liệu sản phẩm

1. Combo 6 hũ 150 ml: https://yenlamnest.vn/product/combo-6-hu-yen-du-vi-150ml/
2. Combo 10 hũ 150 ml: https://yenlamnest.vn/product/combo-10-hu-yen-du-vi-150ml/
3. Combo 12 hũ 150 ml: https://yenlamnest.vn/product/combo-12-hu-yen-du-vi-150ml/
4. Premium Gift 10 hũ 150 ml: https://yenlamnest.vn/product/set-qua-tang-premium-gift-10h-150ml/

Giá tham khảo được lấy từ phiên khảo sát ngày 17/10/2026; không thể hiện là giá cập nhật tự động. Không sao chép mã nguồn hoặc ảnh sản phẩm của website tham khảo.
