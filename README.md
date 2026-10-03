# 🏨 HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ

## BÀI TẬP LỚN MÔN THỰC HÀNH LẬP TRÌNH .NET

**Đề tài 14: Xây dựng hệ thống quản lý đặt phòng khách sạn và lưu trú**

- **Công nghệ:** ASP.NET Core 10 MVC
- **Cơ sở dữ liệu:** SQL Server
- **ORM:** Entity Framework Core 10
- **Ngôn ngữ:** C#
- **Nhóm:** 05 sinh viên
- **Lớp:** TI17A1HN

---

# 1. MỤC TIÊU

Xây dựng ứng dụng Web **Hệ thống quản lý đặt phòng khách sạn và lưu trú** bằng **ASP.NET Core 10 MVC**.

Hệ thống hỗ trợ quản lý xuyên suốt quá trình:

**Công bố loại phòng → Quản lý phòng → Tiếp nhận yêu cầu đặt phòng → Xác nhận → Nhận phòng → Ghi nhận dịch vụ phát sinh → Trả phòng → Thanh toán**

Hệ thống phục vụ ba nhóm người dùng chính:

### Admin

- Quản lý tài khoản.
- Quản lý loại phòng.
- Quản lý dữ liệu dùng chung.
- Quản lý toàn bộ hệ thống.

### Lễ tân

- Quản lý phòng.
- Tiếp nhận đặt phòng.
- Xác nhận hoặc từ chối đặt phòng.
- Làm thủ tục nhận phòng.
- Làm thủ tục trả phòng.
- Ghi nhận dịch vụ phát sinh.
- Lập hóa đơn.
- Dashboard.
- Thống kê.

### Khách hàng

- Đăng nhập.
- Quản lý thông tin cá nhân.
- Tìm phòng trống.
- Đặt phòng.
- Theo dõi trạng thái đặt phòng.
- Xem hóa đơn của chính mình.

---

# 2. KIẾN THỨC VÀ CÔNG NGHỆ ÁP DỤNG

Thông qua bài tập lớn, sinh viên vận dụng:

- C# và lập trình hướng đối tượng.
- ASP.NET Core 10 MVC.
- Kiến trúc Model - View - Controller.
- Entity Framework Core 10.
- Code First.
- Migration.
- CRUD.
- Model Binding.
- Data Annotation.
- Validation.
- Razor View.
- Tag Helper / HTML Helper.
- LINQ.
- Tìm kiếm.
- Lọc.
- Sắp xếp.
- Phân trang.
- Session.
- Đăng nhập.
- Phân quyền.
- Xử lý luồng trạng thái đặt phòng.
- Kiểm tra trùng khoảng thời gian lưu trú.
- Tính tiền lưu trú.
- Làm việc nhóm bằng Git/GitHub.

---

# 3. CÔNG NGHỆ SỬ DỤNG

## 3.1. Công nghệ bắt buộc

| Công nghệ | Mục đích |
|---|---|
| .NET 10 SDK | Nền tảng phát triển |
| ASP.NET Core 10 MVC | Xây dựng ứng dụng Web |
| C# | Ngôn ngữ lập trình |
| Entity Framework Core 10 | ORM và truy cập dữ liệu |
| SQL Server | Cơ sở dữ liệu |
| LINQ | Truy vấn và xử lý dữ liệu |
| Razor View | Xây dựng giao diện |
| HTML/CSS | Thiết kế giao diện |
| JavaScript | Xử lý tương tác cần thiết |
| Visual Studio / Visual Studio Code | Môi trường phát triển |
| Git/GitHub | Quản lý mã nguồn và làm việc nhóm |

## 3.2. Quy định về phiên bản

Project bắt buộc được tạo và phát triển trên **.NET 10**.

Các package Entity Framework Core phải sử dụng **phiên bản 10** và tương thích với .NET 10.

Không chấp nhận:

- ASP.NET MVC trên .NET Framework.
- ASP.NET Core phiên bản thấp hơn yêu cầu.
- Project cũ chỉ đổi tên Entity, giao diện hoặc dữ liệu để chuyển thành đề tài.
- Project chỉ có CRUD nhưng không thực hiện nghiệp vụ đặt phòng và lưu trú.
- Sinh viên không hiểu hoặc không giải thích được mã nguồn thuộc phần mình thực hiện.

---

# 4. QUY MÔ VÀ TỔ CHỨC NHÓM

Mỗi nhóm gồm **05 sinh viên**.

Mỗi nhóm sử dụng:

- 01 Project ASP.NET Core 10 MVC.
- 01 cơ sở dữ liệu SQL Server dùng chung.
- 01 Repository GitHub chung.

Mỗi sinh viên chịu trách nhiệm chính về **01 Module**.

Các thành viên phải phối hợp thống nhất:

- Entity.
- Quan hệ dữ liệu.
- Trạng thái nghiệp vụ.
- Giao diện điều hướng.
- Quy tắc phân quyền.

---

# 5. QUY ĐỊNH ĐẶT TÊN PROJECT

Tên Project được đặt theo cấu trúc:

`[TênĐềTài]_UNETI[STTNhóm]_[MãLớp]`

Ví dụ:

`QuanLyDatPhong_UNETI14_TI17A1HN`

Trong đó:

- `QuanLyDatPhong`: tên đề tài.
- `UNETI14`: số thứ tự nhóm.
- `TI17A1HN`: mã lớp.

Tên Project, Solution và Namespace phải thống nhất trong toàn nhóm.

---

# 6. PHÂN CHIA CHỨC NĂNG CHO 05 SINH VIÊN

| Sinh viên | Module | Nội dung |
|---|---|---|
| SV1 | Module 1 | Tài khoản - Đăng nhập - Phân quyền - Quản lý loại phòng |
| SV2 | Module 2 | Quản lý phòng - Tìm kiếm - Lọc - Sắp xếp - Phân trang |
| SV3 | Module 3 | Quản lý khách hàng - Hồ sơ cá nhân - Đặt phòng - Theo dõi đặt phòng |
| SV4 | Module 4 | Tiếp nhận đặt phòng - Xác nhận/Từ chối - Nhận phòng - Trả phòng - Quản lý trạng thái lưu trú |
| SV5 | Module 5 | Dịch vụ phát sinh - Hóa đơn thanh toán - Dashboard - Thống kê |

Việc phân chia trên xác định trách nhiệm chính.

Mỗi thành viên vẫn phải hiểu:

