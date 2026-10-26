
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var homNay = DateTime.Today;
            var dauThang = new DateTime(homNay.Year, homNay.Month, 1);
            var dauThangSau = dauThang.AddMonths(1);
            var bayNgayToi = homNay.AddDays(7);

            // 1. Tổng số loại phòng
            ViewBag.TongLoaiPhong = await _dashboardRepository.GetTongLoaiPhongAsync();
            // 2. Tổng số phòng
            ViewBag.TongSoPhong = await _dashboardRepository.GetTongSoPhongAsync();

            // 3. Số phòng sẵn sàng
            ViewBag.PhongSanSang = await _dashboardRepository.GetTongSoPhongSanSangAsync();

            // 4. Tổng số khách hàng
            ViewBag.TongKhachHang = await _dashboardRepository.GetTongKhachHangAsync();

            // 5. Tổng số lượt đặt phòng
            ViewBag.TongDatPhong = await _dashboardRepository.GetTongDatPhongAsync();

            // 6. Đặt phòng chờ xác nhận
            ViewBag.DatPhongChoXacNhan = await _dashboardRepository.GetDatPhongChoXacNhanAsync();

            // 7. Khách đang lưu trú
            ViewBag.KhachDangLuuTru = await _dashboardRepository.GetKhachDangLuuTruAsync();

            // 8. Đặt phòng sắp đến trong 7 ngày tới
            ViewBag.DatPhongSapDen = await _dashboardRepository.GetDatPhongSapDenTrong7NgayAsync();

            // 9. Doanh thu tháng này
            // Chỉ tính hóa đơn đã thanh toán
            ViewBag.DoanhThuThangNay =
                await _dashboardRepository.GetDoanhthuThangNayAsync();

            // 10. Tổng số hóa đơn
            ViewBag.TongHoaDon = await _dashboardRepository.GettongHoaDonAsync();

            // 11. Hóa đơn chưa thanh toán
            ViewBag.HoaDonChuaThanhToan = await _dashboardRepository.GetHoaDonChuaThanhToanAsync();

            // 12. Tổng tiền dịch vụ phát sinh đã ghi nhận
            ViewBag.TongTienDichVu = await _dashboardRepository.GetTongTienDichVuPhatSinhAsync();

            return View();
        }
    }
}
