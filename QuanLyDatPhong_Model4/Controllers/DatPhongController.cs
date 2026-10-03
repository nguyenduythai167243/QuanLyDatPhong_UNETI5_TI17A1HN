// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Tiếp nhận đặt phòng, xác nhận/từ chối, nhận phòng, trả phòng, quản lý trạng thái lưu trú, kiểm soát công suất phòng

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_Model4.Data;
using QuanLyDatPhong_Model4.Models.Entities;
using QuanLyDatPhong_Model4.Models.ViewModels;

namespace QuanLyDatPhong_Model4.Controllers
{
    public class DatPhongController : Controller
    {
        private readonly QuanLyDatPhongDbContext _context;

        public DatPhongController(QuanLyDatPhongDbContext context)
        {
            _context = context;
        }

        // Helper kiểm tra quyền truy cập (Admin hoặc Lễ tân)
        private bool KiemTraQuyenLeTan()
        {
            var vaiTro = HttpContext.Session.GetString("VaiTro");
            return vaiTro == "Admin" || vaiTro == "LeTan";
        }

        // =========================================================================
        // 8.1. TIẾP NHẬN ĐẶT PHÒNG - TÌM KIẾM, LỌC, SẮP XẾP, PHÂN TRANG (LINQ)
        // =========================================================================
        public async Task<IActionResult> Index(
            string? searchTerm,
            int? maLoaiPhong,
            string? trangThai,
            DateTime? ngayNhanPhong,
            bool chiXemQuaHan = false,
            string sortOrder = "NgayDat_Desc",
            int pageIndex = 1)
        {
            // Tự động gán quyền Lễ tân mặc định nếu chưa đăng nhập để tiện kiểm thử
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("VaiTro")))
            {
                HttpContext.Session.SetString("MaTaiKhoan", "3");
                HttpContext.Session.SetString("HoTen", "Nguyễn Duy Thái (Lễ tân)");
                HttpContext.Session.SetString("VaiTro", "LeTan");
            }

            var query = _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .AsQueryable();

            // 1. Thống kê số lượng theo trạng thái bằng LINQ (cho các badge tab)
            var counts = await _context.DatPhongs
                .GroupBy(dp => dp.TrangThai)
                .Select(g => new { TrangThai = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.TrangThai, x => x.Count);

            int countChoXacNhan = counts.GetValueOrDefault("Chờ xác nhận", 0);
            int countDaXacNhan = counts.GetValueOrDefault("Đã xác nhận", 0);
            int countDaNhanPhong = counts.GetValueOrDefault("Đã nhận phòng", 0);
            int countDaTraPhong = counts.GetValueOrDefault("Đã trả phòng", 0);
            int countTuChoi = counts.GetValueOrDefault("Từ chối", 0);
            int countDaHuy = counts.GetValueOrDefault("Đã hủy", 0);

            // Số đặt phòng quá hạn nhận phòng (No-show): Đã xác nhận nhưng hôm nay > NgayNhanPhong
            int countQuaHanNoShow = await _context.DatPhongs
                .CountAsync(dp => dp.TrangThai == "Đã xác nhận" && DateTime.Today > dp.NgayNhanPhong.Date);

            // 2. Tìm kiếm bằng LINQ theo Họ tên khách hàng hoặc Số phòng
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim().ToLower();
                query = query.Where(dp =>
                    (dp.KhachHang != null && dp.KhachHang.HoTen.ToLower().Contains(term)) ||
                    (dp.Phong != null && dp.Phong.SoPhong.ToLower().Contains(term)) ||
                    dp.MaDatPhong.ToString().Contains(term));
            }

