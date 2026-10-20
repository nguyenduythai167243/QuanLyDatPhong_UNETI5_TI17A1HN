// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: MODULE 4 - TIẾP NHẬN, XÁC NHẬN VÀ QUẢN LÝ TRẠNG THÁI LƯU TRÚ (LeTanDatPhongController)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    [SessionAuthorize("Admin", "LeTan", "Lễ tân")]
    public class LeTanDatPhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeTanDatPhongController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region 8.1. DANH SÁCH ĐẶT PHÒNG CẦN XỬ LÝ (TÌM KIẾM, LỌC, SẮP XẾP, PHÂN TRANG)

        /// <summary>
        /// Màn hình danh sách đặt phòng dành cho Lễ tân
        /// Hỗ trợ tìm kiếm theo họ tên khách, số phòng; lọc theo loại phòng, trạng thái, ngày nhận phòng;
        /// lọc cảnh báo quá hạn nhận phòng (no-show); sắp xếp theo ngày đặt hoặc ngày nhận; phân trang EF Core.
        /// </summary>
        public async Task<IActionResult> Index(
            string? tuKhoa,
            int? maLoaiPhong,
            string? trangThai,
            DateTime? ngayNhanPhong,
            string? sortOrder,
            bool? canhBaoQuaHan,
            int page = 1)
        {
            // Bắt đầu truy vấn IQueryable với Navigation Properties
            var query = _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .AsQueryable();

            // 1. Thống kê KPI nhanh trên Database bằng LINQ
            var today = DateTime.Today;
            var tongDon = await _context.DatPhongs.CountAsync();
            var soChoXacNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Chờ xác nhận");
            var soDaXacNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã xác nhận");
            var soDangLuuTru = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã nhận phòng");
            var soDaTraPhong = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã trả phòng");
            var soQuaHanChuaNhan = await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã xác nhận" && d.NgayNhanPhong.Date < today);

            // 2. Tìm kiếm theo họ tên khách hàng hoặc số phòng (LINQ)
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(d =>
                    (d.KhachHang != null && d.KhachHang.HoTen.ToLower().Contains(kw)) ||
                    (d.Phong != null && d.Phong.SoPhong.ToLower().Contains(kw)) ||
                    (d.KhachHang != null && d.KhachHang.SoDienThoai.Contains(kw)));
            }

            // 3. Lọc theo Loại phòng (LINQ)
            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(d => d.Phong != null && d.Phong.MaLoaiPhong == maLoaiPhong.Value);
            }

            // 4. Lọc theo Trạng thái đặt phòng (LINQ)
            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(d => d.TrangThai == trangThai);
            }

            // 5. Lọc theo Ngày nhận phòng (LINQ)
            if (ngayNhanPhong.HasValue)
            {
                var targetDate = ngayNhanPhong.Value.Date;
                query = query.Where(d => d.NgayNhanPhong.Date == targetDate);
            }

            // 6. Lọc đơn cảnh báo quá hạn nhận phòng (No-show)
            if (canhBaoQuaHan == true)
            {
                query = query.Where(d => d.TrangThai == "Đã xác nhận" && d.NgayNhanPhong.Date < today);
            }

            // 7. Sắp xếp theo ngày đặt hoặc ngày nhận phòng (LINQ OrderBy)
            query = sortOrder switch
            {
                "ngaydat_asc" => query.OrderBy(d => d.NgayDat),
                "ngaydat_desc" => query.OrderByDescending(d => d.NgayDat),
                "ngaynhan_asc" => query.OrderBy(d => d.NgayNhanPhong).ThenByDescending(d => d.NgayDat),
                "ngaynhan_desc" => query.OrderByDescending(d => d.NgayNhanPhong).ThenByDescending(d => d.NgayDat),
                _ => query.OrderByDescending(d => d.NgayDat) // Mặc định: ngày đặt mới nhất trước
            };

            // 8. Phân trang dữ liệu bằng EF Core (Skip & Take) qua PaginatedList
            const int pageSize = 10;
            var pagedList = await PaginatedList<DatPhong>.CreateAsync(query.AsNoTracking(), page, pageSize);

            // Danh sách loại phòng phục vụ SelectList
            var loaiPhongs = await _context.LoaiPhongs.AsNoTracking().ToListAsync();

            var viewModel = new DatPhongFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoaiPhong = maLoaiPhong,
                TrangThai = trangThai,
                NgayNhanPhong = ngayNhanPhong,
                SortOrder = sortOrder,
                CanhBaoQuaHan = canhBaoQuaHan,
                Page = page,
                PageSize = pageSize,
                DanhSach = pagedList,
                LoaiPhongSelectList = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                TongDon = tongDon,
                SoChoXacNhan = soChoXacNhan,
                SoDaXacNhan = soDaXacNhan,
                SoDangLuuTru = soDangLuuTru,
                SoDaTraPhong = soDaTraPhong,
                SoQuaHanChuaNhan = soQuaHanChuaNhan
            };

            return View(viewModel);
        }

        #endregion

        #region 8.2 & 8.3. XEM CHI TIẾT & KIỂM TRA ĐIỀU KIỆN ĐẶT PHÒNG

        /// <summary>
        /// Xem chi tiết đặt phòng kèm báo cáo kiểm tra điều kiện nghiệp vụ theo thời gian thực (LINQ)
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(d => d.DichVuPhatSinhs)
                .Include(d => d.HoaDon)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);

            if (datPhong == null)
            {
                return NotFound();
            }

            // Kiểm tra điều kiện nghiệp vụ theo LINQ
            var kiemTraResult = await KiemTraDieuKienDatPhongAsync(datPhong);

            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var viewModel = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTraResult,
                SoDem = soDem,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(viewModel);
        }

        /// <summary>
        /// Thuật toán kiểm tra điều kiện trước khi xác nhận và kiểm soát công suất phòng (LINQ)
        /// Theo yêu cầu mục 8.3 & 8.5 trong tài liệu bài tập lớn:
        /// 1. Đặt phòng tồn tại và chưa bị hủy
        /// 2. Phòng liên quan tồn tại và ở trạng thái Sẵn sàng
        /// 3. Khách hàng còn hoạt động
        /// 4. Đặt phòng chưa có kết quả xử lý trước đó (đang là Chờ xác nhận)
        /// 5. Thuật toán Overlap kiểm tra không trùng khoảng ngày lưu trú với booking khác đang hiệu lực của phòng đó (LINQ)
        /// 6. Kiểm soát công suất phòng: Cảnh báo khi loại phòng đã kín lịch trong khoảng ngày (LINQ)
        /// </summary>
        private async Task<KiemTraDatPhongResult> KiemTraDieuKienDatPhongAsync(DatPhong datPhong)
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
            result.KhachHangHoatDong = khachHang != null && khachHang.TrangThai == "Hoạt động";
            if (khachHang == null)
            {
                result.DanhSachLoi.Add("Khách hàng liên quan không tồn tại trong hệ thống.");
            }
            else if (khachHang.TrangThai != "Hoạt động")
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
            // Hai khoảng lưu trú [checkIn, checkOut] và [dp.NgayNhanPhong, dp.NgayTraPhong] trùng nhau khi:
            // datPhong.NgayNhanPhong < dp.NgayTraPhong && datPhong.NgayTraPhong > dp.NgayNhanPhong
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
                // Đếm tổng số phòng Sẵn sàng thuộc loại phòng này bằng LINQ
                var tongPhongSanSang = await _context.Phongs
                    .CountAsync(p => p.MaLoaiPhong == maLoai && p.TrangThai == "Sẵn sàng");
                result.TongPhongSanSangCuaLoai = tongPhongSanSang;

                // Đếm số phòng của loại phòng này đã có đơn đặt hiệu lực trùng khoảng ngày này bằng LINQ
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

            // Đánh giá tính hợp lệ tổng quát để xác nhận (Mục 8.3 & 8.5)
            result.HopLeDeXacNhan = result.DangChoXacNhan &&
                                    datPhong.TrangThai != "Đã hủy" &&
                                    result.PhongSanSang &&
                                    result.KhachHangHoatDong &&
                                    !result.CoBiTrungLich &&
                                    !result.LoaiPhongBiKinLich;

            return result;
        }

        #endregion

        #region 8.2. XÁC NHẬN ĐẶT PHÒNG

        /// <summary>
        /// GET: LeTanDatPhong/XacNhan/5
        /// Hiển thị màn hình xem xét và xác nhận đặt phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> XacNhan(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Chờ xác nhận' mới được xác nhận. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var kiemTra = await KiemTraDieuKienDatPhongAsync(datPhong);
            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTra,
                SoDem = soDem,
                GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan) ? "Đã liên hệ khách và xác nhận đặt phòng." : datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/XacNhan/5
        /// Thực hiện xác nhận đặt phòng sau khi kiểm tra đầy đủ điều kiện bằng LINQ (8.3 & 8.5)
        /// Ghi nhận NgayXuLy và GhiChuLeTan (8.2)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhan(int id, string? ghiChuLeTan)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Đặt phòng #{id} đã được xử lý trước đó (trạng thái: {datPhong.TrangThai}).";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Kiểm tra lại toàn bộ điều kiện bằng LINQ
            var kiemTra = await KiemTraDieuKienDatPhongAsync(datPhong);
            if (!kiemTra.HopLeDeXacNhan)
            {
                var thongBao = string.Join("; ", kiemTra.DanhSachLoi);
                TempData["ThongBaoLoi"] = $"Không thể xác nhận đặt phòng do không thỏa mãn điều kiện: {thongBao}";
                return RedirectToAction(nameof(XacNhan), new { id });
            }

            // Cập nhật trạng thái sang "Đã xác nhận", ghi NgayXuLy và GhiChuLeTan (Mục 8.2)
            datPhong.TrangThai = "Đã xác nhận";
            datPhong.NgayXuLy = DateTime.Now;
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(ghiChuLeTan) ? "Đã xác nhận đặt phòng" : ghiChuLeTan.Trim();

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Đã xác nhận thành công đơn đặt phòng #{datPhong.MaDatPhong} cho khách hàng {datPhong.KhachHang?.HoTen}.";
            return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
        }

        #endregion

        #region 8.2. TỪ CHỐI ĐẶT PHÒNG

        /// <summary>
        /// GET: LeTanDatPhong/TuChoi/5
        /// Hiển thị màn hình từ chối đặt phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TuChoi(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Chờ xác nhận' mới được từ chối. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var kiemTra = await KiemTraDieuKienDatPhongAsync(datPhong);
            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTra,
                SoDem = soDem,
                LyDoTuChoi = "Phòng không đủ điều kiện đáp ứng trong khoảng thời gian đã chọn."
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/TuChoi/5
        /// Cập nhật kết quả xử lý sang "Từ chối", ghi NgayXuLy và GhiChuLeTan (Mục 8.2)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TuChoi(int id, string lyDoTuChoi)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Đặt phòng #{id} đã được xử lý trước đó.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                TempData["ThongBaoLoi"] = "Vui lòng nhập lý do từ chối đặt phòng.";
                return RedirectToAction(nameof(TuChoi), new { id });
            }

            datPhong.TrangThai = "Từ chối";
            datPhong.NgayXuLy = DateTime.Now;
            datPhong.GhiChuLeTan = $"Từ chối: {lyDoTuChoi.Trim()}";

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Đã từ chối đơn đặt phòng #{datPhong.MaDatPhong}. Lý do: {lyDoTuChoi.Trim()}";
            return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
        }

        #endregion

        #region 8.4. LÀM THỦ TỤC NHẬN PHÒNG (CHECK-IN)

        /// <summary>
        /// GET: LeTanDatPhong/NhanPhong/5
        /// Hiển thị màn hình xác nhận nhận phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> NhanPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // Kiểm tra ràng buộc luồng trạng thái (Mục 8.4)
            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Đã xác nhận' mới được làm thủ tục nhận phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // Kiểm tra ngày nhận phòng
            if (datPhong.NgayNhanPhong.Date > DateTime.Today)
            {
                TempData["ThongBaoCanhBao"] = $"Chưa đến ngày nhận phòng ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Hôm nay là {DateTime.Today:dd/MM/yyyy}.";
            }

            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                SoDem = soDem,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/NhanPhong/5
        /// Thực hiện thủ tục nhận phòng (Check-in)
        /// Ghi nhận NgayNhanThucTe = DateTime.Now và chuyển trạng thái sang "Đã nhận phòng" (Mục 8.4)
        /// Nghiêm cấm:
        /// - Chuyển trực tiếp từ "Chờ xác nhận" sang "Đã nhận phòng"
        /// - Đặt phòng "Từ chối" làm thủ tục nhận phòng
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NhanPhong(int id, string? ghiChu)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.Phong)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // Ràng buộc 1: Không cho chuyển trực tiếp từ Chờ xác nhận hoặc Từ chối sang Đã nhận phòng (8.4)
            if (datPhong.TrangThai == "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = "Quy trình không hợp lệ: Không cho chuyển trực tiếp từ 'Chờ xác nhận' sang 'Đã nhận phòng'. Cần xác nhận trước!";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (datPhong.TrangThai == "Từ chối")
            {
                TempData["ThongBaoLoi"] = "Quy trình không hợp lệ: Đặt phòng đã bị 'Từ chối', không được phép làm thủ tục nhận phòng!";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Quy trình không hợp lệ: Không thể nhận phòng cho đơn ở trạng thái '{datPhong.TrangThai}'.";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Ràng buộc 2: Đã đến ngày nhận phòng (8.4)
            if (datPhong.NgayNhanPhong.Date > DateTime.Today)
            {
                TempData["ThongBaoLoi"] = $"Chưa đến ngày nhận phòng theo quy định ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Không thể nhận phòng trước ngày.";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Cập nhật trạng thái đặt phòng sang "Đã nhận phòng" và ghi NgayNhanThucTe
            datPhong.TrangThai = "Đã nhận phòng";
            datPhong.NgayNhanThucTe = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(ghiChu))
            {
                datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                    ? ghiChu.Trim()
                    : $"{datPhong.GhiChuLeTan} | Nhận phòng: {ghiChu.Trim()}";
            }

            // Cập nhật trạng thái phòng sang "Đang có khách" nếu phòng vẫn sẵn sàng
            if (datPhong.Phong != null && datPhong.Phong.TrangThai == "Sẵn sàng")
            {
                datPhong.Phong.TrangThai = "Đang có khách";
            }

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Làm thủ tục nhận phòng thành công cho đơn #{datPhong.MaDatPhong}. Khách hàng {datPhong.KhachHang?.HoTen} đã nhận phòng {datPhong.Phong?.SoPhong} vào lúc {datPhong.NgayNhanThucTe:dd/MM/yyyy HH:mm}.";
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.4. LÀM THỦ TỤC TRẢ PHÒNG (CHECK-OUT)

        /// <summary>
        /// GET: LeTanDatPhong/TraPhong/5
        /// Hiển thị màn hình trả phòng và tính toán lưu trú thực tế
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TraPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(d => d.DichVuPhatSinhs)
                .Include(d => d.HoaDon)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Đã nhận phòng' mới được làm thủ tục trả phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var ngayNhan = datPhong.NgayNhanThucTe ?? datPhong.NgayNhanPhong;
            var soDemThucTe = (int)(DateTime.Today - ngayNhan.Date).TotalDays;
            if (soDemThucTe <= 0) soDemThucTe = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                SoDem = soDemThucTe,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/TraPhong/5
        /// Thực hiện thủ tục trả phòng (Check-out)
        /// Ghi nhận NgayTraThucTe = DateTime.Now và chuyển trạng thái sang "Đã trả phòng" (Mục 8.4)
        /// Trả phòng về trạng thái "Sẵn sàng"
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TraPhong(int id, string? ghiChu)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.Phong)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // Ràng buộc luồng trạng thái: Chỉ "Đã nhận phòng" mới được trả phòng (8.4)
            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ThongBaoLoi"] = $"Không thể làm thủ tục trả phòng cho đơn ở trạng thái '{datPhong.TrangThai}'.";
                return RedirectToAction(nameof(Details), new { id });
            }

            datPhong.TrangThai = "Đã trả phòng";
            datPhong.NgayTraThucTe = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(ghiChu))
            {
                datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                    ? ghiChu.Trim()
                    : $"{datPhong.GhiChuLeTan} | Trả phòng: {ghiChu.Trim()}";
            }

            // Cập nhật trạng thái phòng trở lại "Sẵn sàng"
            if (datPhong.Phong != null && datPhong.Phong.TrangThai == "Đang có khách")
            {
                datPhong.Phong.TrangThai = "Sẵn sàng";
            }

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Làm thủ tục trả phòng thành công cho đơn #{datPhong.MaDatPhong}. Khách hàng đã trả phòng lúc {datPhong.NgayTraThucTe:dd/MM/yyyy HH:mm}. Vui lòng chuyển sang phân hệ Hóa đơn để lập thanh toán!";
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.4 & 8.5. HỦY ĐẶT PHÒNG & XỬ LÝ QUÁ HẠN NHẬN PHÒNG (NO-SHOW)

        /// <summary>
        /// POST: LeTanDatPhong/HuyQuaHan/5
        /// Lễ tân hủy đặt phòng đã xác nhận nhưng khách quá ngày nhận phòng mà không đến nhận (No-show)
        /// Đáp ứng mục 8.5: Đánh dấu cảnh báo để Lễ tân quyết định hủy, không tự ý xóa dữ liệu.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyQuaHan(int id, string? lyDo)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đơn ở trạng thái 'Đã xác nhận' mới có thể hủy do quá hạn nhận phòng.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (datPhong.NgayNhanPhong.Date >= DateTime.Today)
            {
                TempData["ThongBaoLoi"] = $"Đơn đặt phòng #{id} chưa quá ngày nhận phòng ({datPhong.NgayNhanPhong:dd/MM/yyyy}).";
                return RedirectToAction(nameof(Details), new { id });
            }

            datPhong.TrangThai = "Đã hủy";
            datPhong.NgayXuLy = DateTime.Now;
            var noiDungLyDo = string.IsNullOrWhiteSpace(lyDo) ? "Khách quá hạn ngày nhận phòng không đến nhận (No-show)." : lyDo.Trim();
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                ? $"Đã hủy: {noiDungLyDo}"
                : $"{datPhong.GhiChuLeTan} | Đã hủy: {noiDungLyDo}";

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Đã hủy thành công đơn đặt phòng quá hạn #{datPhong.MaDatPhong}.";
            return RedirectToAction(nameof(Index), new { canhBaoQuaHan = true });
        }

        /// <summary>
        /// POST: LeTanDatPhong/HuyDatPhong/5
        /// Lễ tân hủy đặt phòng khi ở trạng thái phù hợp (Chờ xác nhận hoặc Đã xác nhận).
        /// Nghiêm cấm hủy khi đã nhận phòng hoặc đã trả phòng (Mục 8.4).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyDatPhong(int id, string? lyDo)
        {
            var datPhong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận" && datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Không thể hủy đặt phòng ở trạng thái '{datPhong.TrangThai}'. Chỉ đơn 'Chờ xác nhận' hoặc 'Đã xác nhận' mới được hủy.";
                return RedirectToAction(nameof(Details), new { id });
            }

            datPhong.TrangThai = "Đã hủy";
            datPhong.NgayXuLy = DateTime.Now;
            var noiDungLyDo = string.IsNullOrWhiteSpace(lyDo) ? "Khách yêu cầu hủy / Lễ tân xử lý hủy." : lyDo.Trim();
            datPhong.GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan)
                ? $"Đã hủy: {noiDungLyDo}"
                : $"{datPhong.GhiChuLeTan} | Đã hủy: {noiDungLyDo}";

            await _context.SaveChangesAsync();

            TempData["ThongBaoThanhCong"] = $"Đã hủy thành công đặt phòng #{datPhong.MaDatPhong}.";
            return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
        }

        #endregion

        #region 8.5. TRA CỨU & KIỂM SOÁT CÔNG SUẤT PHÒNG THEO LOẠI PHÒNG (LINQ)

        /// <summary>
        /// GET: LeTanDatPhong/KiemTraCongSuat
        /// Action phục vụ tra cứu nhanh công suất phòng của toàn bộ khách sạn trong khoảng ngày bằng LINQ
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> KiemTraCongSuat(DateTime? tuNgay, DateTime? denNgay)
        {
            var checkIn = tuNgay?.Date ?? DateTime.Today;
            var checkOut = denNgay?.Date ?? DateTime.Today.AddDays(1);

            if (checkOut <= checkIn)
            {
                checkOut = checkIn.AddDays(1);
            }

            // Truy vấn LINQ tổng hợp công suất theo từng loại phòng
            var loaiPhongs = await _context.LoaiPhongs.AsNoTracking().ToListAsync();
            var danhSachBaoCao = new List<object>();

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

                danhSachBaoCao.Add(new
                {
                    lp.MaLoaiPhong,
                    lp.TenLoaiPhong,
                    lp.SucChuaToiDa,
                    lp.GiaThamKhao,
                    TongPhong = tongPhong,
                    PhongSanSang = phongSanSang,
                    PhongDaDat = phongDaDat,
                    PhongConTrong = Math.Max(0, phongConTrong),
                    TyLeLapDay = Math.Round(tyLeLapDay, 1),
                    KinLich = phongSanSang > 0 && phongDaDat >= phongSanSang
                });
            }

            ViewBag.TuNgay = checkIn;
            ViewBag.DenNgay = checkOut;

            return View(danhSachBaoCao);
        }

        #endregion

        #region CÁC ACTION CRUD PHỤ TRỢ (TƯƠNG THÍCH HỆ THỐNG)

        // GET: LeTanDatPhong/Create (Tạo đặt phòng trực tiếp tại quầy của Lễ tân)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var khachHangs = await _context.KhachHangs
                .Where(k => k.TrangThai == "Hoạt đ")
                .AsNoTracking()
                .ToListAsync();

            var phongs = await _context.Phongs
                .Where(p => p.TrangThai == "Sẵn sàng")
                .Include(p => p.LoaiPhong)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen");
            ViewBag.MaPhong = new SelectList(phongs.Select(p => new {
                p.MaPhong,
                TenHienThi = $"{p.SoPhong} - {p.LoaiPhong?.TenLoaiPhong} ({p.GiaMotDem:N0} đ/đêm)"
            }), "MaPhong", "TenHienThi");

            var model = new DatPhong
            {
                NgayDat = DateTime.Now,
                NgayNhanPhong = DateTime.Today,
                NgayTraPhong = DateTime.Today.AddDays(1),
                SoNguoi = 1,
                TrangThai = "Chờ xác nhận"
            };

            return View(model);
        }

        // POST: LeTanDatPhong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DatPhong datphong)
        {
            // Kiểm tra ngày hợp lệ
            if (datphong.NgayTraPhong.Date <= datphong.NgayNhanPhong.Date)
            {
                ModelState.AddModelError(nameof(datphong.NgayTraPhong), "Ngày trả phòng phải sau ngày nhận phòng.");
            }

            if (datphong.NgayNhanPhong.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(datphong.NgayNhanPhong), "Ngày nhận phòng không được nằm trong quá khứ.");
            }

            // Kiểm tra phòng và tính giá
            var phong = await _context.Phongs.FindAsync(datphong.MaPhong);
            if (phong == null || phong.TrangThai != "Sẵn sàng")
            {
                ModelState.AddModelError(nameof(datphong.MaPhong), "Phòng đã chọn không tồn tại hoặc không ở trạng thái sẵn sàng.");
            }
            else if (datphong.SoNguoi > phong.SucChua)
            {
                ModelState.AddModelError(nameof(datphong.SoNguoi), $"Số người ({datphong.SoNguoi}) vượt quá sức chứa tối đa ({phong.SucChua}) của phòng {phong.SoPhong}.");
            }

            // Kiểm tra trùng lịch bằng LINQ
            var coTrung = await _context.DatPhongs.AnyAsync(dp =>
                dp.MaPhong == datphong.MaPhong &&
                (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng" || dp.TrangThai == "Chờ xác nhận") &&
                datphong.NgayNhanPhong < dp.NgayTraPhong &&
                datphong.NgayTraPhong > dp.NgayNhanPhong);

            if (coTrung)
            {
                ModelState.AddModelError(nameof(datphong.MaPhong), "Phòng này đã có người đặt trong khoảng ngày đã chọn.");
            }

            if (ModelState.IsValid)
            {
                var soDem = (int)(datphong.NgayTraPhong.Date - datphong.NgayNhanPhong.Date).TotalDays;
                if (soDem <= 0) soDem = 1;

                datphong.NgayDat = DateTime.Now;
                datphong.TongTienDuKien = (phong?.GiaMotDem ?? 0) * soDem;
                datphong.TrangThai = "Chờ xác nhận";

                _context.DatPhongs.Add(datphong);
                await _context.SaveChangesAsync();

                TempData["ThongBaoThanhCong"] = $"Tạo đặt phòng mới #{datphong.MaDatPhong} thành công.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            // Nạp lại danh sách nếu lỗi
            var khachHangs = await _context.KhachHangs.Where(k => k.TrangThai == "Hoạt động").AsNoTracking().ToListAsync();
            var phongs = await _context.Phongs.Where(p => p.TrangThai == "Sẵn sàng").Include(p => p.LoaiPhong).AsNoTracking().ToListAsync();
            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(phongs.Select(p => new {
                p.MaPhong,
                TenHienThi = $"{p.SoPhong} - {p.LoaiPhong?.TenLoaiPhong} ({p.GiaMotDem:N0} đ/đêm)"
            }), "MaPhong", "TenHienThi", datphong.MaPhong);

            return View(datphong);
        }

        // GET: LeTanDatPhong/Edit/5
        // Nghiệp vụ mục 8.4: Không cho sửa đặt phòng đã Đã trả phòng
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var datphong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);

            if (datphong == null) return NotFound();

            // Kiểm tra quy tắc nghiệp vụ mục 8.4: Không cho sửa đặt phòng đã Đã trả phòng
            if (datphong.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = "Nghiệp vụ quy định: Không được phép chỉnh sửa đặt phòng đã 'Đã trả phòng'.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            ViewBag.MaKhachHang = new SelectList(await _context.KhachHangs.Where(k => k.TrangThai == "Hoạt động").AsNoTracking().ToListAsync(), "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(await _context.Phongs.AsNoTracking().ToListAsync(), "MaPhong", "SoPhong", datphong.MaPhong);
            return View(datphong);
        }

        // POST: LeTanDatPhong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DatPhong datphong)
        {
            if (id != datphong.MaDatPhong) return NotFound();

            var existing = await _context.DatPhongs.FindAsync(id);
            if (existing == null) return NotFound();

            // Kiểm tra quy tắc nghiệp vụ mục 8.4: Không cho sửa đặt phòng đã Đã trả phòng
            if (existing.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = "Nghiệp vụ quy định: Không được phép chỉnh sửa đặt phòng đã 'Đã trả phòng'.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existing.MaKhachHang = datphong.MaKhachHang;
                    existing.MaPhong = datphong.MaPhong;
                    existing.NgayNhanPhong = datphong.NgayNhanPhong;
                    existing.NgayTraPhong = datphong.NgayTraPhong;
                    existing.SoNguoi = datphong.SoNguoi;
                    existing.YeuCauDacBiet = datphong.YeuCauDacBiet;
                    existing.GhiChuLeTan = datphong.GhiChuLeTan;

                    await _context.SaveChangesAsync();
                    TempData["ThongBaoThanhCong"] = $"Cập nhật thành công thông tin đặt phòng #{id}.";
                    return RedirectToAction(nameof(Details), new { id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DatPhongs.Any(e => e.MaDatPhong == id)) return NotFound();
                    throw;
                }
            }

            ViewBag.MaKhachHang = new SelectList(await _context.KhachHangs.Where(k => k.TrangThai == "Hoạt động").AsNoTracking().ToListAsync(), "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(await _context.Phongs.AsNoTracking().ToListAsync(), "MaPhong", "SoPhong", datphong.MaPhong);
            return View(datphong);
        }

        // GET: LeTanDatPhong/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var datphong = await _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.Phong)
                .FirstOrDefaultAsync(m => m.MaDatPhong == id);

            if (datphong == null) return NotFound();

            if (datphong.TrangThai == "Đã nhận phòng" || datphong.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = $"Không được xóa đặt phòng ở trạng thái '{datphong.TrangThai}'.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            return View(datphong);
        }

        // POST: LeTanDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datphong = await _context.DatPhongs.FindAsync(id);
            if (datphong != null)
            {
                if (datphong.TrangThai == "Đã nhận phòng" || datphong.TrangThai == "Đã trả phòng")
                {
                    TempData["ThongBaoLoi"] = $"Không được xóa đặt phòng đã nhận hoặc đã trả phòng.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                _context.DatPhongs.Remove(datphong);
                await _context.SaveChangesAsync();
                TempData["ThongBaoThanhCong"] = $"Đã xóa thành công đặt phòng #{id}.";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}