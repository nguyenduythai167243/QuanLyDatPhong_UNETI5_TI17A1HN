
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? nam)
        {
            var homNay = DateTime.Today;
            var namThongKe = nam ?? homNay.Year;

            var dauNam = new DateTime(namThongKe, 1, 1);
            var dauNamSau = dauNam.AddYears(1);

            // 1. Tổng số lượt đặt phòng
            ViewBag.TongDatPhong =
                await _context.DatPhongs.CountAsync();

            // 2. Tổng số hóa đơn
            ViewBag.TongHoaDon =
                await _context.HoaDons.CountAsync();

            // 3. Số hóa đơn đã thanh toán
            ViewBag.HoaDonDaThanhToan =
                await _context.HoaDons.CountAsync(
                    h => h.TrangThai == TrangThaiTT.DaThanhToan);

            // 4. Số hóa đơn chưa thanh toán
            ViewBag.HoaDonChuaThanhToan =
                await _context.HoaDons.CountAsync(
                    h => h.TrangThai == TrangThaiTT.ChuaThanhToan);

            // 5. Tổng doanh thu thực thu
            var hoaDonDaThanhToan = _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan);

            ViewBag.TongDoanhThu =
                await hoaDonDaThanhToan
                    .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0m;

            // 6. Tổng tiền phòng đã thu
            ViewBag.DoanhThuPhong =
                await hoaDonDaThanhToan
                    .SumAsync(h => (decimal?)h.TienPhong) ?? 0m;

            // 7. Tổng tiền dịch vụ trên hóa đơn đã thanh toán
            ViewBag.DoanhThuDichVu =
                await hoaDonDaThanhToan
                    .SumAsync(h => (decimal?)h.TienDichVu) ?? 0m;

            // 8. Tổng tiền dịch vụ phát sinh đã ghi nhận,
            // không tính các dịch vụ đã hủy
            var dichVuDaGhiNhan = _context.DichVuPhatSinhs
                .Where(d => d.TrangThai == TrangThaiDV.DaGhiNhan);

            ViewBag.SoLuotDichVu =
                await dichVuDaGhiNhan.CountAsync();

            ViewBag.TongTienDichVuPhatSinh =
                await dichVuDaGhiNhan
                    .SumAsync(d => (decimal?)(d.SoLuong * d.DonGia))
                    ?? 0m;

            // 9. Doanh thu theo tháng trong năm được chọn
            var doanhThuTheoThang = await _context.HoaDons
                .Where(h =>
                    h.TrangThai == TrangThaiTT.DaThanhToan &&
                    h.NgayLap >= dauNam &&
                    h.NgayLap < dauNamSau)
                .GroupBy(h => h.NgayLap.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(h => h.TongTienThanhToan)
                })
                .ToListAsync();

            // Hiển thị đủ 12 tháng, kể cả tháng chưa có doanh thu
            ViewBag.DoanhThuTheoThang = Enumerable.Range(1, 12)
                .Select(thang => new
                {
                    Thang = thang,
                    DoanhThu = doanhThuTheoThang
                        .Where(x => x.Thang == thang)
                        .Select(x => x.DoanhThu)
                        .FirstOrDefault()
                })
                .ToList();

            // 10. Tổng doanh thu trong năm được chọn
            ViewBag.DoanhThuNam =
                doanhThuTheoThang.Sum(x => x.DoanhThu);

            ViewBag.Nam = namThongKe;
            ViewBag.DanhSachNam = Enumerable
                .Range(homNay.Year - 4, 5)
                .Reverse()
                .ToList();

            return View();
        }
    }
}
