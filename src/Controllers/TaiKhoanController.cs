using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    /// <summary>
    /// Controller quản lý tài khoản người dùng dành cho Quản trị viên (Admin).
    /// </summary>
    [SessionAuthorize("Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanController(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        private static string? ChuanHoaVaiTro(string? vaiTro)
        {
            if (string.IsNullOrWhiteSpace(vaiTro)) return null;

            string v = vaiTro.Trim();
            if (string.Equals(v, "Admin", StringComparison.OrdinalIgnoreCase)) return "Admin";
            if (string.Equals(v, "LeTan", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(v, "Lễ tân", StringComparison.OrdinalIgnoreCase)) return "LeTan";
            if (string.Equals(v, "KhachHang", StringComparison.OrdinalIgnoreCase)) return "KhachHang";

            return null;
        }

        private void NapDanhSachVaiTro(string? selected = null)
        {
            var items = new[]
            {
                new { Value = "Admin", Text = "Quản trị viên (Admin)" },
                new { Value = "LeTan", Text = "Lễ tân" },
                new { Value = "KhachHang", Text = "Khách hàng" }
            };

            ViewBag.VaiTroList = new SelectList(items, "Value", "Text", ChuanHoaVaiTro(selected) ?? selected);
        }

        // GET: TaiKhoan
        public async Task<IActionResult> Index(string? tuKhoa, string? vaiTro)
        {
            var danhSach = await _taiKhoanRepository.SearchAsync(tuKhoa, vaiTro);

            ViewBag.TuKhoa = tuKhoa;
            ViewBag.VaiTro = vaiTro;
            ViewBag.CurrentUserId = HttpContext.Session.GetInt32("MaTaiKhoan");
            NapDanhSachVaiTro(vaiTro);

            return View(danhSach);
        }

        // GET: TaiKhoan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(id.Value);
            if (taiKhoan == null) return NotFound();

            ViewBag.CoKhachHangLienQuan = await _taiKhoanRepository.HasKhachHangAsync(id.Value);
            ViewBag.LaTaiKhoanHienTai = HttpContext.Session.GetInt32("MaTaiKhoan") == id.Value;

            return View(taiKhoan);
        }

        // GET: TaiKhoan/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new TaiKhoan
            {
                VaiTro = "LeTan",
                TrangThai = true
            };

            NapDanhSachVaiTro(model.VaiTro);
            return View(model);
        }

        // POST: TaiKhoan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("TenDangNhap,MatKhau,HoTen,Email,VaiTro,TrangThai")] TaiKhoan taiKhoan)
        {
            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap?.Trim() ?? string.Empty;
            taiKhoan.HoTen = taiKhoan.HoTen?.Trim() ?? string.Empty;
            taiKhoan.Email = taiKhoan.Email?.Trim() ?? string.Empty;

            string? vaiTroHopLe = ChuanHoaVaiTro(taiKhoan.VaiTro);
            if (vaiTroHopLe == null)
            {
                ModelState.AddModelError(nameof(TaiKhoan.VaiTro), "Vai trò không hợp lệ.");
            }
            else
            {
                taiKhoan.VaiTro = vaiTroHopLe;
            }

            if (!string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap) &&
                await _taiKhoanRepository.ExistsTenDangNhapAsync(taiKhoan.TenDangNhap))
            {
                ModelState.AddModelError(nameof(TaiKhoan.TenDangNhap), "Tên đăng nhập đã tồn tại trong hệ thống.");
            }

            if (!ModelState.IsValid)
            {
                NapDanhSachVaiTro(taiKhoan.VaiTro);
                return View(taiKhoan);
            }

            await _taiKhoanRepository.AddAsync(taiKhoan);
            TempData["Success"] = $"Đã thêm tài khoản '{taiKhoan.TenDangNhap}' thành công.";

            return RedirectToAction(nameof(Index));
        }

        // GET: TaiKhoan/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(id.Value);
            if (taiKhoan == null) return NotFound();

            taiKhoan.MatKhau = string.Empty;
            ViewBag.LaTaiKhoanHienTai = HttpContext.Session.GetInt32("MaTaiKhoan") == id.Value;
            NapDanhSachVaiTro(taiKhoan.VaiTro);

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("MaTaiKhoan,TenDangNhap,MatKhau,HoTen,Email,VaiTro,TrangThai")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaTaiKhoan) return NotFound();

            var existing = await _taiKhoanRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap?.Trim() ?? string.Empty;
            taiKhoan.HoTen = taiKhoan.HoTen?.Trim() ?? string.Empty;
            taiKhoan.Email = taiKhoan.Email?.Trim() ?? string.Empty;

            // Cho phép giữ nguyên mật khẩu hiện tại nếu để trống ô mật khẩu.
            bool giuMatKhauCu = string.IsNullOrWhiteSpace(taiKhoan.MatKhau);
            if (giuMatKhauCu)
            {
                ModelState.Remove(nameof(TaiKhoan.MatKhau));
            }

            string? vaiTroHopLe = ChuanHoaVaiTro(taiKhoan.VaiTro);
            if (vaiTroHopLe == null)
            {
                ModelState.AddModelError(nameof(TaiKhoan.VaiTro), "Vai trò không hợp lệ.");
            }
            else
            {
                taiKhoan.VaiTro = vaiTroHopLe;
            }

            if (!string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap) &&
                await _taiKhoanRepository.ExistsTenDangNhapExceptAsync(taiKhoan.TenDangNhap, id))
            {
                ModelState.AddModelError(nameof(TaiKhoan.TenDangNhap), "Tên đăng nhập đã được sử dụng bởi tài khoản khác.");
            }

            int? currentUserId = HttpContext.Session.GetInt32("MaTaiKhoan");
            bool laTaiKhoanHienTai = currentUserId == id;

            if (laTaiKhoanHienTai && !taiKhoan.TrangThai)
            {
                ModelState.AddModelError(nameof(TaiKhoan.TrangThai), "Không thể tự khóa tài khoản đang đăng nhập.");
            }

            if (laTaiKhoanHienTai && !string.Equals(taiKhoan.VaiTro, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(nameof(TaiKhoan.VaiTro), "Không thể tự thay đổi vai trò Admin của tài khoản đang đăng nhập.");
            }

            if (!ModelState.IsValid)
            {
                taiKhoan.MatKhau = string.Empty;
                ViewBag.LaTaiKhoanHienTai = laTaiKhoanHienTai;
                NapDanhSachVaiTro(taiKhoan.VaiTro);
                return View(taiKhoan);
            }

            existing.TenDangNhap = taiKhoan.TenDangNhap;
            existing.HoTen = taiKhoan.HoTen;
            existing.Email = taiKhoan.Email;
            existing.VaiTro = taiKhoan.VaiTro;
            existing.TrangThai = taiKhoan.TrangThai;

            if (!giuMatKhauCu)
            {
                existing.MatKhau = taiKhoan.MatKhau;
            }

            await _taiKhoanRepository.SaveAsync();

            if (laTaiKhoanHienTai)
            {
                HttpContext.Session.SetString("HoTen", existing.HoTen);
            }

            TempData["Success"] = $"Đã cập nhật tài khoản '{existing.TenDangNhap}' thành công.";
            return RedirectToAction(nameof(Index));
        }

        // POST: TaiKhoan/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            if (HttpContext.Session.GetInt32("MaTaiKhoan") == id)
            {
                TempData["Error"] = "Không thể tự khóa tài khoản đang đăng nhập.";
                return RedirectToAction(nameof(Index));
            }

            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(id);
            if (taiKhoan == null) return NotFound();

            taiKhoan.TrangThai = !taiKhoan.TrangThai;
            await _taiKhoanRepository.SaveAsync();

            TempData["Success"] = taiKhoan.TrangThai
                ? $"Đã mở khóa tài khoản '{taiKhoan.TenDangNhap}'."
                : $"Đã khóa tài khoản '{taiKhoan.TenDangNhap}'.";

            return RedirectToAction(nameof(Index));
        }

        // GET: TaiKhoan/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(id.Value);
            if (taiKhoan == null) return NotFound();

            ViewBag.CoKhachHangLienQuan = await _taiKhoanRepository.HasKhachHangAsync(id.Value);
            ViewBag.LaTaiKhoanHienTai = HttpContext.Session.GetInt32("MaTaiKhoan") == id.Value;

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("MaTaiKhoan") == id)
            {
                TempData["Error"] = "Không thể xóa tài khoản đang đăng nhập.";
                return RedirectToAction(nameof(Index));
            }

            var taiKhoan = await _taiKhoanRepository.GetByIdAsync(id);
            if (taiKhoan == null) return NotFound();

            if (await _taiKhoanRepository.HasKhachHangAsync(id))
            {
                TempData["Error"] = $"Không thể xóa tài khoản '{taiKhoan.TenDangNhap}' vì đang liên kết với hồ sơ khách hàng. Vui lòng dùng chức năng Khóa tài khoản.";
                return RedirectToAction(nameof(Index));
            }

            await _taiKhoanRepository.DeleteAsync(id);
            TempData["Success"] = $"Đã xóa tài khoản '{taiKhoan.TenDangNhap}' thành công.";

            return RedirectToAction(nameof(Index));
        }
    }
}

