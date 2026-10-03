# 🏨 HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ

## 📌 Thông tin đề tài

**Tên đề tài:** Xây dựng hệ thống quản lý đặt phòng khách sạn và lưu trú

**Môn học:** Thực hành lập trình .NET

**Công nghệ:** ASP.NET Core 10 MVC

**Nhóm:** Nhóm 5

**Lớp:** TI17A1HN

**Repository:** `QuanLyDatPhong_UNETI5_TI17A1HN`

---

## 🎯 1. Mục tiêu đề tài

Xây dựng ứng dụng Web quản lý đặt phòng khách sạn và lưu trú bằng
**ASP.NET Core 10 MVC**.

Hệ thống hỗ trợ toàn bộ quá trình:

```text
Công bố loại phòng
        ↓
Quản lý phòng
        ↓
Tìm kiếm phòng
        ↓
Đặt phòng
        ↓
Xác nhận / Từ chối
        ↓
Nhận phòng
        ↓
Sử dụng dịch vụ
        ↓
Trả phòng
        ↓
Lập hóa đơn
        ↓
Thanh toán
        ↓
Dashboard & Thống kê

```
Hệ thống phục vụ 3 nhóm người dùng:

Admin
Lễ tân
Khách hàng
## 🛠️ 2. Công nghệ sử dụng
Công nghệ	Phiên bản / Mục đích
C#	Ngôn ngữ lập trình
.NET	10
ASP.NET Core MVC	10
Entity Framework Core	10
SQL Server	Cơ sở dữ liệu
LINQ	Truy vấn và xử lý dữ liệu
Razor View	Xây dựng giao diện
HTML/CSS	Giao diện Web
JavaScript	Xử lý tương tác cần thiết
Visual Studio	IDE phát triển
Git	Quản lý mã nguồn
GitHub	Quản lý Repository và làm việc nhóm
👥 3. Phân quyền người dùng
👨‍💼 Admin

Admin có quyền:

Quản lý tài khoản.
Quản lý loại phòng.
Quản lý dữ liệu hệ thống.
Truy cập các chức năng quản trị.
Theo dõi Dashboard và thống kê.
🧑‍💼 Lễ tân

Lễ tân có quyền:

Quản lý phòng.
Tiếp nhận đặt phòng.
Xác nhận / từ chối đặt phòng.
Làm thủ tục nhận phòng.
Làm thủ tục trả phòng.
Ghi nhận dịch vụ phát sinh.
Lập hóa đơn.
Theo dõi Dashboard.
Xem thống kê.
👤 Khách hàng

Khách hàng có quyền:

Đăng nhập / đăng xuất.
Quản lý thông tin cá nhân.
Tìm kiếm phòng.
Xem phòng còn trống.
Đặt phòng.
Hủy đặt phòng theo điều kiện.
Theo dõi trạng thái đặt phòng.
Xem hóa đơn của chính mình.
🧩 4. Phân chia Module

Hệ thống được chia thành 05 Module chính.

Module	Chức năng
Module 1	Tài khoản - Đăng nhập - Phân quyền - Quản lý loại phòng
Module 2	Quản lý phòng - Tìm kiếm - Lọc - Sắp xếp - Phân trang
Module 3	Quản lý khách hàng - Hồ sơ cá nhân - Đặt phòng - Theo dõi đặt phòng
Module 4	Tiếp nhận đặt phòng - Xác nhận/Từ chối - Nhận phòng - Trả phòng
Module 5	Dịch vụ phát sinh - Hóa đơn - Dashboard - Thống kê
🔐 5. Module 1 - Tài khoản, đăng nhập, phân quyền và loại phòng
Tài khoản

Thông tin tài khoản gồm:

Mã tài khoản
Tên đăng nhập
Mật khẩu
Họ tên
Email
Vai trò
Trạng thái

Các vai trò:

Admin
Lễ tân
Khách hàng
Đăng nhập

Hệ thống kiểm tra:

Tài khoản có tồn tại hay không.
Mật khẩu.
Trạng thái tài khoản.

Sau khi đăng nhập thành công, hệ thống lưu thông tin cần thiết vào Session.

Phân quyền

Hệ thống kiểm tra quyền tại Controller trước khi xử lý chức năng.

Quản lý loại phòng