- Cấu trúc chung.
- Cơ sở dữ liệu.
- Luồng nghiệp vụ.
- Module mình phụ trách.
- Các chức năng liên quan.

---

# 7. MODULE 1 - TÀI KHOẢN, ĐĂNG NHẬP, PHÂN QUYỀN VÀ LOẠI PHÒNG

## 7.1. Quản lý tài khoản

Entity `TaiKhoan` gồm tối thiểu:

- `MaTaiKhoan`
- `TenDangNhap`
- `MatKhau`
- `HoTen`
- `Email`
- `VaiTro`
- `TrangThai`

Hệ thống có ba vai trò:

- Admin
- Lễ tân
- Khách hàng

Validation:

- Tên đăng nhập bắt buộc.
- Tên đăng nhập không được trùng.
- Mật khẩu bắt buộc.
- Email đúng định dạng.
- Tài khoản bị khóa không được đăng nhập.

---

## 7.2. Đăng nhập

Người dùng nhập:

- Tên đăng nhập.
- Mật khẩu.

Hệ thống sử dụng Entity Framework Core và LINQ để kiểm tra tài khoản trong cơ sở dữ liệu.

Khi đăng nhập thành công, lưu tối thiểu vào Session:

- Mã tài khoản.
- Họ tên.
- Vai trò.

Nếu:

- Tài khoản không tồn tại.
- Sai mật khẩu.
- Tài khoản bị khóa.

thì hệ thống phải hiển thị thông báo phù hợp.

---

## 7.3. Đăng xuất

Khi đăng xuất:

- Xóa thông tin đăng nhập khỏi Session.
- Chuyển về trang chủ hoặc trang đăng nhập.
- Không cho phép tiếp tục truy cập chức năng yêu cầu đăng nhập bằng Session cũ.

---

## 7.4. Phân quyền

### Admin

Được phép:

- Quản lý tài khoản.
- Quản lý loại phòng.
- Truy cập các chức năng quản trị.

### Lễ tân

Được phép:

- Quản lý phòng.
- Quản lý đặt phòng.
- Nhận phòng.
- Trả phòng.
- Quản lý dịch vụ phát sinh.
- Quản lý hóa đơn.
- Thống kê.

### Khách hàng

Chỉ được thao tác với:

- Thông tin của chính mình.
- Đặt phòng của chính mình.
- Hóa đơn của chính mình.

Các chức năng quản trị phải kiểm tra quyền tại **Controller** trước khi xử lý.

Không được chỉ ẩn menu hoặc nút trên View.

---

## 7.5. Quản lý loại phòng

Entity `LoaiPhong` gồm:

- `MaLoaiPhong`
- `TenLoaiPhong`
- `MoTa`
- `SucChuaToiDa`
- `GiaThamKhao`
- `TrangThai`

Admin thực hiện:

- Xem danh sách.
- Xem chi tiết.
- Thêm.
- Sửa.
- Xóa.
- Cập nhật trạng thái.

Validation:

- Tên loại phòng bắt buộc.
- Tên loại phòng không được trùng.
- Sức chứa tối đa > 0.
- Giá tham khảo >= 0.
- Mô tả sử dụng textarea.
- Không được xóa loại phòng nếu việc xóa làm các phòng liên quan không còn hợp lệ.

---

# 8. MODULE 2 - QUẢN LÝ PHÒNG

## 8.1. Entity Phong

Entity `Phong` gồm:

- `MaPhong`
- `SoPhong`
- `MaLoaiPhong`
- `Tang`
- `SucChua`
- `GiaMotDem`
- `DienTich`
- `TienNghi`
- `MoTa`
- `TrangThai`

Trạng thái phòng:

- Sẵn sàng.
- Đang bảo trì.
- Ngừng kinh doanh.

---

## 8.2. CRUD và Validation

Lễ tân/Admin thực hiện:

- Danh sách.
- Chi tiết.
- Thêm.
- Sửa.
- Cập nhật trạng thái.

Validation:

- Loại phòng sử dụng `select` lấy dữ liệu từ Database.
- Số phòng bắt buộc.
- Số phòng không được trùng.
- Tầng >= 1.
- Sức chứa > 0.
- Sức chứa không vượt quá sức chứa tối đa của loại phòng.
- Giá một đêm > 0.
- Tiện nghi sử dụng textarea.
- Mô tả sử dụng textarea.

Không cho chuyển phòng sang:

- Đang bảo trì.
- Ngừng kinh doanh.

nếu phòng còn đặt phòng đang hiệu lực:

- Đã xác nhận.
- Đã nhận phòng.

Khi phòng đã có lịch sử đặt phòng, việc xóa phải được kiểm soát để không làm mất dữ liệu lịch sử.

---

## 8.3. Tìm kiếm

Cho phép tìm kiếm bằng LINQ theo:

- Số phòng.
- Tên loại phòng.

---

## 8.4. Lọc

Có thể lọc theo:

- Loại phòng.
- Tầng.
- Sức chứa tối thiểu.
- Khoảng giá một đêm.
- Trạng thái phòng.
- Còn trống/Đã có người đặt trong khoảng ngày nhận phòng - ngày trả phòng.

Các điều kiện lọc phải có khả năng kết hợp với nhau.

---

## 8.5. Sắp xếp

Hỗ trợ:

- Số phòng tăng dần.
- Số phòng giảm dần.
- Giá một đêm tăng dần.
- Giá một đêm giảm dần.
- Sức chứa tăng dần.
- Sức chứa giảm dần.

---

## 8.6. Phân trang

Danh sách phòng bắt buộc phải phân trang.

Có:

- Trang trước.
- Trang sau.
- Số trang.
- Trang hiện tại.

Phải xử lý đúng:

- Trang đầu.
- Trang giữa.
- Trang cuối.

Tìm kiếm + Lọc + Sắp xếp + Phân trang phải hoạt động kết hợp.

Khi chuyển trang phải giữ lại:

- Từ khóa.
- Bộ lọc.
- Khoảng ngày lưu trú.
- Kiểu sắp xếp.

Quy trình:

**Truy vấn dữ liệu → Tìm kiếm → Lọc → Sắp xếp → Phân trang → Hiển thị**

Phân trang thực hiện trên truy vấn EF Core/LINQ bằng `Skip()` và `Take()` hoặc cách tương đương.

Không tải toàn bộ dữ liệu về trình duyệt rồi chỉ ẩn/hiện dữ liệu.

