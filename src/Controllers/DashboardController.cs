
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

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
        var bayNgayToi = homNay.AddDays(7);

        ViewBag.TongLoaiPhong = await _context.LoaiPhong.CountAsync();

        ViewBag.TongSoPhong = await _context.Phong.CountAsync();

        ViewBag.PhongSanSang = await _context.Phong.CountAsync( p => p.TrangThai == "Sẵn sàng");

        ViewBag.TongKhachHang = await _context.KhachHang.CountAsync();

        ViewBag.TongDatPhong = await _context.DatPhong.CountAsync();

        ViewBag.DatPhongChoXacNhan = await _context.DatPhong.CountAsync(
                d => d.TrangThai == "Chờ xác nhận");

        ViewBag.KhachDangLuuTru = await _context.DatPhong.CountAsync(
                d => d.TrangThai == "Đã nhận phòng");

        ViewBag.DatPhongSapDen = await _context.DatPhong.CountAsync(d =>
                d.NgayNhanPhong.Date >= homNay &&
                d.NgayNhanPhong.Date < bayNgayToi &&
                d.TrangThai == "Đã xác nhận");

        ViewBag.DoanhThuThangNay = await _context.HoaDon
                .Where(h =>
                    h.TrangThai == TrangThaiTT.DaThanhToan &&
                    h.NgayLap.Year == homNay.Year &&
                    h.NgayLap.Month == homNay.Month)
                .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0;

        return View();
    }
}
