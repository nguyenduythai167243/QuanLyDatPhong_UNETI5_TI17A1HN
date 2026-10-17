using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
	public class AccountController : Controller
	{

		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(
			string tenDangNhap,
			string matKhau)
		{

			if (string.IsNullOrWhiteSpace(tenDangNhap)
				|| string.IsNullOrWhiteSpace(matKhau))
			{
				ViewBag.ThongBao =
					$"Dữ liệu nhận được không đầy đủ. " +
					$"Tên đăng nhập có dữ liệu: {!string.IsNullOrWhiteSpace(tenDangNhap)}; " +
					$"Mật khẩu có dữ liệu: {!string.IsNullOrWhiteSpace(matKhau)}.";

				return View();
			}


			var taiKhoan = await _context.TaiKhoans
				.FirstOrDefaultAsync(t =>
					t.TenDangNhap == tenDangNhap);

			if (taiKhoan == null || taiKhoan.MatKhau != matKhau)
			{
				ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng.";
				return View();
			}

			if (!taiKhoan.TrangThai)
			{
				ViewBag.ThongBao = "Tài khoản đã bị khóa.";
				return View();
			}

			HttpContext.Session.SetInt32(
				"MaTaiKhoan", taiKhoan.MaTaiKhoan);

			HttpContext.Session.SetString(
				"HoTen", taiKhoan.HoTen);

			HttpContext.Session.SetString(
				"VaiTro", taiKhoan.VaiTro);

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();

			return RedirectToAction("Login", "Account");
		}
	}
}
