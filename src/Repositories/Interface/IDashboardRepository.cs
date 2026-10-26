using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IDashboardRepository
    {
        Task<int> GetTongLoaiPhongAsync();
        Task<int> GetTongSoPhongAsync();
        Task<int> GetTongSoPhongSanSangAsync();
        Task<int> GetTongKhachHangAsync();
        Task<int> GetTongDatPhongAsync();
        Task<int> GetDatPhongChoXacNhanAsync();
        Task<int> GetKhachDangLuuTruAsync();
        Task<int> GetDatPhongSapDenTrong7NgayAsync();
        Task<decimal> GetDoanhthuThangNayAsync();
        Task<int> GettongHoaDonAsync();
        Task<int> GetHoaDonChuaThanhToanAsync();
        Task<decimal> GetTongTienDichVuPhatSinhAsync();
    }
}