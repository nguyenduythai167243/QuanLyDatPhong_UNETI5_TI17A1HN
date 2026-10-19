using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    [SessionAuthorize("Admin", "LeTan", "Lễ tân")]
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. DANH SÁCH KHÁCH HÀNG (TÌM KIẾM, LỌC, PHÂN TRANG)
        // ==========================================
        public async Task<IActionResult> Index(
            string? tuKhoa,
            string? trangThai,
            string? sortOrder,
            int page = 1)
        {
            var query = _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.Phong)
                .AsQueryable();

            // 1. Thống kê KPI nhanh
            var tongKhachHang = await _context.KhachHangs.CountAsync();
            var khachDangLuuTru = await _context.DatPhongs
                .Where(d => d.TrangThai == "Đã nhận phòng")
                .Select(d => d.MaKhachHang)
                .Distinct()
                .CountAsync();
            var khachHoatDong = await _context.KhachHangs.CountAsync(k => k.TrangThai);
            var khachTamKhoa = await _context.KhachHangs.CountAsync(k => !k.TrangThai);

            ViewBag.TongKhachHang = tongKhachHang;
            ViewBag.KhachDangLuuTru = khachDangLuuTru;
            ViewBag.KhachHoatDong = khachHoatDong;
            ViewBag.KhachTamKhoa = khachTamKhoa;

            // 2. Tìm kiếm theo từ khóa
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim();
                query = query.Where(k =>
                    k.HoTen.Contains(tuKhoa) ||
                    k.SoDienThoai.Contains(tuKhoa) ||
                    (k.Email != null && k.Email.Contains(tuKhoa)) ||
                    k.SoGiayToTuyThan.Contains(tuKhoa) ||
                    (k.DiaChi != null && k.DiaChi.Contains(tuKhoa)));
            }

            // 3. Lọc theo trạng thái
            if (!string.IsNullOrEmpty(trangThai))
            {
                if (trangThai == "HoatDong")
                {
                    query = query.Where(k => k.TrangThai);
                }
                else if (trangThai == "Khoa")
                {
                    query = query.Where(k => !k.TrangThai);
                }
                else if (trangThai == "DangLuuTru")
                {
                    query = query.Where(k => k.DatPhongs.Any(d => d.TrangThai == "Đã nhận phòng"));
                }
            }

            // 4. Sắp xếp
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(k => k.HoTen),
                "Date" => query.OrderBy(k => k.MaKhachHang),
                "date_desc" => query.OrderByDescending(k => k.MaKhachHang),
                _ => query.OrderByDescending(k => k.MaKhachHang)
            };

            // 5. Phân trang
            int pageSize = 10;
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            page = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.TuKhoa = tuKhoa;
            ViewBag.TrangThai = trangThai;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;

            return View(items);
        }

        // ==========================================
        // 2. CHI TIẾT HỒ SƠ & LỊCH SỬ ĐẶT PHÒNG
        // ==========================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var khachHang = await _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.Phong)
                        .ThenInclude(p => p!.LoaiPhong)
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.HoaDon)
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.DichVuPhatSinhs)
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);

            if (khachHang == null) return NotFound();

            // Tính thống kê chi tiêu của khách
            var datPhongs = khachHang.DatPhongs.OrderByDescending(d => d.NgayDat).ToList();
            var tongSoLanDat = datPhongs.Count;
            var soLanHoanTat = datPhongs.Count(d => d.TrangThai == "Đã trả phòng");
            var soLanHuy = datPhongs.Count(d => d.TrangThai == "Đã hủy");
            var tongChiTieu = datPhongs
                .Where(d => d.HoaDon != null && d.HoaDon.TrangThai == TrangThaiTT.DaThanhToan)
                .Sum(d => (decimal?)d.HoaDon!.TongTienThanhToan) ?? 0;

            ViewBag.TongSoLanDat = tongSoLanDat;
            ViewBag.SoLanHoanTat = soLanHoanTat;
            ViewBag.SoLanHuy = soLanHuy;
            ViewBag.TongChiTieu = tongChiTieu;

            return View(khachHang);
        }

        // ==========================================
        // 3. THÊM MỚI KHÁCH HÀNG
        // ==========================================
        public IActionResult Create()
        {
            var khachHang = new KhachHang
            {
                QuocTich = "VN",
                TrangThai = true,
                GioiTinh = true
            };
            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang, string? matKhau)
        {
            // Kiểm tra trùng số điện thoại
            var trungSdt = await _context.KhachHangs.AnyAsync(k => k.SoDienThoai == khachHang.SoDienThoai);
            if (trungSdt)
            {
                ModelState.AddModelError("SoDienThoai", "Số điện thoại này đã được sử dụng bởi một khách hàng khác.");
            }

            // Kiểm tra trùng CCCD
            var trungCccd = await _context.KhachHangs.AnyAsync(k => k.SoGiayToTuyThan == khachHang.SoGiayToTuyThan);
            if (trungCccd)
            {
                ModelState.AddModelError("SoGiayToTuyThan", "Số giấy tờ tùy thân (CCCD) này đã tồn tại trong hệ thống.");
            }

            // Bỏ validate MaTaiKhoan vì sẽ tự sinh và bỏ qua giới hạn StringLength quá ngắn của Model gốc
            ModelState.Remove(nameof(khachHang.MaTaiKhoan));
            ModelState.Remove(nameof(khachHang.TaiKhoan));
            if (!string.IsNullOrEmpty(khachHang.HoTen) && khachHang.HoTen.Length <= 50)
                ModelState.Remove(nameof(khachHang.HoTen));
            if (!string.IsNullOrEmpty(khachHang.SoGiayToTuyThan) && khachHang.SoGiayToTuyThan.Length <= 30)
                ModelState.Remove(nameof(khachHang.SoGiayToTuyThan));
            if (!string.IsNullOrEmpty(khachHang.Email) && khachHang.Email.Length <= 50)
                ModelState.Remove(nameof(khachHang.Email));
            if (!string.IsNullOrEmpty(khachHang.DiaChi) && khachHang.DiaChi.Length <= 100)
                ModelState.Remove(nameof(khachHang.DiaChi));
            if (!string.IsNullOrEmpty(khachHang.SoDienThoai) && khachHang.SoDienThoai.Length <= 20)
                ModelState.Remove(nameof(khachHang.SoDienThoai));
            if (!string.IsNullOrEmpty(khachHang.QuocTich) && khachHang.QuocTich.Length <= 20)
                ModelState.Remove(nameof(khachHang.QuocTich));

            if (ModelState.IsValid)
            {
                // Tự động sinh tài khoản khách hàng để liên kết
                string username = "kh_" + khachHang.SoDienThoai.Trim();
                var taiKhoanTonTai = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TenDangNhap == username);
                if (taiKhoanTonTai == null)
                {
                    taiKhoanTonTai = new TaiKhoan
                    {
                        TenDangNhap = username,
                        MatKhau = string.IsNullOrWhiteSpace(matKhau) ? "123456" : matKhau,
                        HoTen = khachHang.HoTen,
                        Email = string.IsNullOrWhiteSpace(khachHang.Email) ? $"{username}@hotel.com" : khachHang.Email,
                        VaiTro = "KhachHang",
                        TrangThai = khachHang.TrangThai
                    };
                    _context.TaiKhoans.Add(taiKhoanTonTai);
                    await _context.SaveChangesAsync();
                }

                khachHang.MaTaiKhoan = taiKhoanTonTai.MaTaiKhoan;
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();

                TempData["ThongBaoThanhCong"] = $"Thêm mới khách hàng '{khachHang.HoTen}' thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(khachHang);
        }

        // ==========================================
        // 4. CHỈNH SỬA THÔNG TIN KHÁCH HÀNG
        // ==========================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var khachHang = await _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .FirstOrDefaultAsync(k => k.MaKhachHang == id);

            if (khachHang == null) return NotFound();

            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhachHang model)
        {
            if (id != model.MaKhachHang) return NotFound();

            var khachHangDb = await _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .FirstOrDefaultAsync(k => k.MaKhachHang == id);

            if (khachHangDb == null) return NotFound();

            // Kiểm tra trùng số điện thoại với người khác
            var trungSdt = await _context.KhachHangs
                .AnyAsync(k => k.SoDienThoai == model.SoDienThoai && k.MaKhachHang != id);
            if (trungSdt)
            {
                ModelState.AddModelError("SoDienThoai", "Số điện thoại này đã được sử dụng bởi khách hàng khác.");
            }

            // Kiểm tra trùng CCCD với người khác
            var trungCccd = await _context.KhachHangs
                .AnyAsync(k => k.SoGiayToTuyThan == model.SoGiayToTuyThan && k.MaKhachHang != id);
            if (trungCccd)
            {
                ModelState.AddModelError("SoGiayToTuyThan", "Số CCCD này đã tồn tại trong hệ thống.");
            }

            ModelState.Remove(nameof(model.MaTaiKhoan));
            ModelState.Remove(nameof(model.TaiKhoan));
            if (!string.IsNullOrEmpty(model.HoTen) && model.HoTen.Length <= 50)
                ModelState.Remove(nameof(model.HoTen));
            if (!string.IsNullOrEmpty(model.SoGiayToTuyThan) && model.SoGiayToTuyThan.Length <= 30)
                ModelState.Remove(nameof(model.SoGiayToTuyThan));
            if (!string.IsNullOrEmpty(model.Email) && model.Email.Length <= 50)
                ModelState.Remove(nameof(model.Email));
            if (!string.IsNullOrEmpty(model.DiaChi) && model.DiaChi.Length <= 100)
                ModelState.Remove(nameof(model.DiaChi));
            if (!string.IsNullOrEmpty(model.SoDienThoai) && model.SoDienThoai.Length <= 20)
                ModelState.Remove(nameof(model.SoDienThoai));
            if (!string.IsNullOrEmpty(model.QuocTich) && model.QuocTich.Length <= 20)
                ModelState.Remove(nameof(model.QuocTich));

            if (ModelState.IsValid)
            {
                khachHangDb.HoTen = model.HoTen;
                khachHangDb.NgaySinh = model.NgaySinh;
                khachHangDb.GioiTinh = model.GioiTinh;
                khachHangDb.SoDienThoai = model.SoDienThoai;
                khachHangDb.Email = model.Email;
                khachHangDb.DiaChi = model.DiaChi;
                khachHangDb.SoGiayToTuyThan = model.SoGiayToTuyThan;
                khachHangDb.QuocTich = model.QuocTich;
                khachHangDb.TrangThai = model.TrangThai;

                // Đồng bộ sang tài khoản đăng nhập nếu có
                if (khachHangDb.TaiKhoan != null)
                {
                    khachHangDb.TaiKhoan.HoTen = model.HoTen;
                    if (!string.IsNullOrWhiteSpace(model.Email))
                    {
                        khachHangDb.TaiKhoan.Email = model.Email;
                    }
                    khachHangDb.TaiKhoan.TrangThai = model.TrangThai;
                }

                await _context.SaveChangesAsync();
                TempData["ThongBaoThanhCong"] = $"Cập nhật hồ sơ khách hàng '{model.HoTen}' thành công!";
                return RedirectToAction(nameof(Details), new { id = model.MaKhachHang });
            }

            return View(model);
        }

        // ==========================================
        // 5. XÓA HOẶC KHÓA KHÁCH HÀNG
        // ==========================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var khachHang = await _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .Include(k => k.DatPhongs)
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);

            if (khachHang == null) return NotFound();

            return View(khachHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs
                .Include(k => k.DatPhongs)
                .FirstOrDefaultAsync(k => k.MaKhachHang == id);

            if (khachHang != null)
            {
                // Kiểm tra ràng buộc: Đang có đặt phòng đang hoạt động
                var dangLuuTru = khachHang.DatPhongs.Any(d =>
                    d.TrangThai == "Đã nhận phòng" || d.TrangThai == "Đã xác nhận");

                if (dangLuuTru)
                {
                    TempData["ThongBaoLoi"] = "Không thể xóa khách hàng đang có phòng lưu trú hoặc đang có đơn đặt phòng đã xác nhận!";
                    return RedirectToAction(nameof(Details), new { id });
                }

                // Nếu có lịch sử đặt phòng cũ, ưu tiên vô hiệu hóa tài khoản để bảo toàn dữ liệu hóa đơn & thống kê
                if (khachHang.DatPhongs.Any())
                {
                    khachHang.TrangThai = false;
                    var taiKhoan = await _context.TaiKhoans.FindAsync(khachHang.MaTaiKhoan);
                    if (taiKhoan != null)
                    {
                        taiKhoan.TrangThai = false;
                    }
                    await _context.SaveChangesAsync();
                    TempData["ThongBaoThanhCong"] = $"Khách hàng '{khachHang.HoTen}' đã có lịch sử đặt phòng nên hệ thống đã chuyển trạng thái sang [Đã Khóa/Vô hiệu hóa] để bảo toàn dữ liệu!";
                }
                else
                {
                    var taiKhoan = await _context.TaiKhoans.FindAsync(khachHang.MaTaiKhoan);
                    _context.KhachHangs.Remove(khachHang);
                    if (taiKhoan != null && taiKhoan.VaiTro == "KhachHang")
                    {
                        _context.TaiKhoans.Remove(taiKhoan);
                    }
                    await _context.SaveChangesAsync();
                    TempData["ThongBaoThanhCong"] = $"Đã xóa thành công khách hàng #{id} khỏi hệ thống.";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