Thông tin loại phòng:

Mã loại phòng
Tên loại phòng
Mô tả
Sức chứa tối đa
Giá tham khảo
Trạng thái

Hỗ trợ:

Xem danh sách
Xem chi tiết
Thêm
Sửa
Xóa
Cập nhật trạng thái
🛏️ 6. Module 2 - Quản lý phòng

Thông tin phòng gồm:

Mã phòng
Số phòng
Loại phòng
Tầng
Sức chứa
Giá một đêm
Diện tích
Tiện nghi
Mô tả
Trạng thái

Trạng thái phòng:

Sẵn sàng
Đang bảo trì
Ngừng kinh doanh
Chức năng
CRUD phòng
Thêm phòng
Sửa phòng
Xem chi tiết
Cập nhật trạng thái
Tìm kiếm

Có thể tìm kiếm theo:

Số phòng
Tên loại phòng
Lọc

Hỗ trợ lọc theo:

Loại phòng
Tầng
Sức chứa tối thiểu
Khoảng giá
Trạng thái
Khoảng ngày nhận phòng / trả phòng
Sắp xếp
Số phòng tăng dần
Số phòng giảm dần
Giá tăng dần
Giá giảm dần
Sức chứa tăng/giảm
Phân trang

Danh sách phòng được phân trang bằng truy vấn EF Core/LINQ.

Quy trình:

Truy vấn dữ liệu
       ↓
Tìm kiếm
       ↓
Lọc
       ↓
Sắp xếp
       ↓
Phân trang
       ↓
Hiển thị

Các điều kiện tìm kiếm, lọc và sắp xếp được giữ lại khi chuyển trang.

👤 7. Module 3 - Quản lý khách hàng và đặt phòng
Quản lý khách hàng

Thông tin khách hàng gồm:

Mã khách hàng
Mã tài khoản
Họ tên
Ngày sinh
Giới tính
Số điện thoại
Email
Địa chỉ
Số giấy tờ tùy thân
Quốc tịch
Trạng thái

Khách hàng chỉ được cập nhật thông tin cá nhân của chính mình.

Đặt phòng

Khách hàng cần đăng nhập trước khi đặt phòng.

Thông tin đặt phòng:

Khách hàng
Phòng
Ngày nhận phòng
Ngày trả phòng
Số người
Yêu cầu đặc biệt
Ngày đặt
Tổng tiền dự kiến
Trạng thái
Trạng thái đặt phòng
Chờ xác nhận
      ↓
Đã xác nhận
      ↓
Đã nhận phòng
      ↓
Đã trả phòng

Các nhánh:

Chờ xác nhận → Từ chối

Đặt phòng phù hợp → Đã hủy
Kiểm tra khi đặt phòng

Hệ thống kiểm tra:

Khách hàng tồn tại và đang hoạt động.
Phòng tồn tại.
Phòng ở trạng thái Sẵn sàng.
Ngày nhận phòng không nằm trong quá khứ.
Ngày trả phòng sau ngày nhận phòng.
Số người không vượt quá sức chứa.
Phòng không bị đặt trùng trong khoảng thời gian lưu trú.

Tổng tiền dự kiến được hệ thống tự tính:

Tổng tiền = Số đêm × Giá phòng / đêm
🧾 8. Module 4 - Tiếp nhận và xử lý đặt phòng

Lễ tân có thể:

Xem danh sách đặt phòng.
Tìm kiếm theo tên khách hàng.
Tìm kiếm theo số phòng.
Lọc theo loại phòng.
Lọc theo trạng thái.
Lọc theo ngày nhận phòng.
Sắp xếp theo ngày đặt.
Sắp xếp theo ngày nhận phòng.
Xem chi tiết đặt phòng.
Xác nhận đặt phòng

Chỉ đặt phòng ở trạng thái:

Chờ xác nhận

mới được chuyển sang:

Đã xác nhận

hoặc:

Từ chối

Khi xử lý, hệ thống ghi nhận:

Ngày xử lý.
Ghi chú lễ tân.
Nhận phòng

Chỉ được nhận phòng khi:

Đặt phòng = Đã xác nhận

và đã đến ngày nhận phòng.

Khi nhận phòng:

Ngày nhận thực tế = Thời điểm thực hiện
Trả phòng

