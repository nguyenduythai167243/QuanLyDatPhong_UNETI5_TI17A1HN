using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IThongkeRepository
    {
        Task<int> GetTongDatPhongAsync();
        Task<int> GetTongHoaDonAsync();
        Task<int> GetHoaDonDaThanhToanAsync();
        Task<int> GetHoaDonChuaThanhToanAsync();
        Task<decimal> GetTongDoanhThuAsync();
        Task<decimal> GetDoanhThuPhongAsync();
        Task<decimal> GetDoanhThuDichVuAsync();
        Task<int> GetSoLuotDichVuAsync();
        Task<decimal> GetTongTienDichVuPhatSinhAsync();
        Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync(int nam);
    }
    public class DoanhThuThangDto
    {
        public int Thang { get; set; }
        public decimal DoanhThu { get; set; }
    }
}