---

## 8.7. Hiển thị phòng cho khách hàng

Khách hàng chỉ được đặt phòng thỏa mãn đồng thời:

- Trạng thái phòng là Sẵn sàng.
- Ngày nhận phòng không nằm trong quá khứ.
- Ngày trả phòng sau ngày nhận phòng.
- Phòng không bị đặt trùng trong khoảng ngày đã chọn.

Hệ thống phải hiển thị rõ:

- Còn trống.
- Đã có người đặt.

---

# 9. MODULE 3 - QUẢN LÝ KHÁCH HÀNG VÀ ĐẶT PHÒNG

## 9.1. Entity KhachHang

Entity `KhachHang` gồm:

- `MaKhachHang`
- `MaTaiKhoan`
- `HoTen`
- `NgaySinh`
- `GioiTinh`
- `SoDienThoai`
- `Email`
- `DiaChi`
- `SoGiayToTuyThan`
- `QuocTich`
- `TrangThai`

Validation:

- Họ tên bắt buộc.
- Ngày sinh hợp lệ.
- Email đúng định dạng.
- Số điện thoại hợp lệ.
- Số giấy tờ tùy thân bắt buộc khi đặt phòng.

Khách hàng chỉ được cập nhật thông tin cá nhân của chính mình.

---

## 9.2. Entity DatPhong

Entity `DatPhong` gồm:

- `MaDatPhong`
- `MaKhachHang`
- `MaPhong`
- `NgayNhanPhong`
- `NgayTraPhong`
- `SoNguoi`
- `YeuCauDacBiet`
- `NgayDat`
- `TongTienDuKien`
- `NgayNhanThucTe`
- `NgayTraThucTe`
- `TrangThai`
- `NgayXuLy`
- `GhiChuLeTan`

Trạng thái đặt phòng:

- Chờ xác nhận.
- Đã xác nhận.
- Đã nhận phòng.
- Đã trả phòng.
- Từ chối.
- Đã hủy.

---

## 9.3. Đặt phòng

Khách hàng phải đăng nhập trước khi đặt phòng.

Người dùng chọn:

- Phòng.
- Khoảng ngày lưu trú.
- Số người.
- Yêu cầu đặc biệt.

Trước khi tạo đặt phòng, hệ thống phải kiểm tra:

- Khách hàng tồn tại và đang hoạt động.
- Phòng tồn tại và ở trạng thái Sẵn sàng.
- Ngày nhận phòng không nằm trong quá khứ.
- Ngày trả phòng sau ngày nhận phòng.
- Số người không vượt quá sức chứa phòng.
- Phòng không có đặt phòng khác đang hiệu lực.
- Thông tin bắt buộc của khách hàng đã được khai báo.

Các trạng thái đặt phòng đang hiệu lực:

- Chờ xác nhận.
- Đã xác nhận.
- Đã nhận phòng.

---

## 9.4. Kiểm tra trùng lịch

Hai khoảng lưu trú được xem là trùng khi:

**Ngày nhận của khoảng A < Ngày trả của khoảng B**

và

**Ngày trả của khoảng A > Ngày nhận của khoảng B**

Vì vậy:

**Ngày trả phòng của khách trước có thể trùng với ngày nhận phòng của khách sau.**

Ví dụ:

- Khách A: 01/10 → 05/10
- Khách B: 05/10 → 08/10

Hai khoảng trên có thể nối tiếp nhau.

---

## 9.5. Tính tổng tiền dự kiến

`NgayDat` và `TongTienDuKien` do hệ thống xác định.

Khách hàng không được tự nhập.

Công thức:

**Số đêm = Ngày trả phòng - Ngày nhận phòng**

**Tổng tiền dự kiến = Số đêm × Giá một đêm**

Đặt phòng mới tạo có trạng thái:

**Chờ xác nhận**

---

## 9.6. Hủy đặt phòng

Khách hàng chỉ được hủy:

- Đặt phòng của chính mình.
- Đặt phòng ở trạng thái cho phép hủy.

Ví dụ:

- Chờ xác nhận.
- Đã xác nhận nhưng chưa đến ngày nhận phòng.

Không được tùy ý hủy:

- Đã nhận phòng.
- Đã trả phòng.

---

## 9.7. Theo dõi đặt phòng

Khách hàng được xem:

- Danh sách đặt phòng đã thực hiện.
- Phòng đã đặt.
- Loại phòng.
- Ngày nhận phòng.
- Ngày trả phòng.
- Số đêm.
- Tổng tiền dự kiến.
- Trạng thái hiện tại.
- Hóa đơn khi đã được lập.

Khách hàng chỉ được xem dữ liệu của chính mình.

Việc thay đổi mã trên URL không được làm lộ dữ liệu của khách hàng khác.

---

# 10. MODULE 4 - TIẾP NHẬN, XÁC NHẬN VÀ QUẢN LÝ TRẠNG THÁI LƯU TRÚ

## 10.1. Danh sách đặt phòng cần xử lý

Lễ tân có thể:

- Tìm theo họ tên khách hàng.
- Tìm theo số phòng.
- Lọc theo loại phòng.
- Lọc theo trạng thái đặt phòng.
- Lọc theo ngày nhận phòng.
- Sắp xếp theo ngày đặt.
- Sắp xếp theo ngày nhận phòng.
- Xem chi tiết đặt phòng.

---

## 10.2. Xác nhận đặt phòng

Chỉ đặt phòng ở trạng thái:

**Chờ xác nhận**

mới được chuyển sang:

- Đã xác nhận.
- Từ chối.

Khi xử lý phải ghi:

- `NgayXuLy`
- `GhiChuLeTan`

Khách hàng không được tự thay đổi các trường này.

---

## 10.3. Kiểm tra trước khi xác nhận

Hệ thống phải kiểm tra:

- Đặt phòng tồn tại.
- Đặt phòng chưa bị hủy.
- Phòng tồn tại.
- Phòng vẫn ở trạng thái Sẵn sàng.
- Khách hàng còn hoạt động.
- Đặt phòng chưa có kết quả xử lý trước đó.
- Không có đặt phòng khác đã xác nhận hoặc đã nhận phòng trùng khoảng ngày lưu trú.

Việc xác nhận hoặc từ chối do Lễ tân quyết định.

Hệ thống chỉ cung cấp thông tin kiểm tra và không tự suy diễn kết quả.

---

## 10.4. Quản lý luồng trạng thái