Khi khách trả phòng:

Đặt phòng = Đã trả phòng

Hệ thống ghi nhận:

Ngày trả thực tế
🧴 9. Module 5 - Dịch vụ phát sinh

Các dịch vụ có thể bao gồm:

Ăn uống
Giặt là
Đưa đón
Các dịch vụ khác

Thông tin dịch vụ:

Mã dịch vụ phát sinh
Mã đặt phòng
Tên dịch vụ
Số lượng
Đơn giá
Ngày sử dụng
Ghi chú
Trạng thái

Trạng thái:

Đã ghi nhận
Đã hủy
Tính tiền dịch vụ
Thành tiền = Số lượng × Đơn giá

Chỉ đặt phòng ở trạng thái:

Đã nhận phòng

mới được ghi nhận dịch vụ phát sinh.

💰 10. Hóa đơn thanh toán

Thông tin hóa đơn gồm:

Mã hóa đơn
Mã đặt phòng
Số đêm
Tiền phòng
Tiền dịch vụ
Giảm giá
Tổng thanh toán
Hình thức thanh toán
Ngày lập
Trạng thái

Hình thức thanh toán:

Tiền mặt
Chuyển khoản
Thẻ

Trạng thái:

Chưa thanh toán
Đã thanh toán
Công thức
Tiền phòng = Số đêm × Giá phòng

Tiền dịch vụ = Tổng tiền các dịch vụ chưa hủy

Tổng thanh toán =
Tiền phòng + Tiền dịch vụ - Giảm giá

Mỗi đặt phòng tối đa có một hóa đơn.

📊 11. Dashboard

Dashboard cung cấp các thông tin tổng quan:

Tổng số loại phòng.
Tổng số phòng.
Số phòng sẵn sàng.
Tổng số khách hàng.
Tổng số đặt phòng.
Số đặt phòng chờ xác nhận.
Số khách đang lưu trú.
Số đặt phòng sắp đến trong 07 ngày.
Doanh thu tháng hiện tại.
📈 12. Thống kê

Hệ thống sử dụng LINQ để thống kê:

Số phòng theo loại phòng.
Số đặt phòng theo từng phòng.
Số đặt phòng theo trạng thái.
Phòng được đặt nhiều nhất.
Doanh thu theo tháng.
Doanh thu theo loại phòng.
Doanh thu dịch vụ theo tên dịch vụ.
Số khách trả phòng theo tháng.
Tỷ lệ lấp đầy phòng.
Tỷ lệ đặt phòng bị hủy hoặc từ chối.

Các phương thức LINQ được sử dụng:

Count()
Sum()
GroupBy()
OrderBy()
OrderByDescending()
Any()
🗄️ 13. Thiết kế cơ sở dữ liệu

Các Entity chính:

TaiKhoan
LoaiPhong
Phong
KhachHang
DatPhong
DichVuPhatSinh
HoaDon
Quan hệ chính
TaiKhoan
   │
   └── KhachHang

LoaiPhong
   │
   └── Phong

KhachHang
   │
   └── DatPhong
          │
          ├── DichVuPhatSinh
          │
          └── HoaDon

Phong
   │
   └── DatPhong
Quan hệ
TaiKhoan      1 ─── 0..1 KhachHang

LoaiPhong    1 ─── n Phong

KhachHang    1 ─── n DatPhong

Phong        1 ─── n DatPhong

DatPhong     1 ─── 0..n DichVuPhatSinh

DatPhong     1 ─── 0..1 HoaDon
🏗️ 14. Kiến trúc ASP.NET Core MVC

Project được xây dựng theo mô hình:

                ┌───────────────┐
                │     User      │
                └───────┬───────┘
                        │
                        ▼
                ┌───────────────┐
                │     View      │
                │ Razor / HTML  │
                └───────┬───────┘
                        │
                        ▼
                ┌───────────────┐
                │   Controller  │
                └───────┬───────┘
                        │
                        ▼
                ┌───────────────┐
                │     Model     │
                │    Entity     │
                └───────┬───────┘
                        │
                        ▼
                ┌───────────────┐
                │   DbContext   │
                │ EF Core 10    │
                └───────┬───────┘
                        │
                        ▼
                ┌───────────────┐
                │  SQL Server   │
                └───────────────┘
