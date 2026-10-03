# BÀI TẬP LỚN MÔN THỰC HÀNH LẬP TRÌNH .NET
## ĐỀ TÀI: HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ
### ASP.NET Core 10 MVC & Entity Framework Core 10 (Code First)

---

### THÔNG TIN SINH VIÊN THỰC HIỆN
- **Họ và tên:** Nguyễn Duy Thái
- **Mã sinh viên:** 23103100052
- **Lớp / Nhóm:** Nhóm 5 - Lớp TI17A1HN
- **Chuyên trách:** **Module 4 - Tiếp nhận đặt phòng, xác nhận/từ chối, nhận phòng, trả phòng, quản lý trạng thái lưu trú và kiểm soát công suất phòng**

---

## 1. CÔNG NGHỆ VÀ PHIÊN BẢN SỬ DỤNG
- **Framework:** .NET 10 SDK (`net10.0`)
- **Kiến trúc:** ASP.NET Core 10 MVC (Model - View - Controller)
- **ORM:** Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.SqlServer` 10.0.12, `Tools` 10.0.12, `Design` 10.0.12)
- **Cơ sở dữ liệu:** Microsoft SQL Server / SQL Server LocalDB (`(localdb)\mssqllocaldb`)
- **Giao diện:** Razor View Engine, Bootstrap 5, Bootstrap Icons, Google Font Plus Jakarta Sans
- **Xác thực & Phiên làm việc:** Session & Distributed Memory Cache

---

## 2. PHẠM VI VÀ CÁC CHỨC NĂNG CỦA MODULE 4 ĐÃ HOÀN THÀNH

### 2.1. Tiếp nhận đặt phòng (Section 8.1)
- Hiển thị danh sách toàn bộ các đặt phòng cần xử lý.
- **Tìm kiếm bằng LINQ:** Theo họ tên khách hàng, số phòng hoặc mã đặt phòng.
- **Lọc đa tiêu chí kết hợp bằng LINQ:**
  - Lọc theo Loại phòng.
  - Lọc theo Trạng thái (Chờ xác nhận, Đã xác nhận, Đã nhận phòng, Đã trả phòng, Từ chối, Đã hủy).
  - Lọc theo Ngày nhận phòng.
  - Bộ lọc riêng cho các đơn **Quá hạn nhận phòng (No-show)**.
- **Sắp xếp linh hoạt bằng LINQ:**
  - Ngày đặt: Mới nhất / Cũ nhất.
  - Ngày nhận phòng: Gần nhất / Xa nhất.
  - Số phòng: Tăng dần.
- **Phân trang LINQ chuẩn xác:** Sử dụng `Skip()` và `Take()`, tự động giữ nguyên toàn bộ từ khóa tìm kiếm, bộ lọc và kiểu sắp xếp khi chuyển trang.

### 2.2. Kiểm tra điều kiện và Xác nhận / Từ chối đặt phòng (Section 8.2 & 8.3)
- Chỉ cho phép xử lý các đặt phòng đang ở trạng thái **Chờ xác nhận**.
- **Kiểm tra tự động toàn diện bằng LINQ trước khi xác nhận:**
  1. Kiểm tra phòng tồn tại và đang ở trạng thái **"Sẵn sàng"**.
  2. Kiểm tra khách hàng còn hoạt động bình thường (không bị khóa).
  3. **Thuật toán kiểm tra trùng lịch lưu trú chuẩn xác:**
     $$\text{NgayNhan}_A < \text{NgayTra}_B \quad \text{và} \quad \text{NgayTra}_A > \text{NgayNhan}_B$$
     *(Cho phép đặt phòng gối đầu: Ngày trả phòng của khách trước có thể trùng với ngày nhận phòng của khách sau).*
  4. **Kiểm soát công suất phòng theo loại:** Đếm số phòng sẵn sàng và số phòng đã kín lịch trong khoảng ngày lưu trú. Cảnh báo và chặn xác nhận nếu loại phòng đó đã kín 100% lịch.
- Ghi nhận `NgayXuLy = DateTime.Now` và bắt buộc nhập `GhiChuLeTan`.

### 2.3. Quản lý luồng trạng thái lưu trú (Section 8.4)
- **Luồng chuẩn:** `Chờ xác nhận` $\rightarrow$ `Đã xác nhận` $\rightarrow$ `Đã nhận phòng` $\rightarrow$ `Đã trả phòng`
- **Nhánh từ chối/hủy:** `Chờ xác nhận` $\rightarrow$ `Từ chối`; `Đã xác nhận` $\rightarrow$ `Đã hủy`.
- **Thủ tục nhận phòng (Check-in):**
  - Chỉ cho phép khi đặt phòng `Đã xác nhận` và **đã đến ngày nhận phòng** (`DateTime.Today >= NgayNhanPhong.Date`).
  - Ghi nhận chính xác `NgayNhanThucTe`.
- **Thủ tục trả phòng (Check-out):**
  - Chỉ cho phép khi đặt phòng `Đã nhận phòng`.
  - Ghi nhận chính xác `NgayTraThucTe`.
  - Tự động tính số đêm thực tế (tối thiểu 1 đêm).
  - Tự động tính tổng tiền dịch vụ phát sinh bằng `LINQ Sum()`.
  - Tự động lập Hóa đơn thanh toán (`HoaDon`) với hình thức: Tiền mặt, Chuyển khoản hoặc Thẻ.

### 2.4. Kiểm soát công suất phòng & Cảnh báo No-show (Section 8.5)
- Màn hình chuyên dụng **Kiểm soát công suất**:
  - Chọn khoảng ngày kiểm tra linh hoạt.
  - Bảng thống kê LINQ: Tổng số phòng, Số phòng sẵn sàng, Số phòng bảo trì, Số phòng đã đặt, Số phòng còn trống, **Tỷ lệ lấp đầy (%)** và nhãn cảnh báo.
  - **Sơ đồ trạng thái phòng thời gian thực:** Trực quan hóa 30 phòng trên 5 tầng (Sẵn sàng/Trống, Đang có khách, Sắp nhận hôm nay, Đang bảo trì).
- **Cảnh báo No-show:** Phát hiện tự động các đặt phòng `Đã xác nhận` nhưng quá ngày hẹn mà khách chưa đến làm thủ tục, gắn nhãn cảnh báo đỏ nhấp nháy và cung cấp nút hủy quá hạn để giải phóng phòng.

---

## 3. DỮ LIỆU MẪU (SEED DATA - MỤC 16)
Khi ứng dụng khởi chạy lần đầu, `DbInitializer` sẽ tự động tạo cơ sở dữ liệu và nạp toàn bộ dữ liệu mẫu chuẩn nghiệp vụ:
- **05 Loại phòng:** Standard (STD), Superior (SUP), Deluxe (DLX), Suite (SUT), Family (FAM).
- **30 Phòng:** Từ P101 đến P506 (Tầng 1 đến Tầng 5), có phòng Sẵn sàng, Đang bảo trì và Ngừng kinh doanh.
- **30 Khách hàng:** Đầy đủ họ tên, ngày sinh, số CCCD/Hộ chiếu, SĐT, Email.
- **Tài khoản người dùng:**
  - `admin` (Mật khẩu: `123456`) - Vai trò Admin
  - `letan` (Mật khẩu: `123456`) - Vai trò Lễ tân (Lê Thị Thu Thảo - Lễ tân trưởng)
  - `letan2` (Mật khẩu: `123456`) - Lễ tân ca ngày
  - `letan3` (Mật khẩu: `123456`) - Lễ tân ca đêm
  - `khach01` đến `khach30` (Mật khẩu: `123456`) - Khách hàng
- **45 Đặt phòng:** Phân bổ đầy đủ các trạng thái để test:
  - 8 đơn `Chờ xác nhận` (để test duyệt/từ chối).
  - 10 đơn `Đã xác nhận` (gồm 2 đơn quá hạn để test cảnh báo No-show, 2 đơn nhận phòng hôm nay để test Check-in ngay).
  - 10 đơn `Đã nhận phòng` (để test thêm dịch vụ và Check-out).
  - 12 đơn `Đã trả phòng` (có hóa đơn).
  - Các ca đặt phòng **gối đầu**: Khách A trả ngày 10, Khách B nhận ngày 10 trên cùng phòng.
- **25 Dịch vụ phát sinh** & **18 Hóa đơn thanh toán**.

---

## 4. HƯỚNG DẪN CÀI ĐẶT VÀ CHẠY DỰ ÁN

### Cách 1: Chạy bằng Visual Studio Code
1. Mở thư mục gốc `NguyenDuyThai_23103100052_Model4` trong **VS Code**.
2. **Cách nhanh (Terminal):**
   - Mở Terminal trong VS Code bằng phím tắt ``Ctrl + ` `` (hoặc menu **Terminal -> New Terminal**).
   - Chạy lệnh:
     ```bash
     cd QuanLyDatPhong_Model4
     dotnet run
     ```
   - Giữ phím `Ctrl` và click vào đường link `http://localhost:5xxx` hiển thị trên Terminal để mở trang web.