Luồng cơ bản:

**Chờ xác nhận → Đã xác nhận → Đã nhận phòng → Đã trả phòng**

Các nhánh khác:

**Chờ xác nhận → Từ chối**

hoặc:

**Trạng thái phù hợp → Đã hủy**

Chỉ được nhận phòng khi:

- Đặt phòng đã được xác nhận.
- Đã đến ngày nhận phòng.

Khi nhận phòng:

- Ghi `NgayNhanThucTe`.

Khi trả phòng:

- Ghi `NgayTraThucTe`.

Không cho:

- Chuyển trực tiếp từ Chờ xác nhận sang Đã nhận phòng.
- Đặt phòng Từ chối làm thủ tục nhận phòng.
- Sửa đặt phòng đã Đã trả phòng.

---

## 10.5. Kiểm soát công suất phòng

Hệ thống sử dụng LINQ để:

- Đếm số phòng đã có đặt phòng đang hiệu lực.
- Kiểm tra theo từng loại phòng.
- Kiểm tra trong khoảng ngày được chọn.

Khi tất cả phòng Sẵn sàng của một loại phòng đã kín lịch:

- Cảnh báo Lễ tân.
- Không cho xác nhận thêm đặt phòng cho loại phòng đó trong khoảng ngày này.

Đặt phòng Đã xác nhận nhưng quá ngày nhận phòng mà khách chưa nhận phòng phải được đánh dấu cảnh báo để Lễ tân quyết định hủy.

Không được tự động xóa hoặc làm mất các đặt phòng còn lại.

---

# 11. MODULE 5 - DỊCH VỤ PHÁT SINH, HÓA ĐƠN, DASHBOARD VÀ THỐNG KÊ

## 11.1. Entity DichVuPhatSinh

Entity `DichVuPhatSinh` gồm:

- `MaDichVuPhatSinh`
- `MaDatPhong`
- `TenDichVu`
- `SoLuong`
- `DonGia`
- `NgaySuDung`
- `GhiChu`
- `TrangThai`

Trạng thái:

- Đã ghi nhận.
- Đã hủy.

---

## 11.2. Ghi nhận dịch vụ phát sinh

Chỉ đặt phòng:

**Đã nhận phòng**

mới được ghi nhận dịch vụ phát sinh.

Ví dụ:

- Ăn uống.
- Giặt là.
- Đưa đón.
- Các dịch vụ khác.

Kiểm tra:

- Tên dịch vụ bắt buộc.
- Số lượng > 0.
- Đơn giá >= 0.
- Ngày sử dụng nằm trong thời gian lưu trú thực tế.
- Đặt phòng chưa Đã trả phòng.
- Đặt phòng chưa bị hủy.

Thành tiền:

**Thành tiền = Số lượng × Đơn giá**

---

# 12. TRẢ PHÒNG VÀ LẬP HÓA ĐƠN

## 12.1. Trả phòng

Khi khách trả phòng:

- Đặt phòng chuyển sang `Đã trả phòng`.
- Ghi `NgayTraThucTe`.
- Lập hóa đơn.

Không lập hóa đơn nếu:

- Đặt phòng chưa Đã nhận phòng.
- Đã có hóa đơn.

Số đêm được tính theo:

- Ngày nhận thực tế.
- Ngày trả thực tế.

Số đêm tối thiểu là 1 đêm.

Sau khi hóa đơn được lập:

**Không được ghi thêm dịch vụ phát sinh cho đặt phòng đó.**

---

## 12.2. Entity HoaDon

Entity `HoaDon` gồm:

- `MaHoaDon`
- `MaDatPhong`
- `SoDem`
- `TienPhong`
- `TienDichVu`
- `GiamGia`
- `TongThanhToan`
- `HinhThucThanhToan`
- `NgayLap`
- `TrangThai`

Trạng thái hóa đơn:

- Chưa thanh toán.
- Đã thanh toán.

Hình thức thanh toán:

- Tiền mặt.
- Chuyển khoản.
- Thẻ.

---

## 12.3. Tính tiền hóa đơn

### Tiền phòng

**Tiền phòng = Số đêm × Giá phòng**

### Tiền dịch vụ

**Tiền dịch vụ = Tổng các dịch vụ chưa hủy**

### Tổng thanh toán

**Tổng thanh toán = Tiền phòng + Tiền dịch vụ - Giảm giá**

`TienPhong`, `TienDichVu` và `TongThanhToan` do hệ thống tính.

`NgayLap` do hệ thống xác định.

Giảm giá:

- >= 0.
- Không vượt quá tổng tiền phòng và dịch vụ.

Hóa đơn Đã thanh toán không được sửa tùy ý.

Mỗi đặt phòng tối đa có một hóa đơn.

---

# 13. DASHBOARD

Dashboard hiển thị:

- Tổng số loại phòng.
- Tổng số phòng.
- Số phòng Sẵn sàng.
- Tổng số khách hàng.
- Tổng số đặt phòng.
- Số đặt phòng Chờ xác nhận.
- Số khách đang lưu trú.
- Số đặt phòng sắp đến ngày nhận trong 07 ngày tới.
- Doanh thu tháng hiện tại từ các hóa đơn Đã thanh toán.

Không bắt buộc sử dụng biểu đồ.

---

# 14. THỐNG KÊ BẰNG LINQ

Hệ thống thống kê:

- Số phòng theo loại phòng.
- Số đặt phòng theo từng phòng.
- Phòng được đặt nhiều nhất.
- Doanh thu theo tháng.
- Doanh thu theo loại phòng.
- Doanh thu dịch vụ phát sinh theo tên dịch vụ.
- Số khách trả phòng theo tháng.
- Tỷ lệ lấp đầy phòng theo tháng.
- Tỷ lệ đặt phòng bị hủy hoặc từ chối trên tổng số đặt phòng.

Các phương thức LINQ phù hợp:

- `Count()`
- `Sum()`
- `GroupBy()`
- `OrderBy()`
- `OrderByDescending()`
- `Any()`

Sinh viên phải giải thích được truy vấn và ý nghĩa kết quả.

---

# 15. THIẾT KẾ CƠ SỞ DỮ LIỆU

Các Entity chính:

- `TaiKhoan`
- `LoaiPhong`
- `Phong`
- `KhachHang`
- `DatPhong`
- `DichVuPhatSinh`
- `HoaDon`

## 15.1. Quan hệ dữ liệu

