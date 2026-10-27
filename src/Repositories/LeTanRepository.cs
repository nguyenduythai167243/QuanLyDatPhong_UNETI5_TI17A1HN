// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: MODULE 4 - TRIỂN KHAI REPOSITORY CHO LỄ TÂN ĐẶT PHÒNG (LeTanRepository)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class LeTanRepository : ILeTanDatPhongRepository
    {
        private readonly ApplicationDbContext _context;

        public LeTanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region 1. Thống kê KPI & Danh sách đặt phòng
        public async Task<ThongKeKpiLeTanDto> GetThongKeKpiAsync()
        {
            var today = DateTime.Today;
            return new ThongKeKpiLeTanDto
            {
                TongDon = await _context.DatPhongs.CountAsync(),
                SoChoXacNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Chờ xác nhận"),
                SoDaXacNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã xác nhận"),
                SoDangLuuTru = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã nhận phòng"),
                SoDaTraPhong = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã trả phòng"),
                SoQuaHanChuaNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã xác nhận" && d.NgayNhanPhong.Date < today)
            };
        }

        public async Task<PaginatedList<DatPhong>> GetDanhSachDatPhongAsync(
            string? tuKhoa,
            int? maLoaiPhong,
            string? trangThai,
            DateTime? ngayNhanPhong,
            string? sortOrder,
            bool? canhBaoQuaHan,
            int page,
            int pageSize)
        {
            var query = _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .AsQueryable();

            var today = DateTime.Today;

            // 1. Tìm kiếm theo từ khóa (họ tên, số phòng, số điện thoại)
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(d =>
                    (d.KhachHang != null && d.KhachHang.HoTen.ToLower().Contains(kw)) ||
                    (d.Phong != null && d.Phong.SoPhong.ToLower().Contains(kw)) ||
                    (d.KhachHang != null && d.KhachHang.SoDienThoai.Contains(kw)));
            }

            // 2. Lọc theo Loại phòng
            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(d => d.Phong != null && d.Phong.MaLoaiPhong == maLoaiPhong.Value);
            }

            // 3. Lọc theo Trạng thái đặt phòng
            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(d => d.TrangThai == trangThai);
            }

            // 4. Lọc theo Ngày nhận phòng
            if (ngayNhanPhong.HasValue)
            {
                var targetDate = ngayNhanPhong.Value.Date;
                query = query.Where(d => d.NgayNhanPhong.Date == targetDate);
            }

            // 5. Lọc đơn cảnh báo quá hạn nhận phòng (No-show)
            if (canhBaoQuaHan == true)
            {
                query = query.Where(d => d.TrangThai == "Đã xác nhận" && d.NgayNhanPhong.Date < today);
            }

            // 6. Sắp xếp dữ liệu (LINQ OrderBy)
            query = sortOrder switch
            {
                "ngaydat_asc" => query.OrderBy(d => d.NgayDat),
                "ngaydat_desc" => query.OrderByDescending(d => d.NgayDat),
                "ngaynhan_asc" => query.OrderBy(d => d.NgayNhanPhong).ThenByDescending(d => d.NgayDat),
                "ngaynhan_desc" => query.OrderByDescending(d => d.NgayNhanPhong).ThenByDescending(d => d.NgayDat),
                _ => query.OrderByDescending(d => d.NgayDat)
            };

            return await PaginatedList<DatPhong>.CreateAsync(query.AsNoTracking(), page, pageSize);
        }
        #endregion

        #region 2. Chi tiết & Kiểm tra điều kiện nghiệp vụ (LINQ)
        public async Task<DatPhong?> GetDatPhongByIdAsync(int id, bool includeFullDetails = true)
        {
            if (includeFullDetails)
            {
                return await _context.DatPhongs
                    .Include(d => d.KhachHang)
                    .Include(d => d.Phong)
                        .ThenInclude(p => p!.LoaiPhong)
                    .Include(d => d.DichVuPhatSinhs)
                    .Include(d => d.HoaDon)
                    .FirstOrDefaultAsync(m => m.MaDatPhong == id);
            }

            return await _context.DatPhongs.FindAsync(id);
        }

        public async Task<KiemTraDatPhongResult> KiemTraDieuKienDatPhongAsync(int maDatPhong)
        {
            var datPhong = await GetDatPhongByIdAsync(maDatPhong, true);
            if (datPhong == null)
            {
                return new KiemTraDatPhongResult
                {
                    MaDatPhong = maDatPhong,
                    DatPhongTonTai = false,
                    DanhSachLoi = new List<string> { "Không tìm thấy thông tin đặt phòng trong hệ thống." }
                };
            }

            return await KiemTraDieuKienDatPhongAsync(datPhong);
        }

        public async Task<KiemTraDatPhongResult> KiemTraDieuKienDatPhongAsync(DatPhong datPhong)
        {
            var result = new KiemTraDatPhongResult
            {
                MaDatPhong = datPhong.MaDatPhong,
                DatPhongTonTai = true,
                DangChoXacNhan = datPhong.TrangThai == "Chờ xác nhận"
            };

            var today = DateTime.Today;
            result.DaDenNgayNhan = datPhong.NgayNhanPhong.Date <= today;
            result.QuaHanNhanPhong = datPhong.TrangThai == "Đã xác nhận" && datPhong.NgayNhanPhong.Date < today;

            // 1. Kiểm tra đặt phòng chưa bị hủy (Mục 8.3)
            if (datPhong.TrangThai == "Đã hủy")
            {
                result.DanhSachLoi.Add("Đặt phòng này đã bị hủy, không thể xác nhận.");
            }

            // 2. Kiểm tra đặt phòng chưa có kết quả xử lý trước đó (Mục 8.3)
            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                result.DanhSachLoi.Add($"Đặt phòng đã có kết quả xử lý trước đó (Trạng thái hiện tại: {datPhong.TrangThai}).");
            }

            // 3. Kiểm tra khách hàng còn hoạt động (Mục 8.3)
            var khachHang = await _context.KhachHangs.FindAsync(datPhong.MaKhachHang);
            result.KhachHangHoatDong = khachHang != null && (khachHang.TrangThai == "Hoạt động" || khachHang.TrangThai.StartsWith("Hoạt đ"));
            if (khachHang == null)
            {
                result.DanhSachLoi.Add("Khách hàng liên quan không tồn tại trong hệ thống.");
            }
            else if (!result.KhachHangHoatDong)
            {
                result.DanhSachLoi.Add("Tài khoản khách hàng này hiện đang bị khóa hoặc ngừng hoạt động.");
            }

            // 4. Kiểm tra phòng tồn tại và vẫn ở trạng thái Sẵn sàng (Mục 8.3)
            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == datPhong.MaPhong);

            if (phong == null)
            {
                result.PhongSanSang = false;
                result.DanhSachLoi.Add("Phòng liên quan không tồn tại trong hệ thống.");
            }
            else
            {
                result.TrangThaiHienTaiCuaPhong = phong.TrangThai;
                result.PhongSanSang = string.Equals(phong.TrangThai, "Sẵn sàng", StringComparison.OrdinalIgnoreCase);

                if (!result.PhongSanSang)
                {
                    result.DanhSachLoi.Add($"Phòng {phong.SoPhong} hiện ở trạng thái '{phong.TrangThai}', không ở trạng thái Sẵn sàng.");
                }

                // Kiểm tra sức chứa phòng
                if (datPhong.SoNguoi > phong.SucChua)
                {
                    result.DanhSachCanhBao.Add($"Số lượng khách ({datPhong.SoNguoi} người) vượt quá sức chứa tối đa ({phong.SucChua} người) của phòng {phong.SoPhong}.");
                }
            }

            // 5. Thuật toán kiểm tra trùng lịch (Overlap Check) bằng LINQ (Mục 8.3)
            var checkIn = datPhong.NgayNhanPhong;
            var checkOut = datPhong.NgayTraPhong;

            var dsTrungLich = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Where(dp =>
                    dp.MaPhong == datPhong.MaPhong &&
                    dp.MaDatPhong != datPhong.MaDatPhong &&
                    (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                    checkIn < dp.NgayTraPhong &&
                    checkOut > dp.NgayNhanPhong)
                .ToListAsync();

            result.DanhSachTrungLich = dsTrungLich;
            result.CoBiTrungLich = dsTrungLich.Any();

            if (result.CoBiTrungLich)
            {
                foreach (var trung in dsTrungLich)
                {
                    result.DanhSachLoi.Add(
                        $"Phòng {phong?.SoPhong} đã có đơn đặt #{trung.MaDatPhong} " +
                        $"({trung.KhachHang?.HoTen}) ở trạng thái '{trung.TrangThai}' " +
                        $"từ {trung.NgayNhanPhong:dd/MM/yyyy} đến {trung.NgayTraPhong:dd/MM/yyyy} trùng khoảng ngày lưu trú.");
                }
            }

            // 6. Kiểm soát công suất phòng theo loại phòng bằng LINQ (Mục 8.5)
            if (phong != null)
            {
                var maLoai = phong.MaLoaiPhong;
                var tongPhongSanSang = await _context.Phongs
                    .CountAsync(p => p.MaLoaiPhong == maLoai && p.TrangThai == "Sẵn sàng");
                result.TongPhongSanSangCuaLoai = tongPhongSanSang;

                var soPhongDaKinLich = await _context.Phongs
                    .Where(p => p.MaLoaiPhong == maLoai && p.TrangThai == "Sẵn sàng")
                    .Where(p => _context.DatPhongs.Any(dp =>
                        dp.MaPhong == p.MaPhong &&
                        dp.MaDatPhong != datPhong.MaDatPhong &&
                        (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                        checkIn < dp.NgayTraPhong &&
                        checkOut > dp.NgayNhanPhong))
                    .CountAsync();

                result.SoPhongDaKinLichTrongKhoang = soPhongDaKinLich;

                if (tongPhongSanSang > 0 && soPhongDaKinLich >= tongPhongSanSang)
                {
                    result.LoaiPhongBiKinLich = true;
                    result.DanhSachLoi.Add(
                        $"CẢNH BÁO KÍN LỊCH: Toàn bộ {tongPhongSanSang} phòng sẵn sàng của loại '{phong.LoaiPhong?.TenLoaiPhong}' " +
                        $"đã kín lịch trong khoảng từ {checkIn:dd/MM/yyyy} đến {checkOut:dd/MM/yyyy}. Không thể xác nhận thêm!");
                }
                else if (tongPhongSanSang > 0 && soPhongDaKinLich == tongPhongSanSang - 1)
                {
                    result.DanhSachCanhBao.Add(
                        $"Công suất phòng cao: Loại '{phong.LoaiPhong?.TenLoaiPhong}' chỉ còn đúng 01 phòng trống trong khoảng ngày này.");
                }
            }

            result.HopLeDeXacNhan = result.DangChoXacNhan &&
                                    datPhong.TrangThai != "Đã hủy" &&
                                    result.PhongSanSang &&
                                    result.KhachHangHoatDong &&
                                    !result.CoBiTrungLich &&
                                    !result.LoaiPhongBiKinLich;

            return result;
        }
        #endregion

        #region 3. Xác nhận, Từ chối và Hủy đặt phòng
        public async Task<(bool Success, string Message)> XacNhanDatPhongAsync(int id, string? ghiChuLeTan)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                return (false, $"Đặt phòng #{id} đã được xử lý trước đó (trạng thái: {datPhong.TrangThai}).");
            }

            var kiemTra = await KiemTraDieuKienDatPhongAsync(datPhong);
            if (!kiemTra.HopLeDeXacNhan)
            {
                var thongBao = string.Join("; ", kiemTra.DanhSachLoi);
                return (false, $"Không thể xác nhận đặt phòng do không thỏa mãn điều kiện: {thongBao}");
            }

            datPhong.TrangThai = "Đã xác nhận";
            datPhong.NgayXuLy = DateTime.Now;
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(ghiChuLeTan) ? "Đã xác nhận đặt phòng" : ghiChuLeTan.Trim();

            await _context.SaveChangesAsync();
            return (true, $"Đã xác nhận thành công đơn đặt phòng #{datPhong.MaDatPhong} cho khách hàng {datPhong.KhachHang?.HoTen}.");
        }

        public async Task<(bool Success, string Message)> TuChoiDatPhongAsync(int id, string lyDoTuChoi)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                return (false, $"Đặt phòng #{id} đã được xử lý trước đó.");
            }

            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                return (false, "Vui lòng nhập lý do từ chối đặt phòng.");
            }

            datPhong.TrangThai = "Từ chối";
            datPhong.NgayXuLy = DateTime.Now;
            datPhong.GhiChuLeTan = $"Từ chối: {lyDoTuChoi.Trim()}";

            await _context.SaveChangesAsync();
            return (true, $"Đã từ chối đơn đặt phòng #{datPhong.MaDatPhong}. Lý do: {lyDoTuChoi.Trim()}");
        }

        public async Task<(bool Success, string Message)> HuyQuaHanAsync(int id, string? lyDo)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                return (false, "Chỉ đơn ở trạng thái 'Đã xác nhận' mới có thể hủy do quá hạn nhận phòng.");
            }

            if (datPhong.NgayNhanPhong.Date >= DateTime.Today)
            {
                return (false, $"Đơn đặt phòng #{id} chưa quá ngày nhận phòng ({datPhong.NgayNhanPhong:dd/MM/yyyy}).");
            }

            datPhong.TrangThai = "Đã hủy";
            datPhong.NgayXuLy = DateTime.Now;
            var noiDungLyDo = string.IsNullOrWhiteSpace(lyDo) ? "Khách quá hạn ngày nhận phòng không đến nhận (No-show)." : lyDo.Trim();
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                ? $"Đã hủy: {noiDungLyDo}"
                : $"{datPhong.GhiChuLeTan} | Đã hủy: {noiDungLyDo}";

            await _context.SaveChangesAsync();
            return (true, $"Đã hủy thành công đơn đặt phòng quá hạn #{datPhong.MaDatPhong}.");
        }

        public async Task<(bool Success, string Message)> HuyDatPhongAsync(int id, string? lyDo)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai != "Chờ xác nhận" && datPhong.TrangThai != "Đã xác nhận")
            {
                return (false, $"Không thể hủy đặt phòng ở trạng thái '{datPhong.TrangThai}'. Chỉ đơn 'Chờ xác nhận' hoặc 'Đã xác nhận' mới được hủy.");
            }

            datPhong.TrangThai = "Đã hủy";
            datPhong.NgayXuLy = DateTime.Now;
            var noiDungLyDo = string.IsNullOrWhiteSpace(lyDo) ? "Khách yêu cầu hủy / Lễ tân xử lý hủy." : lyDo.Trim();
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                ? $"Đã hủy: {noiDungLyDo}"
                : $"{datPhong.GhiChuLeTan} | Đã hủy: {noiDungLyDo}";

            await _context.SaveChangesAsync();
            return (true, $"Đã hủy thành công đặt phòng #{datPhong.MaDatPhong}.");
        }
        #endregion

        #region 4. Quy trình nhận phòng (Check-in) & trả phòng (Check-out)
        public async Task<(bool Success, string Message)> NhanPhongAsync(int id, string? ghiChu)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.Phong)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai == "Chờ xác nhận")
            {
                return (false, "Quy trình không hợp lệ: Không cho chuyển trực tiếp từ 'Chờ xác nhận' sang 'Đã nhận phòng'. Cần xác nhận trước!");
            }

            if (datPhong.TrangThai == "Từ chối")
            {
                return (false, "Quy trình không hợp lệ: Đặt phòng đã bị 'Từ chối', không được phép làm thủ tục nhận phòng!");
            }

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                return (false, $"Quy trình không hợp lệ: Không thể nhận phòng cho đơn ở trạng thái '{datPhong.TrangThai}'.");
            }

            if (datPhong.NgayNhanPhong.Date > DateTime.Today)
            {
                return (false, $"Chưa đến ngày nhận phòng theo quy định ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Không thể nhận phòng trước ngày.");
            }

            datPhong.TrangThai = "Đã nhận phòng";
            datPhong.NgayNhanThucTe = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(ghiChu))
            {
                datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                    ? ghiChu.Trim()
                    : $"{datPhong.GhiChuLeTan} | Nhận phòng: {ghiChu.Trim()}";
            }

            if (datPhong.Phong != null && datPhong.Phong.TrangThai == "Sẵn sàng")
            {
                datPhong.Phong.TrangThai = "Đang có khách";
            }

            await _context.SaveChangesAsync();
            return (true, $"Làm thủ tục nhận phòng thành công cho đơn #{datPhong.MaDatPhong}. Khách hàng {datPhong.KhachHang?.HoTen} đã nhận phòng {datPhong.Phong?.SoPhong} vào lúc {datPhong.NgayNhanThucTe:dd/MM/yyyy HH:mm}.");
        }

        public async Task<(bool Success, string Message)> TraPhongAsync(int id, string? ghiChu)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.Phong)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                return (false, $"Không thể làm thủ tục trả phòng cho đơn ở trạng thái '{datPhong.TrangThai}'.");
            }

            datPhong.TrangThai = "Đã trả phòng";
            datPhong.NgayTraThucTe = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(ghiChu))
            {
                datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                    ? ghiChu.Trim()
                    : $"{datPhong.GhiChuLeTan} | Trả phòng: {ghiChu.Trim()}";
            }

            if (datPhong.Phong != null && datPhong.Phong.TrangThai == "Đang có khách")
            {
                datPhong.Phong.TrangThai = "Sẵn sàng";
            }

            await _context.SaveChangesAsync();
            return (true, $"Làm thủ tục trả phòng thành công cho đơn #{datPhong.MaDatPhong}. Khách hàng đã trả phòng lúc {datPhong.NgayTraThucTe:dd/MM/yyyy HH:mm}. Vui lòng chuyển sang phân hệ Hóa đơn để lập thanh toán!");
        }
        #endregion

        #region 5. Kiểm soát công suất phòng theo loại phòng (Mục 8.5)
        public async Task<List<CongSuatLoaiPhongDto>> KiemTraCongSuatAsync(DateTime checkIn, DateTime checkOut)
        {
            var loaiPhongs = await _context.LoaiPhongs.AsNoTracking().ToListAsync();
            var danhSachBaoCao = new List<CongSuatLoaiPhongDto>();

            foreach (var lp in loaiPhongs)
            {
                var tongPhong = await _context.Phongs.CountAsync(p => p.MaLoaiPhong == lp.MaLoaiPhong);
                var phongSanSang = await _context.Phongs.CountAsync(p => p.MaLoaiPhong == lp.MaLoaiPhong && p.TrangThai == "Sẵn sàng");

                var phongDaDat = await _context.Phongs
                    .Where(p => p.MaLoaiPhong == lp.MaLoaiPhong && p.TrangThai == "Sẵn sàng")
                    .Where(p => _context.DatPhongs.Any(dp =>
                        dp.MaPhong == p.MaPhong &&
                        (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                        checkIn < dp.NgayTraPhong &&
                        checkOut > dp.NgayNhanPhong))
                    .CountAsync();

                var phongConTrong = phongSanSang - phongDaDat;
                var tyLeLapDay = phongSanSang > 0 ? (double)phongDaDat / phongSanSang * 100 : 0;

                danhSachBaoCao.Add(new CongSuatLoaiPhongDto
                {
                    MaLoaiPhong = lp.MaLoaiPhong,
                    TenLoaiPhong = lp.TenLoaiPhong,
                    SucChuaToiDa = lp.SucChuaToiDa,
                    GiaThamKhao = lp.GiaThamKhao,
                    TongPhong = tongPhong,
                    PhongSanSang = phongSanSang,
                    PhongDaDat = phongDaDat,
                    PhongConTrong = Math.Max(0, phongConTrong),
                    TyLeLapDay = Math.Round(tyLeLapDay, 1),
                    KinLich = phongSanSang > 0 && phongDaDat >= phongSanSang
                });
            }

            return danhSachBaoCao;
        }
        #endregion

        #region 6. Danh mục phụ & Thao tác CRUD bổ trợ
        public async Task<List<KhachHang>> GetDanhSachKhachHangHoatDongAsync()
        {
            return await _context.KhachHangs
                .Where(k => k.TrangThai == "Hoạt động" || k.TrangThai.StartsWith("Hoạt đ"))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Phong>> GetDanhSachPhongSanSangAsync()
        {
            return await _context.Phongs
                .Where(p => p.TrangThai == "Sẵn sàng")
                .Include(p => p.LoaiPhong)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<LoaiPhong>> GetDanhSachLoaiPhongAsync()
        {
            return await _context.LoaiPhongs.AsNoTracking().ToListAsync();
        }

        public async Task<Phong?> GetPhongByIdAsync(int maPhong)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);
        }

        public async Task<bool> IsPhongTrungLichAsync(int maPhong, DateTime checkIn, DateTime checkOut, int? excludeMaDatPhong = null)
        {
            var query = _context.DatPhongs.Where(dp =>
                dp.MaPhong == maPhong &&
                (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng" || dp.TrangThai == "Chờ xác nhận") &&
                checkIn < dp.NgayTraPhong &&
                checkOut > dp.NgayNhanPhong);

            if (excludeMaDatPhong.HasValue)
            {
                query = query.Where(dp => dp.MaDatPhong != excludeMaDatPhong.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> CreateDatPhongAsync(DatPhong datPhong)
        {
            _context.DatPhongs.Add(datPhong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDatPhongAsync(DatPhong datPhong)
        {
            var existing = await _context.DatPhongs.FindAsync(datPhong.MaDatPhong);
            if (existing == null) return false;

            if (existing.TrangThai == "Đã trả phòng")
            {
                return false;
            }

            existing.MaKhachHang = datPhong.MaKhachHang;
            existing.MaPhong = datPhong.MaPhong;
            existing.NgayNhanPhong = datPhong.NgayNhanPhong;
            existing.NgayTraPhong = datPhong.NgayTraPhong;
            existing.SoNguoi = datPhong.SoNguoi;
            existing.YeuCauDacBiet = datPhong.YeuCauDacBiet;
            existing.GhiChuLeTan = datPhong.GhiChuLeTan;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<(bool Success, string Message)> DeleteDatPhongAsync(int id)
        {
            var datPhong = await _context.DatPhongs.FindAsync(id);
            if (datPhong == null)
            {
                return (false, "Không tìm thấy thông tin đặt phòng.");
            }

            if (datPhong.TrangThai == "Đã nhận phòng" || datPhong.TrangThai == "Đã trả phòng")
            {
                return (false, $"Không được xóa đặt phòng ở trạng thái '{datPhong.TrangThai}'.");
            }

            _context.DatPhongs.Remove(datPhong);
            await _context.SaveChangesAsync();
            return (true, $"Đã xóa thành công đặt phòng #{id}.");
        }
        #endregion
    }
}
