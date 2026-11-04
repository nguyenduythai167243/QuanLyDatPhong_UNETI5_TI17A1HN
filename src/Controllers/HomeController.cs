using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (ngayTra <= ngayNhan)
            {
                ngayTra = ngayNhan.Value.AddDays(1);
            }

            ViewBag.LoaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);
            ViewBag.NgayNhan = ngayNhan.Value.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = ngayTra.Value.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soNguoi ?? 2;
            ViewBag.MaLoaiPhong = maLoaiPhong;

            // Lấy danh sách 9 phòng tiêu biểu phân bổ đủ các hạng phòng
            var phongs = await _phongRepository.GetFeaturedRoomsAsync(maLoaiPhong, soNguoi, 9);
            return View(phongs);
        }

        // ==========================================
        // 2. DANH SÁCH TẤT CẢ PHÒNG & TÌM PHÒNG TRỐNG (CÓ PHÂN TRANG)
        // ==========================================
        public async Task<IActionResult> DanhSachPhong(
            string? tuKhoa,
            int? maLoaiPhong,
            int? soNguoi,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            DateTime? ngayNhan,
            DateTime? ngayTra,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int page = 1)
        {
            const int pageSize = 6;

            // Chuẩn hóa ngày nhận và ngày trả
            var checkIn = ngayNhan ?? ngayNhanPhong ?? DateTime.Today;
            var checkOut = ngayTra ?? ngayTraPhong ?? DateTime.Today.AddDays(1);
            if (checkOut <= checkIn)
            {
                checkOut = checkIn.AddDays(1);
            }

            var soKhach = soNguoi ?? sucChuaToiThieu;
            sortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "gia_asc" : sortOrder;

            // Gọi repository lọc và phân trang chuyên dụng cho Khách hàng
            var (paginatedPhongs, phongTrongMap) = await _phongRepository.GetCustomerAvailablePhongsAsync(
                tuKhoa,
                maLoaiPhong,
                soKhach,
                giaTu,
                giaDen,
                checkIn,
                checkOut,
                sortOrder,
                page,
                pageSize);

            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);

            var vm = new PhongFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoaiPhong = maLoaiPhong,
                SucChuaToiThieu = soKhach,
                GiaTu = giaTu,
                GiaDen = giaDen,
                NgayNhanPhong = checkIn,
                NgayTraPhong = checkOut,
                SortOrder = sortOrder,
                PageIndex = page,
                PageSize = pageSize,
                DanhSachPhong = paginatedPhongs,
                LoaiPhongList = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                PhongTrongMap = phongTrongMap
            };

            // Hỗ trợ thêm cho các binding dạng ViewBag
            ViewBag.LoaiPhongs = loaiPhongs;
            ViewBag.NgayNhan = checkIn.ToString("yyyy-MM-dd");
            ViewBag.NgayTra = checkOut.ToString("yyyy-MM-dd");
            ViewBag.SoNguoi = soKhach;
            ViewBag.MaLoaiPhong = maLoaiPhong;
            ViewBag.GiaTu = giaTu;
            ViewBag.GiaDen = giaDen;
            ViewBag.TuKhoa = tuKhoa;
            ViewBag.SortOrder = sortOrder;

            return View(vm);
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
