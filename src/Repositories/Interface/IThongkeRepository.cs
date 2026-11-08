using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IThongkeRepository
    {
        Task<int> GetTongDatPhongAsync(int nam);
        Task<int> GetTongHoaDonAsync(int nam);
        Task<int> GetHoaDonDaThanhToanAsync(int nam);
        Task<int> GetHoaDonChuaThanhToanAsync(int nam);
        Task<decimal> GetTongDoanhThuAsync(int nam);
        Task<decimal> GetDoanhThuPhongAsync(int nam);
        Task<decimal> GetDoanhThuDichVuAsync(int nam);
        Task<int> GetSoLuotDichVuAsync(int nam);
        Task<decimal> GetTongTienDichVuPhatSinhAsync(int nam);
        Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync(int nam);
    }
    public class DoanhThuThangDto
    {
        public int Thang { get; set; }
        public decimal DoanhThu { get; set; }
    }
}