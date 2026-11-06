
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class DatPhongController : Controller
    {
        private readonly IDatPhongRepository _datPhongRepository;

        public DatPhongController(IDatPhongRepository datPhongRepository)
        {
            _datPhongRepository = datPhongRepository;
        }

        // Lấy mã tài khoản đang đăng nhập
        private int? LayMaTaiKhoan()
        {
            return HttpContext.Session.GetInt32("MaTaiKhoan");
        }

        // Nạp dữ liệu phòng và thông tin cá nhân vào form
        private void NapForm(
            Phong phong,
            KhachHang khachHang,
            DateTime? ngayNhanPhong = null,
            DateTime? ngayTraPhong = null,
            int soNguoi = 1,
            string? yeuCauDacBiet = null)
        {
            DateTime ngayNhan = ngayNhanPhong ?? DateTime.Today.AddDays(1);
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
            ViewBag.TongTienDuKien =
                Math.Max(0, (ngayTra - ngayNhan).Days) * phong.GiaMotDem;

            ViewBag.YeuCauDacBiet = yeuCauDacBiet;
        }

        // GET: DatPhong/DatPhong?maPhong=1
        [HttpGet]
        public async Task<IActionResult> DatPhong(int maPhong)
        {
            int? maTaiKhoan = LayMaTaiKhoan();

            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");

            // Lấy thông tin phòng
            var phong = await _datPhongRepository.GetPhongAsync(maPhong);

            if (phong == null)
                return NotFound("Không tìm thấy phòng.");

            if (phong.TrangThai != "Sẵn sàng")
            {
                TempData["ThongBao"] = "Phòng hiện không ở trạng thái sẵn sàng.";
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin khách hàng
            var khachHang = await _datPhongRepository.GetKhachHangAsync(
                maTaiKhoan.Value);

            if (khachHang == null)
            {
                TempData["ThongBao"] =
                    "Bạn chưa có hồ sơ khách hàng. Vui lòng cập nhật hồ sơ trước khi đặt phòng.";

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

            // Lấy thông tin phòng
            var phong = await _datPhongRepository.GetPhongAsync(maPhong);

            if (phong == null)
                return NotFound("Không tìm thấy phòng.");

            // Lấy thông tin khách hàng
            var khachHang = await _datPhongRepository.GetKhachHangAsync(
                maTaiKhoan.Value);

            if (khachHang == null)
            {
                TempData["ThongBao"] =
                    "Không tìm thấy hồ sơ cá nhân. Vui lòng cập nhật hồ sơ trước khi đặt phòng.";

                return RedirectToAction("Index", "Home");
            }

            if (khachHang.TrangThai != "Hoạt động")
            {
                TempData["ThongBao"] =
                    "Hồ sơ khách hàng của bạn hiện không hoạt động.";

                return RedirectToAction("Index", "Home");
            }

            yeuCauDacBiet = yeuCauDacBiet?.Trim();

            // Kiểm tra trạng thái phòng
            if (phong.TrangThai != "Sẵn sàng")
            {
                TempData["ThongBao"] =
                    "Phòng hiện không ở trạng thái sẵn sàng.";

                return RedirectToAction("Index", "Home");
            }

            DateTime ngayNhan = ngayNhanPhong.Date;
            DateTime ngayTra = ngayTraPhong.Date;

            // Kiểm tra ngày nhận phòng
            if (ngayNhan < DateTime.Today)
            {
                ModelState.AddModelError(
                    "ngayNhanPhong",
                    "Ngày nhận phòng không được nằm trong quá khứ.");
            }

            // Kiểm tra ngày trả phòng
            if (ngayTra <= ngayNhan)
            {
                ModelState.AddModelError(
                    "ngayTraPhong",
                    "Ngày trả phòng phải sau ngày nhận phòng.");
            }

            // Kiểm tra số người
            if (soNguoi < 1 || soNguoi > phong.SucChua)
            {
                ModelState.AddModelError(
                    "soNguoi",
                    "Số người phải lớn hơn 0 và không vượt sức chứa phòng.");
            }

            // Kiểm tra yêu cầu đặc biệt
            if (!string.IsNullOrWhiteSpace(yeuCauDacBiet) &&
                yeuCauDacBiet.Length > 10)
            {
                ModelState.AddModelError(
                    "yeuCauDacBiet",
                    "Yêu cầu đặc biệt không được vượt quá 10 ký tự.");
            }

            // Nếu dữ liệu không hợp lệ thì quay lại form
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

            // Kiểm tra phòng có bị trùng lịch không
            bool biTrungLich =
                await _datPhongRepository.KiemTraTrungLichAsync(
                    maPhong,
                    ngayNhan,
                    ngayTra);

            if (biTrungLich)
            {
                ModelState.AddModelError(
                    "",
                    "Phòng đã có đơn đặt trong thời gian này.");

                NapForm(
                    phong,
                    khachHang,
                    ngayNhanPhong,
                    ngayTraPhong,
                    soNguoi,
                    yeuCauDacBiet);

                return View("~/Views/DatPhong/DatPhong.cshtml");
            }

            // Tính tổng tiền dự kiến
            int soDem = (ngayTra - ngayNhan).Days;
            decimal tongTien = soDem * phong.GiaMotDem;

            // Tạo đối tượng đặt phòng
            var datPhong = new DatPhong
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaPhong = phong.MaPhong,
                NgayNhanPhong = ngayNhan,
                NgayTraPhong = ngayTra,
                SoNguoi = soNguoi,

                YeuCauDacBiet =
                    string.IsNullOrWhiteSpace(yeuCauDacBiet)
                    ? null
                    : yeuCauDacBiet,

                NgayDat = DateTime.Now,
                TongTienDuKien = tongTien,
                TrangThai = "Chờ xác nhận",
                GhiChuLeTan = ""
            };

            // Lưu đơn đặt phòng thông qua Repository
            await _datPhongRepository.AddAsync(datPhong);

            TempData["ThongBao"] =
                "Đặt phòng thành công. Vui lòng chờ xác nhận.";

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

            // Lấy đơn đặt phòng thuộc tài khoản đang đăng nhập
            var datPhong =
                await _datPhongRepository.GetDonDatPhongAsync(
                    id,
                    maTaiKhoan.Value);

            if (datPhong == null)
            {
                TempData["ThongBao"] =
                    "Không tìm thấy đơn đặt phòng thuộc tài khoản của bạn.";

                return RedirectToAction(nameof(LichSu));
            }

            // Kiểm tra điều kiện hủy
            bool dungTrangThai =
                datPhong.TrangThai == "Chờ xác nhận" ||
                datPhong.TrangThai == "Đã xác nhận";

            if (!dungTrangThai ||
                datPhong.NgayNhanPhong.Date <= DateTime.Today)
            {
                TempData["ThongBao"] =
                    "Đơn đặt phòng không đủ điều kiện hủy.";

                return RedirectToAction(nameof(LichSu));
            }

            // Cập nhật trạng thái hủy
            datPhong.TrangThai = "Đã hủy";

            await _datPhongRepository.CapNhatDatPhongAsync(datPhong);

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

            // Lấy lịch sử đặt phòng thông qua Repository
            var danhSach =
                await _datPhongRepository.GetLichSuAsync(
                    maTaiKhoan.Value);

            return View("~/Views/DatPhong/LichSu.cshtml", danhSach);
        }
    }
}
