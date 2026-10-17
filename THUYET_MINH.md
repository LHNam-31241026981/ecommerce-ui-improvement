# Thiết kế lại thẻ sản phẩm trên website Yến Lam

## 1. Khu vực lựa chọn

Khu vực được lựa chọn cải tiến là thẻ sản phẩm trong trang danh mục của website Yến Lam. Phương án tập trung vào cách hiển thị hình ảnh, tên, quy cách đóng gói, giá bán và nút thao tác để khách hàng thuận tiện nhận biết, so sánh các lựa chọn. [1](https://yenlamnest.vn/shop/)

## 2. Vấn đề cần giải quyết

Tại kích thước màn hình đã khảo sát, hình ảnh trong danh sách sản phẩm có chiều cao khác nhau, khiến vị trí tên và giá giữa các thẻ chưa đồng đều. Đồng thời, số lượng hũ và dung tích thường được đưa vào tên sản phẩm, làm tăng lượng thông tin phải đọc khi so sánh nhiều bộ yến. Vì vậy, cần chuẩn hóa cấu trúc thẻ và xác lập thứ tự ưu tiên thông tin rõ ràng. [1](https://yenlamnest.vn/shop/)

## 3. Phương án cải tiến

Mỗi thẻ được tổ chức theo trình tự: ảnh minh họa, tên sản phẩm, số lượng hũ và dung tích, giá của cả bộ, giá tương đương mỗi hũ và nút "Xem thông tin". Khung ảnh thống nhất tỷ lệ 4:3; vùng tên có chiều cao tối thiểu để giữ giá và nút thao tác thẳng hàng. Giá mỗi hũ là giá trị tính toán để so sánh quy cách, không đại diện cho giá bán lẻ từng hũ.

Trang hiển thị bốn sản phẩm mẫu. Khi người xem mở "Xem thông tin", phần quy cách được mở rộng bằng HTML và có liên kết đến trang sản phẩm nguồn. Bố cục điều chỉnh thành bốn, hai hoặc một cột tùy độ rộng màn hình. Các hình minh họa SVG được tự thiết kế để bảo đảm tính độc lập của bài tập.

## 4. Phạm vi thực hiện

Sản phẩm là một trang index.html với CSS tách riêng trong style.css, có thể mở trực tiếp bằng trình duyệt. Bài tập tập trung vào giao diện, không triển khai JavaScript, backend, cơ sở dữ liệu, đăng nhập hoặc quy trình thanh toán. Các liên kết đặt mua dẫn sang website tham khảo, không tiếp nhận đơn trong bản mẫu.

## 5. Kiểm tra và điều chỉnh

Đã kiểm tra bằng trình duyệt ở các độ rộng 390, 430, 768, 1024 và 1280 px. Ảnh tải đủ, không ghi nhận tràn ngang; ở màn hình 1280 px, bốn vùng giá được căn cùng hàng. Nút mở thông tin đã được thử bằng chuột và phím Enter.

Một điều chỉnh xuất phát từ kiểm tra là đổi ngưỡng hiển thị một cột từ 420 thành 520 px, vì bố cục hai cột ở 430 px làm thông tin phụ nhỏ và khó đọc hơn. Đơn giá mỗi hũ cũng được tăng cỡ chữ tại màn hình trung bình. Những thay đổi này nhằm cải thiện khả năng đọc; hiệu quả chuyển đổi mua hàng chưa được đo lường.

Một lỗi được người dùng trực tiếp phát hiện là khi nhấn "Xem thông tin" trên một thẻ, tất cả thẻ cùng hàng đều tăng chiều cao. Nguyên nhân là thuộc tính align-items: stretch trong bộ chọn .product-grid kéo giãn các phần tử theo chiều cao của hàng. ChatGPT hỗ trợ đổi giá trị này thành start để mỗi thẻ giữ chiều cao theo nội dung riêng. Phần tử article xác định phạm vi của từng thẻ; details và summary điều khiển việc mở, đóng phần mô tả; CSS Grid tổ chức các thẻ thành cột và hàng; align-items: start căn thẻ về đầu hàng, tránh kéo giãn các thẻ bên cạnh.

Sau chỉnh sửa, kiểm tra ở 1280 px ghi nhận thẻ đầu tăng từ khoảng 495 lên 596 px khi mở mô tả, trong khi ba thẻ còn lại giữ khoảng 495 px và không thay đổi vị trí. Tại 768 và 390 px, chỉ thẻ được mở thay đổi chiều cao; hình minh họa hiển thị đầy đủ và không ghi nhận tràn ngang. Các hàng bên dưới vẫn di chuyển theo luồng bố cục thông thường. Đây là lỗi do người dùng phát hiện và được AI hỗ trợ khắc phục, không quy toàn bộ hoạt động kiểm tra cho sinh viên.

## Tài liệu tham khảo

[1] Yến Lam. [Trang cửa hàng](https://yenlamnest.vn/shop/). Khảo sát ngày 17/10/2026.
