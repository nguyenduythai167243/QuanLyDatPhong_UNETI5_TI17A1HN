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
