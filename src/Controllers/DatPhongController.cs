
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class DatPhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatPhongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy mã tài khoản đang đăng nhập
        private int? LayMaTaiKhoan()
        {
            return HttpContext.Session.GetInt32("MaTaiKhoan");
        }

        // Lấy hồ sơ cá nhân của tài khoản
        private async Task<KhachHang?> LayKhachHangAsync(int maTaiKhoan)
        {
            return await _context.KhachHangs
                .FirstOrDefaultAsync(k => k.MaTaiKhoan == maTaiKhoan);
        }

        // Nạp dữ liệu phòng và thông tin cá nhân vào form
        private void NapForm(Phong phong, KhachHang khachHang, DateTime? ngayNhanPhong = null, DateTime? ngayTraPhong = null, int soNguoi = 1, string? yeuCauDacBiet = null)
        {
            DateTime ngayNhan = ngayNhanPhong?? DateTime.Today.AddDays(1);
            DateTime ngayTra = ngayTraPhong ?? DateTime.Today.AddDays(2);
            ViewBag.MaPhong = phong.MaPhong;
            ViewBag.SoPhong = phong.SoPhong;
            ViewBag.TenLoaiPhong = phong.LoaiPhong?.TenLoaiPhong;
            ViewBag.GiaMotDem = phong.GiaMotDem;
            ViewBag.HoTen = khachHang.HoTen;
            ViewBag.SoDienThoai = khachHang.SoDienThoai;
            ViewBag.Email = khachHang.Email;
            ViewBag.SoGiayToTuyThan = khachHang.SoGiayToTuyThan;
            ViewBag.DiaChi = khachHang.DiaChi;
            ViewBag.QuocTich = khachHang.QuocTich;
            ViewBag.NgayNhanPhong = ngayNhan.ToString("yyyy-MM-dd");
            ViewBag.NgayTraPhong = ngayTra.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi;
            ViewBag.SoDem = Math.Max(0, (ngayTra - ngayNhan).Days);
            ViewBag.TongTienDuKien =Math.Max(0, (ngayTra - ngayNhan).Days)* phong.GiaMotDem;
            ViewBag.YeuCauDacBiet = yeuCauDacBiet;
        }

        // GET: DatPhong/DatPhong?maPhong=1
        [HttpGet]
        public async Task<IActionResult> DatPhong(int maPhong)
        {
            int? maTaiKhoan = LayMaTaiKhoan();
            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");
            var phong = await _context.Phongs.Include(p => p.LoaiPhong).FirstOrDefaultAsync(p => p.MaPhong == maPhong);
            if (phong == null)
                return NotFound("Không tìm thấy phòng.");
            if (phong.TrangThai != "Sẵn sàng")
            {
                TempData["ThongBao"] = "Phòng hiện không ở trạng thái sẵn sàng.";

                return RedirectToAction("Index", "Home");
            }
            var khachHang = await LayKhachHangAsync(maTaiKhoan.Value);
            if (khachHang == null)
            {
                TempData["ThongBao"] = "Bạn chưa có hồ sơ khách hàng. Vui lòng cập nhật hồ sơ trước khi đặt phòng.";
                return RedirectToAction("Index", "Home");
            }
            if (khachHang.TrangThai != "Hoạt động")
            {
                TempData["ThongBao"] =
                    "Hồ sơ khách hàng của bạn hiện không hoạt động.";

                return RedirectToAction("Index", "Home");
            }
            NapForm(phong, khachHang);
            return View("~/Views/DatPhong/DatPhong.cshtml");
        }
        // POST: Tạo đơn đặt phòng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatPhong(
            int maPhong,
            DateTime ngayNhanPhong,
            DateTime ngayTraPhong,
            int soNguoi,
            string? yeuCauDacBiet)
        {
            int? maTaiKhoan = LayMaTaiKhoan();
            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");
            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);
            if (phong == null)
                return NotFound("Không tìm thấy phòng.");
            var khachHang = await LayKhachHangAsync(maTaiKhoan.Value);
            if (khachHang == null)
            {
                TempData["ThongBao"] = "Không tìm thấy hồ sơ cá nhân. Vui lòng cập nhật hồ sơ trước khi đặt phòng.";
                return RedirectToAction("Index", "Home");
            }
            if (khachHang.TrangThai != "Hoạt động")
            {
                TempData["ThongBao"] = "Hồ sơ khách hàng của bạn hiện không hoạt động.";
                return RedirectToAction("Index", "Home");
            }
            yeuCauDacBiet = yeuCauDacBiet?.Trim();
            if (phong.TrangThai != "Sẵn sàng")
            {
                TempData["ThongBao"] = "Phòng hiện không ở trạng thái sẵn sàng.";
                return RedirectToAction("Index", "Home");
            }
            DateTime ngayNhan = ngayNhanPhong.Date;
            DateTime ngayTra = ngayTraPhong.Date;
            if (ngayNhan < DateTime.Today)
            {
                ModelState.AddModelError(
                    "ngayNhanPhong",
                    "Ngày nhận phòng không được nằm trong quá khứ.");
            }
            if (ngayTra <= ngayNhan)
            {
                ModelState.AddModelError(
                    "ngayTraPhong",
                    "Ngày trả phòng phải sau ngày nhận phòng.");
            }
            if (soNguoi < 1 || soNguoi > phong.SucChua)
            {
                ModelState.AddModelError(
                    "soNguoi",
                    "Số người phải lớn hơn 0 và không vượt sức chứa phòng.");
            }
            if (!string.IsNullOrWhiteSpace(yeuCauDacBiet) &&
                yeuCauDacBiet.Length > 10)
            {
                ModelState.AddModelError(
                    "yeuCauDacBiet",
                    "Yêu cầu đặc biệt không được vượt quá 10 ký tự.");
            }
            if (!ModelState.IsValid)
            {
                NapForm(
                    phong,
                    khachHang,
                    ngayNhanPhong,
                    ngayTraPhong,
                    soNguoi,
                    yeuCauDacBiet);
                return View("~/Views/DatPhong/DatPhong.cshtml");
            }
            string[] trangThaiHieuLuc =
            {
                "Chờ xác nhận",
                "Đã xác nhận",
                "Đã nhận phòng"
            };
            bool biTrungLich = await _context.DatPhongs.AnyAsync(d =>
                d.MaPhong == maPhong &&
                trangThaiHieuLuc.Contains(d.TrangThai) &&
                ngayNhan < d.NgayTraPhong &&
                ngayTra > d.NgayNhanPhong);
            if (biTrungLich)
            {
                ModelState.AddModelError( "", "Phòng đã có đơn đặt trong thời gian này.");
                NapForm(
                    phong,
                    khachHang,
                    ngayNhanPhong,
                    ngayTraPhong,
                    soNguoi,
                    yeuCauDacBiet);
                return View("~/Views/DatPhong/DatPhong.cshtml");
            }
            int soDem = (ngayTra - ngayNhan).Days;
            decimal tongTien = soDem * phong.GiaMotDem;
            var datPhong = new DatPhong
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaPhong = phong.MaPhong,
                NgayNhanPhong = ngayNhan,
                NgayTraPhong = ngayTra,
                SoNguoi = soNguoi,
                YeuCauDacBiet = string.IsNullOrWhiteSpace(yeuCauDacBiet)? null : yeuCauDacBiet,
                NgayDat = DateTime.Now,
                TongTienDuKien = tongTien,
                TrangThai = "Chờ xác nhận",
                GhiChuLeTan = ""
            };
            _context.DatPhongs.Add(datPhong);
            await _context.SaveChangesAsync();
            TempData["ThongBao"] = "Đặt phòng thành công. Vui lòng chờ xác nhận.";
            string? vaiTro = HttpContext.Session.GetString("VaiTro");
            if (vaiTro == "KhachHang")
            {
                return RedirectToAction(nameof(LichSu));
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Hủy đơn đặt phòng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Huy(int id)
        {
            int? maTaiKhoan = LayMaTaiKhoan();
            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");
            var datPhong = await _context.DatPhongs.FirstOrDefaultAsync(d => d.MaDatPhong == id && _context.KhachHangs.Any(k => k.MaKhachHang == d.MaKhachHang && k.MaTaiKhoan == maTaiKhoan.Value));
            if (datPhong == null)
            {
                TempData["ThongBao"] = "Không tìm thấy đơn đặt phòng thuộc tài khoản của bạn.";
                return RedirectToAction(nameof(LichSu));
            }
            bool dungTrangThai = datPhong.TrangThai == "Chờ xác nhận" || datPhong.TrangThai == "Đã xác nhận";
            if (!dungTrangThai || datPhong.NgayNhanPhong.Date <= DateTime.Today)
            {
                TempData["ThongBao"] = "Đơn đặt phòng không đủ điều kiện hủy.";
                return RedirectToAction(nameof(LichSu));
            }
            datPhong.TrangThai = "Đã hủy";
            await _context.SaveChangesAsync();
            TempData["ThongBao"] = "Hủy đặt phòng thành công.";
            return RedirectToAction(nameof(LichSu));
        }
        // GET: Lịch sử đặt phòng cá nhân
        [HttpGet]
        public async Task<IActionResult> LichSu()
        {
            int? maTaiKhoan = LayMaTaiKhoan();
            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");
            var maKhachHang = await _context.KhachHangs.Where(k => k.MaTaiKhoan == maTaiKhoan.Value).Select(k => k.MaKhachHang).ToListAsync();
            var danhSach = await _context.DatPhongs.Include(d => d.Phong).ThenInclude(p => p!.LoaiPhong).Include(d => d.HoaDon).Where(d => maKhachHang.Contains(d.MaKhachHang)).OrderByDescending(d => d.NgayDat).ToListAsync();
            return View("~/Views/DatPhong/LichSu.cshtml", danhSach);
        }
    }
}