| Entity | Quan hệ | Entity |
|---|---|---|
| TaiKhoan | 1 - 0..1 | KhachHang |
| LoaiPhong | 1 - n | Phong |
| KhachHang | 1 - n | DatPhong |
| Phong | 1 - n | DatPhong |
| DatPhong | 1 - 0..n | DichVuPhatSinh |
| DatPhong | 1 - 0..1 | HoaDon |

Sinh viên phải xác định:

- Primary Key.
- Foreign Key.
- Navigation Property.
- Kiểu dữ liệu.
- Required / Nullable.
- Độ dài chuỗi.
- Quan hệ giữa các Entity.
- Ràng buộc nghiệp vụ.

Có thể bổ sung:

- `DichVu`
- `NhanVienLeTan`

nhưng không được làm giảm các chức năng bắt buộc.

---

# 16. ENTITY FRAMEWORK CORE VÀ MIGRATION

Cơ sở dữ liệu được xây dựng bằng **Entity Framework Core 10 - Code First**.

Quy trình:

**Entity → DbContext → Migration → SQL Server**

Các công việc:

1. Xây dựng Entity.
2. Xây dựng DbContext.
3. Khai báo DbSet.
4. Cấu hình Connection String SQL Server.
5. Tạo Migration.
6. Cập nhật Database.
7. Truy xuất dữ liệu bằng Entity Framework Core và LINQ.

---

# 17. QUY ĐỊNH MIGRATION KHI LÀM NHÓM

Các thành viên phải:

- Thống nhất thay đổi Entity trước khi tạo Migration.
- Migration phải có tên rõ nghĩa.
- Commit Migration lên Repository.
- Không tự ý thay đổi cùng một Entity gây xung đột Database.

Không sử dụng SQL viết trực tiếp để thay thế các chức năng CRUD bắt buộc.

---

# 18. YÊU CẦU ASP.NET CORE MVC

Ứng dụng phải tuân thủ kiến trúc MVC.

Các Controller có thể gồm:

- `TaiKhoanController`
- `LoaiPhongController`
- `PhongController`
- `KhachHangController`
- `DatPhongController`
- `DichVuPhatSinhController`
- `HoaDonController`
- `ThongKeController`

Không xây dựng toàn bộ hệ thống trong một Controller.

Mỗi Controller phải có phạm vi chức năng hợp lý.

Phải sử dụng phù hợp:

- Model / Entity.
- Controller.
- Action.
- View.
- ViewModel.
- Model Binding.
- Data Annotation.
- Validation.
- Razor Syntax.
- Tag Helper / HTML Helper.
- Entity Framework Core.
- LINQ.

---

# 19. VALIDATION VÀ XỬ LÝ NGHIỆP VỤ

Có thể sử dụng các Data Annotation:

- `[Required]`
- `[StringLength]`
- `[Range]`
- `[EmailAddress]`
- `[Phone]`
- `[Display]`

Các validation bắt buộc:

- Tên đăng nhập không trùng.
- Tên loại phòng không trùng.
- Số phòng không trùng.
- Sức chứa > 0.
- Giá một đêm > 0.
- Ngày trả phòng sau ngày nhận phòng.
- Ngày nhận phòng không nằm trong quá khứ khi tạo đặt phòng.
- Số người không vượt quá sức chứa.
- Email hợp lệ.
- Số điện thoại hợp lệ.
- Không đặt phòng trùng.
- Không đặt phòng đang bảo trì.
- Không đặt phòng ngừng kinh doanh.
- Chỉ đặt phòng Chờ xác nhận mới được xác nhận hoặc từ chối.
- Chỉ đặt phòng Đã xác nhận mới được nhận phòng.
- Chỉ đặt phòng Đã nhận phòng mới được ghi dịch vụ.
- Chỉ đặt phòng Đã nhận phòng mới được trả phòng.
- Số lượng dịch vụ > 0.
- Mỗi đặt phòng tối đa một hóa đơn.
- Hóa đơn chỉ lập sau khi trả phòng.
- Giảm giá không vượt quá tổng tiền.
- Khách hàng chỉ thao tác với dữ liệu của chính mình.

Các lỗi Validation phải hiển thị rõ trên View.

Quy tắc nghiệp vụ không phù hợp với Data Annotation phải được kiểm tra tại Controller hoặc lớp xử lý nghiệp vụ.

---

# 20. YÊU CẦU VỀ RAZOR VIEW

Không giữ nguyên hoàn toàn View do Scaffolding sinh ra.

Giao diện phải được chỉnh sửa phù hợp với nghiệp vụ.

Yêu cầu:

- Ngày nhận phòng sử dụng `type="date"` khi phù hợp.
- Ngày trả phòng sử dụng `type="date"` khi phù hợp.
- Ngày sử dụng dịch vụ sử dụng `type="date"` khi phù hợp.
- Yêu cầu đặc biệt sử dụng textarea.
- Mô tả phòng sử dụng textarea.
- Tiện nghi sử dụng textarea.
- Ghi chú sử dụng textarea.
- Loại phòng sử dụng select lấy dữ liệu từ Database.
- Phòng sử dụng select lấy dữ liệu từ Database.
- Giới tính sử dụng radio hoặc select.
- Hình thức thanh toán sử dụng select.
- Trạng thái sử dụng kiểu nhập liệu phù hợp.
- Không cho người dùng tự chọn trạng thái trái nghiệp vụ.
- Có Validation Message.
- Hiển thị số phòng thay cho chỉ mã phòng.
- Hiển thị tên khách hàng thay cho chỉ mã khách hàng.
- Hiển thị tên loại phòng thay cho chỉ mã loại phòng.
- Định dạng ngày rõ ràng.
- Định dạng tiền rõ ràng.
- Hiển thị đơn vị đồng.

Các nút thao tác phải phù hợp với trạng thái.

Ví dụ:

- Đặt phòng Từ chối không hiển thị chức năng nhận phòng.
- Đặt phòng chưa nhận phòng không hiển thị chức năng ghi dịch vụ.
- Đặt phòng chưa nhận phòng không hiển thị chức năng trả phòng.

---

# 21. TÌM KIẾM, LỌC, SẮP XẾP, PHÂN TRANG VÀ LINQ

Đây là yêu cầu bắt buộc.

Danh sách phòng phải thực hiện đầy đủ:

**Tìm kiếm + Lọc + Sắp xếp + Phân trang**

Khi chuyển trang phải giữ:

