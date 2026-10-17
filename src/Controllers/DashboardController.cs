
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var homNay = DateTime.Today;
            var dauThang = new DateTime(homNay.Year, homNay.Month, 1);
            var dauThangSau = dauThang.AddMonths(1);
            var bayNgayToi = homNay.AddDays(7);

            // 1. Tổng số loại phòng
            ViewBag.TongLoaiPhong = await _context.LoaiPhongs
                .CountAsync();

            // 2. Tổng số phòng
            ViewBag.TongSoPhong = await _context.Phongs
                .CountAsync();

            // 3. Số phòng sẵn sàng
            ViewBag.PhongSanSang = await _context.Phongs
                .CountAsync(p => p.TrangThai == "Sẵn sàng");

            // 4. Tổng số khách hàng
            ViewBag.TongKhachHang = await _context.KhachHangs
                .CountAsync();

            // 5. Tổng số lượt đặt phòng
            ViewBag.TongDatPhong = await _context.DatPhongs
                .CountAsync();

            // 6. Đặt phòng chờ xác nhận
            ViewBag.DatPhongChoXacNhan = await _context.DatPhongs
                .CountAsync(d => d.TrangThai == "Chờ xác nhận");

            // 7. Khách đang lưu trú
            ViewBag.KhachDangLuuTru = await _context.DatPhongs
                .CountAsync(d => d.TrangThai == "Đã nhận phòng");

            // 8. Đặt phòng sắp đến trong 7 ngày tới
            ViewBag.DatPhongSapDen = await _context.DatPhongs
                .CountAsync(d =>
                    d.NgayNhanPhong >= homNay &&
                    d.NgayNhanPhong < bayNgayToi &&
                    d.TrangThai == "Đã xác nhận");

            // 9. Doanh thu tháng này
            // Chỉ tính hóa đơn đã thanh toán
            ViewBag.DoanhThuThangNay =
                await _context.HoaDons
                    .Where(h =>
                        h.TrangThai == TrangThaiTT.DaThanhToan &&
                        h.NgayLap >= dauThang &&
                        h.NgayLap < dauThangSau)
                    .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0m;

            // 10. Tổng số hóa đơn
            ViewBag.TongHoaDon = await _context.HoaDons
                .CountAsync();

            // 11. Hóa đơn chưa thanh toán
            ViewBag.HoaDonChuaThanhToan = await _context.HoaDons
                .CountAsync(h => h.TrangThai == TrangThaiTT.ChuaThanhToan);

            // 12. Tổng tiền dịch vụ phát sinh đã ghi nhận
            ViewBag.TongTienDichVu = await _context.DichVuPhatSinhs
                .Where(d => d.TrangThai == TrangThaiDV.DaGhiNhan)
                .SumAsync(d => (decimal?)(d.SoLuong * d.DonGia)) ?? 0m;

            return View();
        }
    }
}
