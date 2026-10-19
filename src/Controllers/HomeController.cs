using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. TRANG CHỦ KHÁCH SẠN (PORTAL KHÁCH HÀNG)
        // ==========================================
        public async Task<IActionResult> Index(int? maLoaiPhong, DateTime? ngayNhan, DateTime? ngayTra, int? soNguoi)
        {
            ngayNhan ??= DateTime.Today;
            ngayTra ??= DateTime.Today.AddDays(1);

            ViewBag.LoaiPhongs = await _context.LoaiPhongs.ToListAsync();
            ViewBag.NgayNhan = ngayNhan.Value.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = ngayTra.Value.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi ?? 2;
            ViewBag.MaLoaiPhong = maLoaiPhong;

            // Lấy danh sách phòng nổi bật có thể đặt
            var query = _context.Phongs
                .Include(p => p.LoaiPhong)
                .AsQueryable();

            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (soNguoi.HasValue && soNguoi.Value > 0)
            {
                query = query.Where(p => p.SucChua >= soNguoi.Value);
            }

            var phongs = await query.Take(6).ToListAsync();
            return View(phongs);
        }

        // ==========================================
        // 2. DANH SÁCH TẤT CẢ PHÒNG & TÌM PHÒNG TRỐNG
        // ==========================================
        public async Task<IActionResult> DanhSachPhong(
            int? maLoaiPhong,
            DateTime? ngayNhan,
            DateTime? ngayTra,
            int? soNguoi,
            decimal? giaTu,
            decimal? giaDen)
        {
            ngayNhan ??= DateTime.Today;
            ngayTra ??= DateTime.Today.AddDays(1);

            var query = _context.Phongs
                .Include(p => p.LoaiPhong)
                .AsQueryable();

            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (soNguoi.HasValue && soNguoi.Value > 0)
            {
                query = query.Where(p => p.SucChua >= soNguoi.Value);
            }

            if (giaTu.HasValue)
            {
                query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            }

            if (giaDen.HasValue)
            {
                query = query.Where(p => p.GiaMotDem <= giaDen.Value);
            }

            // Kiểm tra phòng bận trong khoảng thời gian đã chọn
            var checkIn = ngayNhan.Value.Date;
            var checkOut = ngayTra.Value.Date;

            var phongBanIds = await _context.DatPhongs
                .Where(d => (d.TrangThai == "Đã xác nhận" || d.TrangThai == "Đã nhận phòng")
                            && d.NgayNhanPhong.Date < checkOut
                            && d.NgayTraPhong.Date > checkIn)
                .Select(d => d.MaPhong)
                .Distinct()
                .ToListAsync();

            ViewBag.PhongBanIds = phongBanIds;
            ViewBag.LoaiPhongs = await _context.LoaiPhongs.ToListAsync();
            ViewBag.NgayNhan = checkIn.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = checkOut.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi;
            ViewBag.MaLoaiPhong = maLoaiPhong;
            ViewBag.GiaTu = giaTu;
            ViewBag.GiaDen = giaDen;

            var danhSach = await query.ToListAsync();
            return View(danhSach);
        }

        // ==========================================
        // 3. CHI TIẾT PHÒNG
        // ==========================================
        public async Task<IActionResult> ChiTietPhong(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == id);

            if (phong == null) return NotFound();

            // Lấy các phòng liên quan cùng loại phòng
            ViewBag.PhongLienQuan = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .Where(p => p.MaLoaiPhong == phong.MaLoaiPhong && p.MaPhong != phong.MaPhong)
                .Take(3)
                .ToListAsync();

            return View(phong);
        }

        // ==========================================
        // 4. FORM ĐẶT PHÒNG TRỰC TUYẾN DÀNH CHO KHÁCH
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> DatPhong(int? maPhong, DateTime? ngayNhan, DateTime? ngayTra, int? soNguoi)
        {
            if (maPhong == null)
            {
                return RedirectToAction(nameof(DanhSachPhong));
            }

            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);

            if (phong == null) return NotFound();

            var checkIn = ngayNhan ?? DateTime.Today;
            var checkOut = ngayTra ?? checkIn.AddDays(1);
            if (checkOut <= checkIn) checkOut = checkIn.AddDays(1);

            var model = new DatPhongOnlineViewModel
            {
                MaPhong = phong.MaPhong,
                Phong = phong,
                NgayNhanPhong = checkIn,
                NgayTraPhong = checkOut,
                SoNguoi = soNguoi ?? 1
            };

            // Nếu khách hàng đã đăng nhập, tự động điền thông tin sẵn
            var maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan.HasValue)
            {
                var khachHang = await _context.KhachHangs
                    .FirstOrDefaultAsync(k => k.MaTaiKhoan == maTaiKhoan.Value);

                if (khachHang != null)
                {
                    model.HoTen = khachHang.HoTen;
                    model.SoDienThoai = khachHang.SoDienThoai;
                    model.Email = khachHang.Email;
                    model.SoGiayToTuyThan = khachHang.SoGiayToTuyThan;
                    model.DiaChi = khachHang.DiaChi;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatPhong(DatPhongOnlineViewModel model)
        {
            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == model.MaPhong);

            if (phong == null)
            {
                ModelState.AddModelError("", "Phòng đã chọn không tồn tại.");
                return View(model);
            }
            model.Phong = phong;

            // Kiểm tra ngày hợp lệ
            if (model.NgayNhanPhong.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.NgayNhanPhong), "Ngày nhận phòng không thể trong quá khứ.");
            }

            if (model.NgayTraPhong.Date <= model.NgayNhanPhong.Date)
            {
                ModelState.AddModelError(nameof(model.NgayTraPhong), "Ngày trả phòng phải sau ngày nhận phòng ít nhất 1 ngày.");
            }

            if (model.SoNguoi > phong.SucChua)
            {
                ModelState.AddModelError(nameof(model.SoNguoi), $"Số người ({model.SoNguoi}) vượt quá sức chứa tối đa của phòng ({phong.SucChua} người).");
            }

            // Kiểm tra phòng có bị trùng lịch đã xác nhận hoặc đang lưu trú không
            var isConflict = await _context.DatPhongs
                .AnyAsync(d => d.MaPhong == model.MaPhong
                            && (d.TrangThai == "Đã xác nhận" || d.TrangThai == "Đã nhận phòng")
                            && d.NgayNhanPhong.Date < model.NgayTraPhong.Date
                            && d.NgayTraPhong.Date > model.NgayNhanPhong.Date);

            if (isConflict)
            {
                ModelState.AddModelError("", $"Rất tiếc! Phòng {phong.SoPhong} đã có khách đặt trong khoảng thời gian từ {model.NgayNhanPhong:dd/MM/yyyy} đến {model.NgayTraPhong:dd/MM/yyyy}. Quý khách vui lòng chọn ngày khác hoặc phòng khác.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1. Tìm hoặc tạo mới hồ sơ Khách hàng
            var sdt = model.SoDienThoai.Trim();
            var cccd = model.SoGiayToTuyThan.Trim();

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(k => k.SoDienThoai == sdt || k.SoGiayToTuyThan == cccd);

            if (khachHang == null)
            {
                // Kiểm tra tài khoản
                string username = "kh_" + sdt;
                var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TenDangNhap == username);
                if (taiKhoan == null)
                {
                    taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = username,
                        MatKhau = "123456",
                        HoTen = model.HoTen.Trim(),
                        Email = string.IsNullOrWhiteSpace(model.Email) ? $"{username}@hotel.com" : model.Email.Trim(),
                        VaiTro = "KhachHang",
                        TrangThai = true
                    };
                    _context.TaiKhoans.Add(taiKhoan);
                    await _context.SaveChangesAsync();
                }

                khachHang = new KhachHang
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    HoTen = model.HoTen.Trim(),
                    SoDienThoai = sdt,
                    SoGiayToTuyThan = cccd,
                    Email = model.Email?.Trim() ?? $"{username}@hotel.com",
                    DiaChi = model.DiaChi?.Trim() ?? "Chưa cung cấp",
                    QuocTich = "VN",
                    GioiTinh = true,
                    TrangThai = true
                };
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Cập nhật thông tin bổ sung nếu trước đó còn thiếu
                if (string.IsNullOrEmpty(khachHang.DiaChi) && !string.IsNullOrEmpty(model.DiaChi))
                {
                    khachHang.DiaChi = model.DiaChi.Trim();
                }
                if (string.IsNullOrEmpty(khachHang.Email) && !string.IsNullOrEmpty(model.Email))
                {
                    khachHang.Email = model.Email.Trim();
                }
                await _context.SaveChangesAsync();
            }

            // 2. Tính tiền dự kiến
            int soDem = Math.Max(1, (int)(model.NgayTraPhong.Date - model.NgayNhanPhong.Date).TotalDays);
            decimal tongTien = phong.GiaMotDem * soDem;

            // 3. Tạo bản ghi Đặt phòng mới (Trạng thái: "Chờ xác nhận")
            var datPhong = new DatPhong
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaPhong = phong.MaPhong,
                NgayNhanPhong = model.NgayNhanPhong.Date,
                NgayTraPhong = model.NgayTraPhong.Date,
                SoNguoi = model.SoNguoi,
                YeuCauDacBiet = model.YeuCauDacBiet,
                NgayDat = DateTime.Now,
                TongTienDuKien = tongTien,
                TrangThai = "Chờ xác nhận",
                GhiChuLeTan = "Online"
            };

            _context.DatPhongs.Add(datPhong);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(XacNhanDatPhong), new { id = datPhong.MaDatPhong });
        }

        // ==========================================
        // 5. TRANG XÁC NHẬN ĐẶT PHÒNG THÀNH CÔNG
        // ==========================================
        public async Task<IActionResult> XacNhanDatPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhongs
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatPhong == id);

            if (datPhong == null) return NotFound();

            return View(datPhong);
        }

        // ==========================================
        // 6. TRA CỨU ĐƠN ĐẶT PHÒNG CHO KHÁCH
        // ==========================================
        public async Task<IActionResult> TraCuu(string? tuKhoa)
        {
            List<DatPhong> ketQua = new();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim();
                var cleanKeyword = tuKhoa.ToUpper().Replace("#", "").Replace("DP", "").Trim();

                var query = _context.DatPhongs
                    .Include(d => d.Phong)
                        .ThenInclude(p => p!.LoaiPhong)
                    .Include(d => d.KhachHang)
                    .Include(d => d.HoaDon)
                    .AsQueryable();

                if (int.TryParse(cleanKeyword, out int maDatPhong))
                {
                    query = query.Where(d => d.MaDatPhong == maDatPhong || (d.KhachHang != null && (d.KhachHang.SoDienThoai.Contains(tuKhoa) || d.KhachHang.SoGiayToTuyThan.Contains(tuKhoa))));
                }
                else
                {
                    query = query.Where(d => d.KhachHang != null && (d.KhachHang.SoDienThoai.Contains(tuKhoa) || d.KhachHang.SoGiayToTuyThan.Contains(tuKhoa)));
                }

                ketQua = await query.OrderByDescending(d => d.NgayDat).ToListAsync();
            }

            ViewBag.TuKhoa = tuKhoa;
            return View(ketQua);
        }

        // ==========================================
        // 7. LỊCH SỬ ĐẶT PHÒNG CỦA KHÁCH ĐÃ ĐĂNG NHẬP
        // ==========================================
        public async Task<IActionResult> LichSuDatPhong()
        {
            var maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var khachHang = await _context.KhachHangs
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.Phong)
                        .ThenInclude(p => p!.LoaiPhong)
                .Include(k => k.DatPhongs)
                    .ThenInclude(d => d.HoaDon)
                .FirstOrDefaultAsync(k => k.MaTaiKhoan == maTaiKhoan.Value);

            if (khachHang == null)
            {
                ViewBag.ThongBao = "Tài khoản hiện tại chưa được liên kết với hồ sơ khách hàng.";
                return View(new List<DatPhong>());
            }

            var danhSach = khachHang.DatPhongs.OrderByDescending(d => d.NgayDat).ToList();
            return View(danhSach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