- Từ khóa tìm kiếm.
- Loại phòng.
- Tầng.
- Sức chứa tối thiểu.
- Khoảng giá.
- Trạng thái.
- Khoảng ngày nhận phòng - ngày trả phòng.
- Kiểu sắp xếp.

LINQ bắt buộc được sử dụng cho:

- Kiểm tra đặt phòng trùng.
- Kiểm tra phòng còn trống.
- Tìm kiếm đặt phòng.
- Lọc đặt phòng.
- Đếm công suất phòng.
- Tính số đêm.
- Tính tiền phòng.
- Tính tiền dịch vụ.
- Kiểm tra dịch vụ.
- Kiểm tra hóa đơn.
- Dashboard.
- Thống kê.

Không tải toàn bộ dữ liệu rồi dùng JavaScript để giả lập phân trang.

---

# 22. DỮ LIỆU MẪU

Tối thiểu:

- 05 loại phòng.
- 30 phòng.
- Phòng Sẵn sàng.
- Phòng Đang bảo trì.
- Phòng Ngừng kinh doanh.
- 30 khách hàng.
- 01-02 tài khoản Admin.
- 02-03 tài khoản Lễ tân.
- Tài khoản khách hàng tương ứng.
- 40-50 đặt phòng.
- Nhiều trạng thái đặt phòng.
- Các trường hợp đặt phòng gối đầu ngày nhận/trả.
- 20-30 dịch vụ phát sinh.
- 15-20 hóa đơn.
- Hóa đơn Đã thanh toán.
- Hóa đơn Chưa thanh toán.

Dữ liệu phải đủ đa dạng để kiểm tra:

- CRUD.
- Validation.
- Search.
- Filter.
- Sort.
- Pagination.
- Đăng nhập.
- Phân quyền.
- Đặt phòng.
- Xác nhận.
- Trùng lịch.
- Nhận phòng.
- Trả phòng.
- Hóa đơn.
- Dashboard.
- Thống kê.

Danh sách phòng phải đủ dữ liệu để tạo nhiều trang khi kiểm tra Pagination.

---

# 23. YÊU CẦU GIAO DIỆN

## 23.1. Phía khách hàng

- Trang chủ.
- Đăng nhập.
- Đăng xuất.
- Thông tin cá nhân.
- Danh sách phòng.
- Tìm kiếm.
- Lọc theo khoảng ngày.
- Sắp xếp.
- Phân trang.
- Chi tiết phòng.
- Đặt phòng.
- Danh sách đặt phòng của tôi.
- Theo dõi trạng thái đặt phòng.
- Xem hóa đơn.

## 23.2. Phía Lễ tân/Admin

- Trang quản trị.
- Quản lý loại phòng.
- Quản lý phòng.
- Quản lý khách hàng.
- Quản lý đặt phòng.
- Xác nhận đặt phòng.
- Từ chối đặt phòng.
- Nhận phòng.
- Trả phòng.
- Ghi nhận dịch vụ phát sinh.
- Lập hóa đơn.
- Quản lý hóa đơn.
- Dashboard.
- Thống kê.

Không đặt nặng thiết kế đồ họa.

Trọng tâm là:

- Chức năng.
- Tính đúng đắn nghiệp vụ.
- Kỹ thuật ASP.NET Core MVC.

---

# 24. QUY ĐỊNH GHI THÔNG TIN NGƯỜI THỰC HIỆN TRONG MÃ NGUỒN

Mỗi sinh viên phải ghi:

- Họ và tên.
- Mã sinh viên.
- Nội dung thực hiện.

Thông tin được ghi tại đầu các file mã nguồn do mình phụ trách.

Áp dụng đối với:

- Model / Entity.
- Controller.
- ViewModel.
- View.
- Các lớp xử lý khác nếu có.

Nếu file có nhiều sinh viên cùng thực hiện, phải ghi rõ:

- Họ tên.
- Mã sinh viên.
- Nội dung cụ thể của từng người.

Thông tin trong mã nguồn phải phù hợp với:

- Module được phân công.
- Bảng phân công công việc.
- Lịch sử Commit/Push trên GitHub.
- Nội dung sinh viên trực tiếp bảo vệ.

Không ghi tên sinh viên vào file nếu sinh viên đó không trực tiếp tham gia thực hiện phần mã nguồn tương ứng.

---

# 25. CHỨC NĂNG NÂNG CAO

Các chức năng sau **không bắt buộc**:

- Upload ảnh phòng.
- Gửi email xác nhận đặt phòng.
- Thanh toán trực tuyến giả lập.
- Biểu đồ Chart.js.
- AJAX kiểm tra phòng trống.
- Xuất Excel.
- Xuất PDF hóa đơn.
- Lịch đặt phòng theo từng phòng.
- Mã hóa mật khẩu.
- ASP.NET Core Identity.

Chức năng nâng cao không được thay thế chức năng bắt buộc còn thiếu.

---

# 26. QUY ĐỊNH SỬ DỤNG AI

Sinh viên được phép sử dụng AI để hỗ trợ:

- Tìm hiểu kiến thức.
- Tham khảo thiết kế.
- Tham khảo mã nguồn.
- Tìm và sửa lỗi.
- Tham khảo truy vấn LINQ.
- Hỗ trợ xây dựng giao diện.

Sinh viên phải:

- Hiểu mã nguồn.
- Giải thích được mã nguồn.
- Chỉnh sửa được mã nguồn.
- Chịu trách nhiệm đối với phần đã sử dụng.

Việc sử dụng AI không thay thế yêu cầu báo cáo, bảo vệ và đánh giá trực tiếp.

---

# 27. LÀM VIỆC NHÓM VỚI GITHUB

Mỗi nhóm sử dụng:

- 01 Repository GitHub chung.
- Mỗi thành viên sử dụng tài khoản GitHub cá nhân.
- Mỗi thành viên phải có lịch sử Commit thể hiện phần việc thực tế.

## 27.1. Mẫu Commit

`[Mã SV] [Module] Nội dung công việc`

Ví dụ:

`[22103100001] [Phong] Them chuc nang phan trang`

## 27.2. Quy trình làm việc

**Thiết kế CSDL → Thống nhất Entity → Tạo Project → DbContext/Migration → Phân chia 05 Module → Code → Commit/Push → Pull/Tích hợp → Kiểm thử → Hoàn thiện**

Mỗi thành viên phải có các Commit phù hợp với tiến độ.