            // 3. Lọc theo Loại phòng
            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(dp => dp.Phong != null && dp.Phong.MaLoaiPhong == maLoaiPhong.Value);
            }

            // 4. Lọc theo Trạng thái
            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "TatCa")
            {
                query = query.Where(dp => dp.TrangThai == trangThai);
            }

            // 5. Lọc theo Ngày nhận phòng
            if (ngayNhanPhong.HasValue)
            {
                DateTime filterDate = ngayNhanPhong.Value.Date;
                query = query.Where(dp => dp.NgayNhanPhong.Date == filterDate);
            }

            // 6. Lọc chỉ xem đặt phòng Quá hạn nhận phòng (Section 8.5)
            if (chiXemQuaHan)
            {
                query = query.Where(dp => dp.TrangThai == "Đã xác nhận" && DateTime.Today > dp.NgayNhanPhong.Date);
            }

            // 7. Sắp xếp bằng LINQ
            query = sortOrder switch
            {
                "NgayDat_Asc" => query.OrderBy(dp => dp.NgayDat),
                "NgayDat_Desc" => query.OrderByDescending(dp => dp.NgayDat),
                "NgayNhan_Asc" => query.OrderBy(dp => dp.NgayNhanPhong),
                "NgayNhan_Desc" => query.OrderByDescending(dp => dp.NgayNhanPhong),
                "SoPhong_Asc" => query.OrderBy(dp => dp.Phong != null ? dp.Phong.SoPhong : ""),
                _ => query.OrderByDescending(dp => dp.NgayDat)
            };

            // 8. Phân trang bằng Skip() và Take() trên LINQ EF Core (Section 6.6 & 15)
            int totalItems = await query.CountAsync();
            int pageSize = 8;
            if (pageIndex < 1) pageIndex = 1;

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 9. Chuẩn bị ViewModel
            var viewModel = new DatPhongListViewModel
            {
                SearchTerm = searchTerm,
                MaLoaiPhong = maLoaiPhong,
                TrangThai = trangThai,
                NgayNhanPhong = ngayNhanPhong,
                ChiXemQuaHan = chiXemQuaHan,
                SortOrder = sortOrder,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items,
                CountChoXacNhan = countChoXacNhan,
                CountDaXacNhan = countDaXacNhan,
                CountDaNhanPhong = countDaNhanPhong,
                CountDaTraPhong = countDaTraPhong,
                CountTuChoi = countTuChoi,
                CountDaHuy = countDaHuy,
                CountQuaHanNoShow = countQuaHanNoShow
            };

            // Danh sách Loại phòng cho Select dropdown
            ViewBag.LoaiPhongs = new SelectList(await _context.LoaiPhongs.OrderBy(l => l.TenLoaiPhong).ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong);

            return View(viewModel);
        }

        // =========================================================================
        // 8.1. XEM CHI TIẾT ĐẶT PHÒNG
        // =========================================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(dp => dp.DichVuPhatSinhs)
                .Include(dp => dp.HoaDon)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            return View(datPhong);
        }

        // =========================================================================
        // 8.2 & 8.3. XÁC NHẬN / TỪ CHỐI ĐẶT PHÒNG (KIỂM TRA ĐIỀU KIỆN & TRÙNG LỊCH BẰNG LINQ)
        // =========================================================================
        [HttpGet]
        public async Task<IActionResult> XacNhan(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // Chỉ đặt phòng "Chờ xác nhận" mới được xử lý xác nhận hoặc từ chối (Section 8.2)
            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ErrorMessage"] = $"Đặt phòng #{datPhong.MaDatPhong} đang ở trạng thái '{datPhong.TrangThai}', không thể thực hiện xác nhận/từ chối lại.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // KIỂM TRA ĐIỀU KIỆN TRƯỚC KHI XÁC NHẬN (Section 8.3):
            // 1. Phòng liên quan tồn tại và trạng thái là 'Sẵn sàng'
            bool phongSanSang = datPhong.Phong != null && datPhong.Phong.TrangThai == "Sẵn sàng";

            // 2. Khách hàng còn hoạt động
            bool khachHangHoatDong = datPhong.KhachHang != null && datPhong.KhachHang.TrangThai;

            // 3. KIỂM TRA TRÙNG LỊCH PHÒNG BẰNG LINQ:
            // "Hai khoảng lưu trú được xem là trùng nhau khi ngày nhận của khoảng này nhỏ hơn
            //  ngày trả của khoảng kia và ngày trả của khoảng này lớn hơn ngày nhận của khoảng kia." (Section 7.3 & 8.3)
            var trungLichList = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Where(dp => dp.MaPhong == datPhong.MaPhong
                          && dp.MaDatPhong != datPhong.MaDatPhong
                          && (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                          && datPhong.NgayNhanPhong < dp.NgayTraPhong
                          && datPhong.NgayTraPhong > dp.NgayNhanPhong)
                .ToListAsync();

            bool coDatPhongTrungLich = trungLichList.Any();

            // 4. KIỂM SOÁT CÔNG SUẤT PHÒNG THEO LOẠI PHÒNG (Section 8.5):
            // Đếm số phòng 'Sẵn sàng' của loại phòng này
            int maLoaiPhong = datPhong.Phong?.MaLoaiPhong ?? 0;
            var cacPhongCuaLoai = await _context.Phongs
                .Where(p => p.MaLoaiPhong == maLoaiPhong && p.TrangThai == "Sẵn sàng")
                .Select(p => p.MaPhong)
                .ToListAsync();

            int soPhongSanSang = cacPhongCuaLoai.Count;

            // Đếm số phòng đã kín lịch trong khoảng ngày này
            var phongDaCoLichCount = await _context.DatPhongs
                .Where(dp => cacPhongCuaLoai.Contains(dp.MaPhong)
                          && dp.MaDatPhong != datPhong.MaDatPhong
                          && (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                          && datPhong.NgayNhanPhong < dp.NgayTraPhong
                          && datPhong.NgayTraPhong > dp.NgayNhanPhong)
                .Select(dp => dp.MaPhong)
                .Distinct()
                .CountAsync();

            bool loaiPhongKinLich = (soPhongSanSang > 0 && phongDaCoLichCount >= soPhongSanSang);

            var vm = new XacNhanDatPhongViewModel
            {
                MaDatPhong = datPhong.MaDatPhong,
                DatPhong = datPhong,
                PhongSanSang = phongSanSang,
                KhachHangHoatDong = khachHangHoatDong,
                CoDatPhongTrungLich = coDatPhongTrungLich,
                DanhSachTrungLich = trungLichList,
                LoaiPhongKinLich = loaiPhongKinLich,
                SoPhongSanSangCuaLoai = soPhongSanSang,
                SoPhongDaKinhCuaLoai = phongDaCoLichCount,
                HanhDong = coDatPhongTrungLich || loaiPhongKinLich ? "TuChoi" : "XacNhan",
                GhiChuLeTan = coDatPhongTrungLich ? "Từ chối do phòng đã có khách đặt trùng khoảng ngày." :
                              loaiPhongKinLich ? "Từ chối do toàn bộ phòng loại này đã kín lịch." :
                              "Đã kiểm tra đầy đủ thông tin, phòng hợp lệ và sẵn sàng đón khách."
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhan(XacNhanDatPhongViewModel vm)
        {
            var datPhong = await _context.DatPhongs
                .Include(dp => dp.Phong)
                .Include(dp => dp.KhachHang)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == vm.MaDatPhong);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ErrorMessage"] = "Đặt phòng không ở trạng thái 'Chờ xác nhận'.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            if (string.IsNullOrWhiteSpace(vm.GhiChuLeTan))
            {
                ModelState.AddModelError(nameof(vm.GhiChuLeTan), "Vui lòng nhập ghi chú của lễ tân khi xử lý.");
            }

            if (vm.HanhDong == "XacNhan")
            {
                // Kiểm tra lại tính hợp lệ trước khi xác nhận (tránh race condition)
                bool coTrungLich = await _context.DatPhongs
                    .AnyAsync(dp => dp.MaPhong == datPhong.MaPhong
                              && dp.MaDatPhong != datPhong.MaDatPhong
                              && (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                              && datPhong.NgayNhanPhong < dp.NgayTraPhong
                              && datPhong.NgayTraPhong > dp.NgayNhanPhong);

                if (coTrungLich)
                {
                    ModelState.AddModelError("", "Không thể xác nhận vì phòng này vừa có đặt phòng khác được xác nhận trùng lịch!");
                }

                if (datPhong.Phong?.TrangThai != "Sẵn sàng")
                {
                    ModelState.AddModelError("", "Không thể xác nhận vì phòng không ở trạng thái Sẵn sàng.");
                }

                if (ModelState.IsValid)
                {
                    datPhong.TrangThai = "Đã xác nhận";
                    datPhong.NgayXuLy = DateTime.Now;
                    datPhong.GhiChuLeTan = vm.GhiChuLeTan;

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Xác nhận thành công đặt phòng #{datPhong.MaDatPhong} cho phòng {datPhong.Phong?.SoPhong}!";
                    return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
                }
            }
            else if (vm.HanhDong == "TuChoi")
            {
                if (ModelState.IsValid)
                {
                    datPhong.TrangThai = "Từ chối";
                    datPhong.NgayXuLy = DateTime.Now;
                    datPhong.GhiChuLeTan = vm.GhiChuLeTan;

                    await _context.SaveChangesAsync();
                    TempData["WarningMessage"] = $"Đã từ chối đặt phòng #{datPhong.MaDatPhong}. Lý do: {vm.GhiChuLeTan}";
                    return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
                }
            }

            // Nếu có lỗi, tải lại thông tin view
            vm.DatPhong = datPhong;
            return View(vm);
        }

        // =========================================================================
        // 8.4. LÀM THỦ TỤC NHẬN PHÒNG (CHECK-IN)
        // =========================================================================
        [HttpGet]
        public async Task<IActionResult> NhanPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // 1. Chỉ đặt phòng "Đã xác nhận" mới được làm thủ tục nhận phòng
            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ErrorMessage"] = $"Chỉ đặt phòng 'Đã xác nhận' mới được làm thủ tục nhận phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // 2. Chỉ được nhận phòng khi đã đến ngày nhận phòng (hoặc từ ngày nhận phòng)
            if (DateTime.Today < datPhong.NgayNhanPhong.Date)
            {
                TempData["ErrorMessage"] = $"Chưa đến ngày nhận phòng theo lịch hẹn ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Hôm nay là {DateTime.Today:dd/MM/yyyy}, không thể làm thủ tục nhận phòng sớm.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            return View(datPhong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NhanPhong(int maDatPhong, string? ghiChuLeTan)
        {
            var datPhong = await _context.DatPhongs
                .Include(dp => dp.Phong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ErrorMessage"] = "Đặt phòng không ở trạng thái 'Đã xác nhận'.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            if (DateTime.Today < datPhong.NgayNhanPhong.Date)
            {
                TempData["ErrorMessage"] = $"Chưa đến ngày nhận phòng ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Không thể nhận phòng.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // Cập nhật trạng thái và ghi nhận NgayNhanThucTe
            datPhong.TrangThai = "Đã nhận phòng";
            datPhong.NgayNhanThucTe = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(ghiChuLeTan))
            {
                datPhong.GhiChuLeTan = (string.IsNullOrEmpty(datPhong.GhiChuLeTan) ? "" : datPhong.GhiChuLeTan + " | ") +
                                      $"[Check-in {DateTime.Now:dd/MM HH:mm}]: {ghiChuLeTan}";
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Làm thủ tục nhận phòng thành công cho khách hàng! Phòng: {datPhong.Phong?.SoPhong}. Thời gian nhận: {datPhong.NgayNhanThucTe:dd/MM/yyyy HH:mm}.";

            return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
        }

        // =========================================================================
        // 8.4 & 9.3. LÀM THỦ TỤC TRẢ PHÒNG (CHECK-OUT) VÀ LẬP HÓA ĐƠN
        // =========================================================================
        [HttpGet]
        public async Task<IActionResult> TraPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Include(dp => dp.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(dp => dp.DichVuPhatSinhs)
                .Include(dp => dp.HoaDon)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            // Chỉ đặt phòng "Đã nhận phòng" mới được làm thủ tục trả phòng (Section 8.4)
            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ErrorMessage"] = $"Chỉ đặt phòng 'Đã nhận phòng' mới được làm thủ tục trả phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // Tính số đêm thực tế (tối thiểu 1 đêm)
            DateTime checkInTime = datPhong.NgayNhanThucTe ?? datPhong.NgayNhanPhong;
            DateTime checkOutTime = DateTime.Now;
            int soDem = (checkOutTime.Date - checkInTime.Date).Days;
            if (soDem < 1) soDem = 1;

            decimal tienPhong = soDem * (datPhong.Phong?.GiaMotDem ?? 0);
            decimal tienDichVu = datPhong.DichVuPhatSinhs.Where(d => d.TrangThai != "Đã hủy").Sum(d => d.ThanhTien);
            decimal tongThanhToan = tienPhong + tienDichVu;

            ViewBag.SoDemThucTe = soDem;
            ViewBag.TienPhong = tienPhong;
            ViewBag.TienDichVu = tienDichVu;
            ViewBag.TongThanhToan = tongThanhToan;

            return View(datPhong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TraPhong(int maDatPhong, decimal giamGia, string hinhThucThanhToan, string? ghiChuLeTan)
        {
            var datPhong = await _context.DatPhongs
                .Include(dp => dp.Phong)
                .Include(dp => dp.DichVuPhatSinhs)
                .Include(dp => dp.HoaDon)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ErrorMessage"] = "Đặt phòng không ở trạng thái 'Đã nhận phòng'.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            DateTime checkInTime = datPhong.NgayNhanThucTe ?? datPhong.NgayNhanPhong;
            DateTime checkOutTime = DateTime.Now;
            int soDem = (checkOutTime.Date - checkInTime.Date).Days;
            if (soDem < 1) soDem = 1;

            decimal tienPhong = soDem * (datPhong.Phong?.GiaMotDem ?? 0);
            decimal tienDichVu = datPhong.DichVuPhatSinhs.Where(d => d.TrangThai != "Đã hủy").Sum(d => d.ThanhTien);

            if (giamGia < 0 || giamGia > (tienPhong + tienDichVu))
            {
                TempData["ErrorMessage"] = "Giảm giá phải >= 0 và không vượt quá tổng tiền phòng + dịch vụ.";
                return RedirectToAction(nameof(TraPhong), new { id = maDatPhong });
            }

            decimal tongThanhToan = tienPhong + tienDichVu - giamGia;

            // 1. Cập nhật Đặt phòng sang "Đã trả phòng" và lưu NgayTraThucTe
            datPhong.TrangThai = "Đã trả phòng";
            datPhong.NgayTraThucTe = checkOutTime;
            if (!string.IsNullOrWhiteSpace(ghiChuLeTan))
            {
                datPhong.GhiChuLeTan = (string.IsNullOrEmpty(datPhong.GhiChuLeTan) ? "" : datPhong.GhiChuLeTan + " | ") +
                                      $"[Check-out {DateTime.Now:dd/MM HH:mm}]: {ghiChuLeTan}";
            }

            // 2. Tự động lập Hóa đơn thanh toán nếu chưa có (Section 9.3 & 9.4)
            if (datPhong.HoaDon == null)
            {
                var hoaDon = new HoaDon
                {
                    MaDatPhong = datPhong.MaDatPhong,
                    SoDem = soDem,
                    TienPhong = tienPhong,
                    TienDichVu = tienDichVu,
                    GiamGia = giamGia,
                    TongThanhToan = tongThanhToan,
                    HinhThucThanhToan = hinhThucThanhToan,
                    NgayLap = checkOutTime,
                    TrangThai = "Đã thanh toán"
                };
                _context.HoaDons.Add(hoaDon);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Làm thủ tục trả phòng và thanh toán thành công cho phòng {datPhong.Phong?.SoPhong}! Tổng thanh toán: {tongThanhToan:N0} VNĐ.";

            return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
        }

        // =========================================================================
        // 8.5. XỬ LÝ HỦY ĐẶT PHÒNG QUÁ HẠN NHẬN PHÒNG (NO-SHOW)
        // =========================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyQuaHan(int id)
        {
            var datPhong = await _context.DatPhongs
                .Include(dp => dp.Phong)
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ErrorMessage"] = "Chỉ hủy được đặt phòng đang ở trạng thái 'Đã xác nhận'.";
                return RedirectToAction(nameof(Details), new { id });
            }

            datPhong.TrangThai = "Đã hủy";
            datPhong.NgayXuLy = DateTime.Now;
            datPhong.GhiChuLeTan = (string.IsNullOrEmpty(datPhong.GhiChuLeTan) ? "" : datPhong.GhiChuLeTan + " | ") +
                                  $"[Hủy quá hạn {DateTime.Now:dd/MM HH:mm}]: Hủy do khách quá hạn nhận phòng (No-show). Giải phóng phòng {datPhong.Phong?.SoPhong}.";

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã hủy đặt phòng quá hạn #{datPhong.MaDatPhong} và giải phóng phòng {datPhong.Phong?.SoPhong} thành công!";

            return RedirectToAction(nameof(Index), new { chiXemQuaHan = true });
        }

        // =========================================================================
        // 8.5. KIỂM SOÁT CÔNG SUẤT PHÒNG & SƠ ĐỒ PHÒNG THỜI GIAN THỰC (LINQ)
        // =========================================================================
        public async Task<IActionResult> KiemSoatCongSuat(DateTime? tuNgay, DateTime? denNgay)
        {
            DateTime from = tuNgay ?? DateTime.Today;
            DateTime to = denNgay ?? DateTime.Today.AddDays(7);

            if (to <= from)
            {
                to = from.AddDays(1);
            }

            // 1. Thống kê công suất từng loại phòng bằng LINQ (Section 8.5)
            var loaiPhongs = await _context.LoaiPhongs.Include(l => l.Phongs).ToListAsync();
            var congSuatList = new List<LoaiPhongCongSuatItem>();

            foreach (var lp in loaiPhongs)
            {
                int tongPhong = lp.Phongs.Count;
                int phongSanSang = lp.Phongs.Count(p => p.TrangThai == "Sẵn sàng");
                int phongBaoTri = lp.Phongs.Count(p => p.TrangThai != "Sẵn sàng");

                var phongsSanSangIds = lp.Phongs.Where(p => p.TrangThai == "Sẵn sàng").Select(p => p.MaPhong).ToList();

                // Đếm số phòng đã có đặt phòng hiệu lực (Đã xác nhận hoặc Đã nhận phòng) trùng khoảng ngày
                int phongDaCoLich = await _context.DatPhongs
                    .Where(dp => phongsSanSangIds.Contains(dp.MaPhong)
                              && (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                              && from < dp.NgayTraPhong
                              && to > dp.NgayNhanPhong)
                    .Select(dp => dp.MaPhong)
                    .Distinct()
                    .CountAsync();

                congSuatList.Add(new LoaiPhongCongSuatItem
                {
                    MaLoaiPhong = lp.MaLoaiPhong,
                    TenLoaiPhong = lp.TenLoaiPhong,
                    TongSoPhong = tongPhong,
                    SoPhongSanSang = phongSanSang,
                    SoPhongBaoTri = phongBaoTri,
                    SoPhongDaCoLich = phongDaCoLich
                });
            }

            // 2. Sơ đồ trạng thái từng phòng thời gian thực (hôm nay)
            var allPhongs = await _context.Phongs.Include(p => p.LoaiPhong).OrderBy(p => p.SoPhong).ToListAsync();
            var activeDatPhongsToday = await _context.DatPhongs
                .Include(dp => dp.KhachHang)
                .Where(dp => (dp.TrangThai == "Đã nhận phòng" || (dp.TrangThai == "Đã xác nhận" && dp.NgayNhanPhong.Date <= DateTime.Today && dp.NgayTraPhong.Date > DateTime.Today)))
                .ToListAsync();

            var soDoPhong = new List<PhongGridItem>();
            foreach (var p in allPhongs)
            {
                var curDp = activeDatPhongsToday.FirstOrDefault(dp => dp.MaPhong == p.MaPhong);
                string tt = "Trống";
                if (p.TrangThai != "Sẵn sàng")
                {
                    tt = p.TrangThai; // Đang bảo trì / Ngừng kinh doanh
                }
                else if (curDp != null)
                {
                    if (curDp.TrangThai == "Đã nhận phòng")
                    {
                        tt = "Đang có khách";
                    }
                    else if (curDp.TrangThai == "Đã xác nhận" && curDp.NgayNhanPhong.Date == DateTime.Today)
                    {
                        tt = "Sắp nhận hôm nay";
                    }
                    else
                    {
                        tt = "Đã có người đặt";
                    }
                }

                soDoPhong.Add(new PhongGridItem
                {
                    MaPhong = p.MaPhong,
                    SoPhong = p.SoPhong,
                    Tang = p.Tang,
                    TenLoaiPhong = p.LoaiPhong?.TenLoaiPhong ?? "",
                    GiaMotDem = p.GiaMotDem,
                    TrangThaiVatLy = p.TrangThai,
                    TrangThaiHienTai = tt,
                    DatPhongHienTai = curDp
                });
            }

            var vm = new KiemSoatCongSuatViewModel
            {
                TuNgay = from,
                DenNgay = to,
                ThongKeLoaiPhong = congSuatList,
                SoDoPhong = soDoPhong
            };

            return View(vm);
        }

        // =========================================================================
        // THÊM NHANH DỊCH VỤ PHÁT SINH CHO ĐẶT PHÒNG ĐANG LƯU TRÚ (HỖ TRỢ MODULE 5)
        // =========================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemDichVu(int maDatPhong, string tenDichVu, int soLuong, decimal donGia, string? ghiChu)
        {
            var datPhong = await _context.DatPhongs.FindAsync(maDatPhong);
            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ErrorMessage"] = "Chỉ đặt phòng đang ở trạng thái 'Đã nhận phòng' mới được ghi nhận dịch vụ phát sinh.";
                return RedirectToAction(nameof(Details), new { id = maDatPhong });
            }

            if (string.IsNullOrWhiteSpace(tenDichVu) || soLuong <= 0 || donGia < 0)
            {
                TempData["ErrorMessage"] = "Thông tin dịch vụ không hợp lệ. Số lượng > 0 và đơn giá >= 0.";
                return RedirectToAction(nameof(Details), new { id = maDatPhong });
            }

            var dv = new DichVuPhatSinh
            {
                MaDatPhong = maDatPhong,
                TenDichVu = tenDichVu,
                SoLuong = soLuong,
                DonGia = donGia,
                NgaySuDung = DateTime.Now,
                GhiChu = ghiChu,
                TrangThai = "Đã ghi nhận"
            };

            _context.DichVuPhatSinhs.Add(dv);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã ghi nhận dịch vụ '{tenDichVu}' thành công cho đặt phòng #{maDatPhong}!";
            return RedirectToAction(nameof(Details), new { id = maDatPhong });
        }
    }
}