3. **Cách bấm F5 (Run & Debug):**
   - Đã được tạo sẵn cấu hình trong `.vscode/launch.json` và `.vscode/tasks.json`.
   - Bạn chỉ cần nhấn **F5** (hoặc vào menu **Run -> Start Debugging** / **Run Without Debugging** `Ctrl + F5`) để khởi chạy trực tiếp.

### Cách 2: Chạy bằng Visual Studio (Visual Studio 2022 / 2026)
1. Mở file Solution `QuanLyDatPhong_Model4.sln`.
2. Nhấn **F5** hoặc nút **Start** để chạy.
3. Cơ sở dữ liệu và dữ liệu mẫu sẽ tự động được khởi tạo vào `(localdb)\mssqllocaldb`.

### Cách 3: Chạy bằng dòng lệnh (.NET CLI)
```bash
cd QuanLyDatPhong_Model4
dotnet run
```
Mở trình duyệt truy cập: `http://localhost:5000` hoặc địa chỉ hiển thị trên màn hình console.

---

## 5. THƯ MỤC NGUỒN CỦA DỰ ÁN
```
QuanLyDatPhong_Model4/
├── Data/
│   ├── QuanLyDatPhongDbContext.cs  # DbContext và cấu hình quan hệ thực thể
│   └── DbInitializer.cs            # Nạp dữ liệu mẫu 5 loại phòng, 30 phòng, 30 khách, 45 đặt phòng
├── Models/
│   ├── Entities/
│   │   ├── TaiKhoan.cs
│   │   ├── LoaiPhong.cs
│   │   ├── Phong.cs
│   │   ├── KhachHang.cs
│   │   ├── DatPhong.cs              # Entity trung tâm của Module 4
│   │   ├── DichVuPhatSinh.cs
│   │   └── HoaDon.cs
│   └── ViewModels/
│       └── DatPhongViewModels.cs    # ViewModel cho tìm kiếm, lọc, phân trang, công suất
├── Controllers/
│   ├── DatPhongController.cs        # Controller chính xử lý toàn bộ nghiệp vụ Module 4
│   └── TaiKhoanController.cs        # Đăng nhập, đăng xuất, chuyển nhanh vai trò
├── Views/
│   ├── DatPhong/
│   │   ├── Index.cshtml             # Danh sách tiếp nhận, tìm kiếm, lọc, phân trang
│   │   ├── Details.cshtml           # Chi tiết đặt phòng, nhật ký xử lý, dịch vụ phát sinh
│   │   ├── XacNhan.cshtml           # Màn hình kiểm tra điều kiện (LINQ) & xác nhận/từ chối
│   │   ├── NhanPhong.cshtml         # Thủ tục nhận phòng (Check-in)
│   │   ├── TraPhong.cshtml          # Thủ tục trả phòng (Check-out) & lập hóa đơn
│   │   └── KiemSoatCongSuat.cshtml  # Thống kê công suất LINQ & sơ đồ phòng thời gian thực
│   ├── TaiKhoan/
│   │   └── DangNhap.cshtml          # Đăng nhập kèm tài khoản kiểm thử nhanh
│   └── Shared/
│       └── _Layout.cshtml           # Giao diện chính, banner thông tin sinh viên, thanh chuyển vai trò
└── Program.cs                       # Cấu hình DI, DbContext, Session, Routing
```