Không dồn toàn bộ công việc vào một Commit duy nhất ở cuối.

Thông tin:

**Họ tên + Mã sinh viên + Nội dung thực hiện trong mã nguồn**

phải có khả năng đối chiếu với:

**Lịch sử GitHub**

Không chấp nhận một thành viên thực hiện toàn bộ Project rồi chia mã nguồn cho các thành viên còn lại.

---

# 28. KIỂM THỬ VÀ HOÀN THIỆN

## 28.1. Tài khoản và phân quyền

Kiểm thử:

- Đăng nhập đúng.
- Sai mật khẩu.
- Tài khoản không tồn tại.
- Tài khoản bị khóa.
- Đăng xuất.
- Khách hàng truy cập URL quản trị.
- Phân quyền tại Controller.

## 28.2. CRUD và Validation

Kiểm thử:

- Create.
- Edit.
- Details.
- Delete.
- Dữ liệu hợp lệ.
- Dữ liệu không hợp lệ.
- Tên đăng nhập trùng.
- Loại phòng trùng.
- Số phòng trùng.
- Sức chứa không hợp lệ.
- Giá phòng không hợp lệ.
- Khoảng ngày lưu trú không hợp lệ.
- Validation Message.

## 28.3. Tìm kiếm, lọc, sắp xếp và phân trang

Kiểm thử:

- Tìm kiếm có kết quả.
- Tìm kiếm không có kết quả.
- Lọc từng điều kiện.
- Lọc nhiều điều kiện.
- Lọc phòng trống theo khoảng ngày.
- Sắp xếp tăng.
- Sắp xếp giảm.
- Trang đầu.
- Trang giữa.
- Trang cuối.
- Giữ điều kiện khi chuyển trang.

## 28.4. Đặt phòng

Kiểm thử:

- Đặt phòng hợp lệ.
- Đặt phòng đang bảo trì.
- Đặt phòng ngừng kinh doanh.
- Đặt phòng trùng.
- Đặt phòng gối đầu.
- Số người vượt sức chứa.
- Hủy đặt phòng hợp lệ.
- Hủy đặt phòng không hợp lệ.
- Xác nhận đặt phòng.
- Từ chối đặt phòng.
- Không cho đặt phòng Từ chối nhận phòng.

## 28.5. Nhận phòng, trả phòng và công suất

Kiểm thử:

- Nhận phòng hợp lệ.
- Nhận phòng trước ngày.
- Nhận phòng khi chưa xác nhận.
- Trả phòng hợp lệ.
- Loại phòng kín lịch.
- Đặt phòng quá ngày nhận nhưng chưa nhận phòng.

## 28.6. Dịch vụ và hóa đơn

Kiểm thử:

- Ghi nhận dịch vụ khi khách đang lưu trú.
- Không ghi dịch vụ khi chưa nhận phòng.
- Không ghi dịch vụ khi đã trả phòng.
- Số lượng dịch vụ không hợp lệ.
- Đơn giá dịch vụ không hợp lệ.
- Lập hóa đơn sau khi trả phòng.
- Không lập hóa đơn thứ hai.
- Giảm giá vượt tổng tiền.
- Tính đúng số đêm.
- Tính đúng tiền phòng.
- Tính đúng tiền dịch vụ.

## 28.7. Kiểm thử quyền dữ liệu

Khách hàng:

- Chỉ xem thông tin cá nhân của mình.
- Chỉ sửa thông tin cá nhân của mình.
- Chỉ xem đặt phòng của mình.
- Chỉ xem hóa đơn của mình.

Thay đổi mã trên URL không được truy cập dữ liệu của khách hàng khác.

## 28.8. Dashboard và thống kê

Đối chiếu kết quả LINQ với dữ liệu thực tế trong Database.

Các số liệu phải thay đổi đúng khi:

- Thêm đặt phòng.
- Cập nhật trạng thái.
- Hủy đặt phòng.
- Thêm hóa đơn.
- Thanh toán hóa đơn.

---

# 29. SẢN PHẨM CẦN NỘP

Mỗi nhóm cần nộp:

- Source Code hoàn chỉnh.
- Repository GitHub.
- Migration.
- Database hoặc hướng dẫn tạo Database từ Migration.
- Dữ liệu mẫu.
- Tài khoản kiểm thử theo từng vai trò.
- Báo cáo bài tập lớn.
- Thiết kế cơ sở dữ liệu.
- Bảng phân công công việc cho 05 thành viên.
- Hướng dẫn cài đặt và chạy chương trình.
- Video minh chứng quá trình thực hiện và sản phẩm.

---

# 30. NỘI DUNG BÁO CÁO

Báo cáo phải thể hiện:

- Danh sách thành viên.
- Mã sinh viên.
- Phân công 05 Module.
- Phân tích bài toán.
- Luồng đặt phòng.
- Luồng lưu trú.
- Thiết kế CSDL.
- Entity.
- Quan hệ giữa các Entity.
- Các chức năng chính.
- Mã nguồn quan trọng.
- Truy vấn LINQ.
- Search.
- Filter.
- Sort.
- Pagination.
- Kiểm tra đặt phòng.
- Xác nhận đặt phòng.
- Kiểm tra trùng lịch.
- Nhận phòng.
- Trả phòng.
- Hóa đơn.
- Giao diện.
- Kết quả.
- Quá trình GitHub.
- Bảng tổng hợp file/chức năng do từng sinh viên phụ trách.

---

# 31. BẢO VỆ VÀ ĐÁNH GIÁ CÁ NHÂN

Mỗi sinh viên phải trực tiếp trình bày và giải thích phần chức năng mình phụ trách.

Sinh viên phải hiểu:

- MVC.
- Entity.
- Primary Key.
- Foreign Key.
- Navigation Property.
- DbContext.
- Code First.
- Migration.
- CRUD.
- Model Binding.
- Data Annotation.
- Validation.
- Razor View.
- LINQ.
- Search.
- Filter.
- Sort.
- Pagination.
- Session.
- Đăng nhập.
- Phân quyền.
- GitHub.

## 31.1. Nội dung riêng của từng Module

### SV1

Phải giải thích:

- Kiểm tra đăng nhập.
- Session.
- Phân quyền.
- Quản lý loại phòng.

### SV2

Phải giải thích:

- Tìm kiếm.
- Lọc.
- Sắp xếp.
- Phân trang.
- Lọc phòng trống theo khoảng ngày.
- LINQ.

