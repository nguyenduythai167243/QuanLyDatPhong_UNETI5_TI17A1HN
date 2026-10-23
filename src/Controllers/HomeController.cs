using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhongRepository _phongRepository;

        public HomeController(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        // ==========================================
        // 1. TRANG CHỦ KHÁCH SẠN (PORTAL KHÁCH HÀNG)
        // ==========================================
        public async Task<IActionResult> Index(int? maLoaiPhong, DateTime? ngayNhan, DateTime? ngayTra, int? soNguoi)
        {
            ngayNhan ??= DateTime.Today;
            ngayTra ??= DateTime.Today.AddDays(1);

            ViewBag.LoaiPhongs = await _phongRepository.GetLoaiPhongsAsync();
            ViewBag.NgayNhan = ngayNhan.Value.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = ngayTra.Value.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi ?? 2;
            ViewBag.MaLoaiPhong = maLoaiPhong;

            // Lấy danh sách phòng nổi bật có thể đặt
            var phongs = await _phongRepository.GetFeaturedRoomsAsync(maLoaiPhong, soNguoi, 6);
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

            // Kiểm tra phòng bận trong khoảng thời gian đã chọn
            var checkIn = ngayNhan.Value.Date;
            var checkOut = ngayTra.Value.Date;

            var phongBanIds = await _phongRepository.GetOccupiedRoomIdsAsync(checkIn, checkOut, includeChoXacNhan: false);

            ViewBag.PhongBanIds = phongBanIds;
            ViewBag.LoaiPhongs = await _phongRepository.GetLoaiPhongsAsync();
            ViewBag.NgayNhan = checkIn.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = checkOut.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi;
            ViewBag.MaLoaiPhong = maLoaiPhong;
            ViewBag.GiaTu = giaTu;
            ViewBag.GiaDen = giaDen;

            var danhSach = await _phongRepository.GetCustomerRoomsAsync(maLoaiPhong, soNguoi, giaTu, giaDen);
            return View(danhSach);
        }

        // ==========================================
        // 3. CHI TIẾT PHÒNG
        // ==========================================
        public async Task<IActionResult> ChiTietPhong(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _phongRepository.GetByIdWithLoaiPhongAsync(id.Value);
            if (phong == null) return NotFound();

            // Lấy các phòng liên quan cùng loại phòng
            ViewBag.PhongLienQuan = await _phongRepository.GetRelatedRoomsAsync(phong.MaLoaiPhong, phong.MaPhong, 3);

            return View(phong);
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
