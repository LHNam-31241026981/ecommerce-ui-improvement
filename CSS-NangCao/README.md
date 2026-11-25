# Lab 07 CSS nâng cao

**Nhóm 1** của bài tập cá nhân Advanced FrontEnd. Sinh viên **Lê Hoàng Nam**, MSSV **31241026981**, lớp học phần **26C1INF50902702**. Giảng viên **TS. Nguyễn Mạnh Tuấn**.

Lab thực hành cách phần tử tham gia luồng tài liệu, cách bố cục thay đổi theo chiều rộng và cách CSS phản hồi khi người dùng tương tác. Phần Bootstrap dùng bản 3 trong bộ khởi đầu để giữ đúng API `panel` và `jumbotron` của bài.

## Mở bài

- [Portfolio trên GitHub Pages](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/CSS-NangCao/)
- [Danh sách 19 Exercise](https://lhnam-31241026981.github.io/ecommerce-ui-improvement/CSS-NangCao/Lab07/)
- [Thư mục mã nguồn](https://github.com/LHNam-31241026981/ecommerce-ui-improvement/tree/main/CSS-NangCao)

Có thể mở trực tiếp `index.html` trên máy bằng Chrome hoặc Edge. Không cần cài package. Xem mục 7.12 ở hai kích thước quanh 480/481 px, rồi dùng liên kết 7.13 để kiểm tra cách menu đổi giữa danh sách và ô chọn. Bài 7.13 sử dụng lại mã của 7.12 theo tài liệu.

| Exercise | Kỹ thuật |
| --- | --- |
| 7.1–7.4 | Document flow, định vị tương đối, định vị tuyệt đối và stacking context |
| 7.5–7.9 | Float, clear, định vị chú thích và nhãn phủ ảnh |
| 7.10–7.13 | Bố cục hai và ba cột, media queries và viewport |
| 7.14–7.18 | Transform, transition, filter và animation |
| 7.19 | Lưới Bootstrap 12 cột, cột lồng và các thành phần giao diện |

```text
CSS-NangCao/
  index.html                 Portfolio cá nhân
  assets/                    Stylesheet và ảnh đại diện chữ LN
  Lab07/
    index.html               Điều hướng trực tiếp tới các Exercise
    Exercise01/ … Exercise19/
    bootstrap/               CSS, JS và fonts Bootstrap từ bộ khởi đầu
    images/                  Ảnh dùng trong các bài thực hành
```

## Nguồn và minh chứng

[1](https://drive.google.com/open?id=1JauEArZUR7UaORz1Jyg47yP7NYC6_mEy) Hướng dẫn Nhóm 1 của TS. Nguyễn Mạnh Tuấn.

[2](https://github.com/funwebdev-2nd-ed/ch07-lab-start/tree/8f96dde522ee04fc4bd7e3ce34b561aba9138eda) Lab 07 và tài nguyên khởi đầu của Randy Connolly và Ricardo Hoar, *Fundamentals of Web Development*, ấn bản thứ hai.

Ảnh đại diện trên portfolio là chữ LN tự thiết kế. AI hỗ trợ hoàn thiện mã, kiểm thử và soạn báo cáo. Ảnh minh chứng lấy từ các trang chạy thực tế và mã nguồn của bài.