### SV3

Phải giải thích:

- Điều kiện đặt phòng.
- Kiểm tra trùng lịch.
- Quyền dữ liệu của khách hàng.
- Theo dõi đặt phòng.

### SV4

Phải giải thích:

- Luồng trạng thái đặt phòng.
- Điều kiện xác nhận.
- Kiểm tra trùng khoảng ngày.
- Nhận phòng.
- Trả phòng.
- Kiểm soát công suất phòng.

### SV5

Phải giải thích:

- Dịch vụ phát sinh.
- Tính tiền dịch vụ.
- Lập hóa đơn.
- Tính tiền phòng.
- Dashboard.
- Thống kê LINQ.

Giảng viên có thể yêu cầu sinh viên:

- Giải thích mã nguồn.
- Thực hiện thay đổi nhỏ trực tiếp trên Project.
- Chạy thử chức năng.
- Kiểm tra nghiệp vụ.

---

# 32. QUY TRÌNH NGHIỆP VỤ

## 32.1. Quy trình khách hàng

**Đăng nhập → Cập nhật thông tin cá nhân → Xem danh sách phòng → Tìm kiếm/Lọc/Sắp xếp/Phân trang → Đặt phòng → Theo dõi xác nhận → Nhận phòng → Sử dụng dịch vụ → Trả phòng → Xem hóa đơn**

## 32.2. Quy trình Lễ tân/Admin

**Đăng nhập → Quản lý loại phòng → Quản lý phòng → Tiếp nhận đặt phòng → Xác nhận → Nhận phòng → Ghi dịch vụ phát sinh → Trả phòng → Lập hóa đơn → Dashboard → Thống kê**

## 32.3. Quy trình xử lý đặt phòng

**Chờ xác nhận → Đã xác nhận → Đã nhận phòng → Đã trả phòng**

Nhánh:

**Chờ xác nhận → Từ chối**

hoặc:

**Trạng thái phù hợp → Đã hủy**

---

# 33. QUY TRÌNH XỬ LÝ DỮ LIỆU

**Entity → DbContext → Entity Framework Core 10 → Migration → SQL Server → LINQ → Controller → Razor View**

---

# 34. CẤU TRÚC PROJECT THAM KHẢO

- `Controllers/`
  - `TaiKhoanController`
  - `LoaiPhongController`
  - `PhongController`
  - `KhachHangController`
  - `DatPhongController`
  - `DichVuPhatSinhController`
  - `HoaDonController`
  - `ThongKeController`

- `Models/`
  - `TaiKhoan`
  - `LoaiPhong`
  - `Phong`
  - `KhachHang`
  - `DatPhong`
  - `DichVuPhatSinh`
  - `HoaDon`

- `Data/`
  - `ApplicationDbContext`

- `ViewModels/`

- `Views/`
  - `TaiKhoan`
  - `LoaiPhong`
  - `Phong`
  - `KhachHang`
  - `DatPhong`
  - `DichVuPhatSinh`
  - `HoaDon`
  - `ThongKe`

- `Migrations/`

- `wwwroot/`
  - `css/`
  - `js/`
  - `images/`

- `Program.cs`
- `appsettings.json`
- `README.md`

---

# 35. KẾT QUẢ CẦN ĐẠT

Sau khi hoàn thành bài tập lớn, nhóm phải xây dựng được ứng dụng Web:

**HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ**

sử dụng:

- ASP.NET Core 10 MVC.
- Entity Framework Core 10.
- SQL Server.

Hệ thống phải đáp ứng đầy đủ 05 Module:

1. Tài khoản - Đăng nhập - Phân quyền - Quản lý loại phòng.
2. Quản lý phòng - Tìm kiếm - Lọc - Sắp xếp - Phân trang.
3. Quản lý khách hàng - Hồ sơ cá nhân - Đặt phòng - Theo dõi đặt phòng.
4. Tiếp nhận đặt phòng - Xác nhận/Từ chối - Nhận phòng - Trả phòng - Quản lý trạng thái lưu trú.
5. Dịch vụ phát sinh - Hóa đơn thanh toán - Dashboard - Thống kê.

---

# 36. KIẾN THỨC TỔNG HỢP

**C#**

→ **ASP.NET Core 10 MVC**

→ **Entity / Data Annotation**

→ **DbContext**

→ **Entity Framework Core 10**

→ **Code First / Migration**

→ **CRUD**

→ **Validation**

→ **Razor View**

→ **LINQ**

→ **Search / Filter / Sort / Pagination**

→ **Session**

→ **Đăng nhập / Phân quyền**

→ **Đặt phòng**

→ **Xác nhận**

→ **Kiểm tra trùng phòng**

→ **Nhận phòng**

→ **Trả phòng**

→ **Hóa đơn**

→ **Dashboard / Thống kê**

→ **GitHub**

---

# 37. THÀNH VIÊN NHÓM

| STT | Họ và tên | Mã sinh viên | Module phụ trách |
|---:|---|---|---|
| 1 | Nguyễn Duy Thái | 23103100052 | Module 1 |
| 2 | Chưa cập nhật | Chưa cập nhật | Module 2 |
| 3 | Chưa cập nhật | Chưa cập nhật | Module 3 |
| 4 | Chưa cập nhật | Chưa cập nhật | Module 4 |
| 5 | Chưa cập nhật | Chưa cập nhật | Module 5 |

> Cập nhật chính xác họ tên, mã sinh viên và Module trước khi nộp bài.

---

# 38. GITHUB REPOSITORY

Repository của nhóm:

**QuanLyDatPhong_UNETI5_TI17A1HN**

GitHub:

https://github.com/nguyenduythai167243/QuanLyDatPhong_UNETI5_TI17A1HN

---

# 39. THÔNG TIN PROJECT

| Nội dung | Thông tin |
|---|---|
| Đề tài | Quản lý đặt phòng khách sạn và lưu trú |
| Môn học | Thực hành lập trình .NET |
| Framework | ASP.NET Core 10 MVC |
| Ngôn ngữ | C# |
| ORM | Entity Framework Core 10 |
| Database | SQL Server |
| Query | LINQ |
| View | Razor View |
| Version Control | Git/GitHub |
| Số thành viên | 05 |

---

# 🏨 NHÓM 5 - TI17A1HN

**HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG KHÁCH SẠN VÀ LƯU TRÚ**

**ASP.NET Core 10 MVC + Entity Framework Core 10 + SQL Server**
