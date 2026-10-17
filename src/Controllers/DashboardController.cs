
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
public class DashboardController : Controller
{
    private readonly QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext _context;

    public DashboardController(QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var homNay = DateTime.Today;
        var bayNgayToi = homNay.AddDays(7);

        ViewBag.TongLoaiPhong = await _context.LoaiPhongs.CountAsync();

        ViewBag.TongSoPhong = await _context.Phongs.CountAsync();

        ViewBag.PhongSanSang = await _context.Phongs.CountAsync(p => p.TrangThai == "Sẵn sàng");

        ViewBag.TongKhachHang = await _context.KhachHangs.CountAsync();

        ViewBag.TongDatPhong = await _context.DatPhongs.CountAsync();

        ViewBag.DatPhongChoXacNhan = await _context.DatPhongs.CountAsync(
                d => d.TrangThai == "Chờ xác nhận");

        ViewBag.KhachDangLuuTru = await _context.DatPhongs.CountAsync(
                d => d.TrangThai == "Đã nhận phòng");

        ViewBag.DatPhongSapDen = await _context.DatPhongs.CountAsync(d =>
                d.NgayNhanPhong.Date >= homNay &&
                d.NgayNhanPhong.Date < bayNgayToi &&
                d.TrangThai == "Đã xác nhận");

        ViewBag.DoanhThuThangNay = await _context.HoaDons
                .Where(h =>
                    h.TrangThai == TrangThaiTT.DaThanhToan &&
                    h.NgayLap.Year == homNay.Year &&
                    h.NgayLap.Month == homNay.Month)
                .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0;

        return View();
    }
}