📁 15. Các Controller chính

Dự kiến hệ thống gồm:

TaiKhoanController
LoaiPhongController
PhongController
KhachHangController
DatPhongController
DichVuPhatSinhController
HoaDonController
ThongKeController

Mỗi Controller phụ trách một phạm vi chức năng riêng.

🔄 16. Quy trình hoạt động của hệ thống
Khách hàng
Đăng nhập
   ↓
Cập nhật thông tin cá nhân
   ↓
Xem danh sách phòng
   ↓
Tìm kiếm / Lọc / Sắp xếp / Phân trang
   ↓
Chọn phòng
   ↓
Đặt phòng
   ↓
Chờ xác nhận
   ↓
Đã xác nhận
   ↓
Nhận phòng
   ↓
Sử dụng dịch vụ
   ↓
Trả phòng
   ↓
Xem hóa đơn
Lễ tân / Admin
Đăng nhập
   ↓
Quản lý loại phòng
   ↓
Quản lý phòng
   ↓
Tiếp nhận đặt phòng
   ↓
Xác nhận / Từ chối
   ↓
Nhận phòng
   ↓
Ghi nhận dịch vụ
   ↓
Trả phòng
   ↓
Lập hóa đơn
   ↓
Dashboard
   ↓
Thống kê
🧪 17. Kiểm thử

Các chức năng cần kiểm thử:

Tài khoản
Đăng nhập đúng.
Sai mật khẩu.
Tài khoản không tồn tại.
Tài khoản bị khóa.
Đăng xuất.
Phân quyền.
Phòng
CRUD phòng.
Tìm kiếm.
Lọc.
Sắp xếp.
Phân trang.
Kiểm tra phòng trống.
Đặt phòng
Đặt phòng hợp lệ.
Đặt phòng trùng ngày.
Đặt phòng vượt sức chứa.
Đặt phòng bảo trì.
Hủy đặt phòng.
Xác nhận.
Từ chối.
Nhận / Trả phòng
Nhận phòng đúng điều kiện.
Không nhận phòng khi chưa xác nhận.
Không nhận phòng trước ngày.
Trả phòng hợp lệ.
Kiểm tra công suất phòng.
Dịch vụ và hóa đơn
Ghi nhận dịch vụ.
Kiểm tra số lượng.
Kiểm tra đơn giá.
Lập hóa đơn.
Không lập hóa đơn lần hai.
Tính tiền phòng.
Tính tiền dịch vụ.
Tính giảm giá.
🌱 18. Chức năng nâng cao

Các chức năng có thể phát triển thêm:

Upload ảnh phòng.
Gửi email xác nhận đặt phòng.
Thanh toán trực tuyến giả lập.
Biểu đồ Chart.js.
AJAX kiểm tra phòng trống.
Xuất Excel/PDF hóa đơn.
Lịch đặt phòng theo từng phòng.
Mã hóa mật khẩu.
ASP.NET Core Identity.
🔀 19. Quy trình làm việc với GitHub

Repository được sử dụng chung cho các thành viên trong nhóm.

Quy trình đề xuất:

Clone Repository
       ↓
Tạo Branch
       ↓
Code Module
       ↓
Test
       ↓
git add
       ↓
git commit
       ↓
git push
       ↓
Pull Request
       ↓
Review
       ↓
Merge
Tạo branch
git checkout -b feature-ten-chuc-nang

Ví dụ:

git checkout -b feature-quan-ly-phong
Commit

Mẫu commit:

[Mã SV] [Module] Nội dung công việc

Ví dụ:

[22103100001] [Phong] Them chuc nang phan trang
Push
git add .
git commit -m "[Mã SV] [Module] Nội dung công việc"
git push origin ten-branch
👥 20. Thành viên nhóm
STT	Họ và tên	Mã sinh viên	Module phụ trách
1	Chưa cập nhật	Chưa cập nhật	Module 1
2	Chưa cập nhật	Chưa cập nhật	Module 2
3	Chưa cập nhật	Chưa cập nhật	Module 3
4	Chưa cập nhật	Chưa cập nhật	Module 4
5	Chưa cập nhật	Chưa cập nhật	Module 5

Cập nhật họ tên và mã sinh viên của từng thành viên trước khi nộp project.

