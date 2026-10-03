// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Quản lý đăng nhập, phiên làm việc Session và phân quyền cho hệ thống lưu trú

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_Model4.Data;

namespace QuanLyDatPhong_Model4.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly QuanLyDatPhongDbContext _context;

        public TaiKhoanController(QuanLyDatPhongDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DangNhap(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap(string tenDangNhap, string matKhau, string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                ViewBag.Error = "Tên đăng nhập và mật khẩu không được để trống.";
                return View();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau);

            if (taiKhoan == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return View();
            }

            if (!taiKhoan.TrangThai)
            {
                ViewBag.Error = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin.";
                return View();
            }

            // Lưu vào Session (Section 5.2)
            HttpContext.Session.SetString("MaTaiKhoan", taiKhoan.MaTaiKhoan.ToString());
            HttpContext.Session.SetString("TenDangNhap", taiKhoan.TenDangNhap);
            HttpContext.Session.SetString("HoTen", taiKhoan.HoTen);
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "DatPhong");
        }

        [HttpGet]
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }

        // Chức năng chuyển đổi nhanh vai trò kiểm thử tiện lợi khi chấm bài
        [HttpGet]
        public async Task<IActionResult> ChuyenNhanhVaiTro(string vaiTro)
        {
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.VaiTro == vaiTro && t.TrangThai);
            if (tk != null)
            {
                HttpContext.Session.SetString("MaTaiKhoan", tk.MaTaiKhoan.ToString());
                HttpContext.Session.SetString("TenDangNhap", tk.TenDangNhap);
                HttpContext.Session.SetString("HoTen", tk.HoTen);
                HttpContext.Session.SetString("VaiTro", tk.VaiTro);
                TempData["SuccessMessage"] = $"Đã chuyển nhanh sang vai trò: {tk.VaiTro} ({tk.HoTen})";
            }
            return RedirectToAction("Index", "DatPhong");
        }
    }
}
