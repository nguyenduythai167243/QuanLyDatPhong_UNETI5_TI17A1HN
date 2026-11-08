
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly IThongkeRepository _thongKeRepository;

        public ThongKeController(IThongkeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }
    

        public async Task<IActionResult> Index(int? nam)
        {
            var homNay = DateTime.Today;
            var namThongKe = nam ?? homNay.Year;

            // 1. Tổng số lượt đặt phòng
            ViewBag.TongDatPhong =
                await _thongKeRepository.GetTongDatPhongAsync(namThongKe);

            // 2. Tổng số hóa đơn
            ViewBag.TongHoaDon =
                await _thongKeRepository.GetTongHoaDonAsync(namThongKe);

            // 3. Số hóa đơn đã thanh toán
            ViewBag.HoaDonDaThanhToan =
                await _thongKeRepository.GetHoaDonDaThanhToanAsync(namThongKe);

            // 4. Số hóa đơn chưa thanh toán
            ViewBag.HoaDonChuaThanhToan =
                await _thongKeRepository.GetHoaDonChuaThanhToanAsync(namThongKe);

            // 5. Tổng doanh thu thực thu
            ViewBag.TongDoanhThu =
                await _thongKeRepository.GetTongDoanhThuAsync(namThongKe);

            // 6. Tổng tiền phòng đã thu
            ViewBag.DoanhThuPhong =
                await _thongKeRepository.GetDoanhThuPhongAsync(namThongKe);

            // 7. Tổng tiền dịch vụ trên hóa đơn đã thanh toán
            ViewBag.DoanhThuDichVu =
                await _thongKeRepository.GetDoanhThuDichVuAsync(namThongKe);

            // 8. Số lượt dịch vụ đã ghi nhận
            ViewBag.SoLuotDichVu =
                await _thongKeRepository.GetSoLuotDichVuAsync(namThongKe);

            // 9. Tổng tiền dịch vụ phát sinh đã ghi nhận
            ViewBag.TongTienDichVuPhatSinh =
                await _thongKeRepository.GetTongTienDichVuPhatSinhAsync(namThongKe);

            // 10. Doanh thu theo 12 tháng của năm được chọn
            var doanhThuTheoThang =
                await _thongKeRepository.GetDoanhThuTheoThangAsync(namThongKe);

            ViewBag.DoanhThuTheoThang = doanhThuTheoThang;

            // Tổng doanh thu trong năm được chọn
            ViewBag.DoanhThuNam =
                doanhThuTheoThang.Sum(x => x.DoanhThu);

            ViewBag.Nam = namThongKe;

            // Danh sách 5 năm để chọn thống kê
            ViewBag.DanhSachNam = Enumerable
                .Range(homNay.Year - 4, 5)
                .Reverse()
                .ToList();

            return View();
        }
    }
}