⚙️ 21. Cài đặt và chạy Project
Yêu cầu môi trường

Cài đặt:

.NET 10 SDK
Visual Studio 2022/phiên bản hỗ trợ .NET 10
SQL Server
SQL Server Management Studio
Git
Clone project
git clone https://github.com/nguyenduythai167243/QuanLyDatPhong_UNETI5_TI17A1HN.git

Di chuyển vào project:

cd QuanLyDatPhong_UNETI5_TI17A1HN
Khôi phục package
dotnet restore
Build project
dotnet build
Chạy project
dotnet run

Sau đó mở địa chỉ được hiển thị trong Terminal.

🗃️ 22. Entity Framework Core Migration

Project sử dụng Code First.

Quy trình:

Entity
  ↓
DbContext
  ↓
Migration
  ↓
SQL Server

Tạo Migration:

dotnet ef migrations add InitialCreate

Cập nhật Database:

dotnet ef database update
🔒 23. Validation và nghiệp vụ

Hệ thống kiểm tra các nghiệp vụ quan trọng:

Tên đăng nhập không trùng.
Tên loại phòng không trùng.
Số phòng không trùng.
Sức chứa hợp lệ.
Giá phòng hợp lệ.
Ngày trả phòng sau ngày nhận phòng.
Không đặt phòng trong quá khứ.
Không vượt sức chứa.
Không đặt trùng phòng.
Không đặt phòng đang bảo trì.
Không đặt phòng đang ngừng kinh doanh.
Kiểm tra trạng thái đặt phòng.
Kiểm tra quyền truy cập dữ liệu.
Kiểm tra dịch vụ phát sinh.
Kiểm tra hóa đơn.
Kiểm tra giảm giá.
📚 24. Kiến thức áp dụng

Project áp dụng các kiến thức:

C#
↓
Lập trình hướng đối tượng
↓
ASP.NET Core 10 MVC
↓
Model - View - Controller
↓
Entity Framework Core 10
↓
Code First
↓
Migration
↓
SQL Server
↓
CRUD
↓
Model Binding
↓
Data Annotation
↓
Validation
↓
Razor View
↓
LINQ
↓
Search / Filter / Sort / Pagination
↓
Session
↓
Đăng nhập / Phân quyền
↓
Đặt phòng
↓
Nhận phòng / Trả phòng
↓
Dịch vụ
↓
Hóa đơn
↓
Dashboard / Thống kê
↓
Git / GitHub
🤖 25. Sử dụng AI

AI được sử dụng để hỗ trợ:

Tìm hiểu kiến thức.
Tham khảo thiết kế.
Tham khảo mã nguồn.
Tìm và sửa lỗi.
Tham khảo truy vấn LINQ.
Hỗ trợ xây dựng giao diện.

Các thành viên phải hiểu và giải thích được mã nguồn do mình thực hiện.

📦 26. Sản phẩm của nhóm

Project bao gồm:

Source Code.
Repository GitHub.
Entity Framework Core Migration.
SQL Server Database.
Dữ liệu mẫu.
Tài khoản kiểm thử.
Báo cáo bài tập lớn.
Thiết kế cơ sở dữ liệu.
Bảng phân công công việc.
Hướng dẫn cài đặt và chạy chương trình.
Video minh chứng quá trình thực hiện.
📌 27. Kết quả cần đạt

Sau khi hoàn thành, nhóm xây dựng được ứng dụng Web:

HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ

với:

ASP.NET Core 10 MVC.
Entity Framework Core 10.
SQL Server.
CRUD.
Validation.
LINQ.
Search.
Filter.
Sort.
Pagination.
Session.
Đăng nhập.
Phân quyền.
Đặt phòng.
Xác nhận / Từ chối.
Kiểm tra trùng phòng.
Nhận phòng.
Trả phòng.
Dịch vụ phát sinh.
Hóa đơn.
Dashboard.
Thống kê.
GitHub.
👨‍💻 Nhóm 5 - TI17A1HN

Đề tài: Xây dựng hệ thống quản lý đặt phòng khách sạn và lưu trú

Công nghệ chính: ASP.NET Core 10 MVC + Entity Framework Core 10 + SQL Server

Repository: QuanLyDatPhong_UNETI5_TI17A1HN
