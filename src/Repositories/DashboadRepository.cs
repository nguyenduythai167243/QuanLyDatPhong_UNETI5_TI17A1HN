using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDatPhong_UNeTI5_TI17A1HN.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTongLoaiPhongAsync()
        {
            return await _context.LoaiPhongs.CountAsync();
        }
        public async Task<int> GetTongSoPhongAsync()
        {
            return await _context.Phongs.CountAsync();
        }
        public async Task<int> GetTongSoPhongSanSangAsync()
        {
            return await _context.Phongs.CountAsync(p => p.TrangThai == "Sẵn sàng");
        }
        // Tong so khach hang
        public async Task<int> GetTongKhachHangAsync()
        {
            return await _context.KhachHangs.CountAsync();
        }
        // Tong so luot dat phong
        public async Task<int> GetTongDatPhongAsync()
        {
            return await _context.DatPhongs.CountAsync();
        }
        // Dat phong cho xac nhan
        public async Task<int> GetDatPhongChoXacNhanAsync()
        {
            return await _context.DatPhongs.CountAsync(d => d.TrangThai == "Chờ xác nhận");
        }
        // Khach hang dang luu tru
        public async Task<int> GetKhachDangLuuTruAsync()
        {
            return await _context.DatPhongs.CountAsync(d => d.TrangThai == "Đã nhận phòng");
        }
        // Dat phong sap den trong 7 ngay toi
        public async Task<int> GetDatPhongSapDenTrong7NgayAsync()
        {
            
            var homNay = DateTime.Today;
            var dauThang = new DateTime(homNay.Year, homNay.Month, 1);
            var dauThangSau = dauThang.AddMonths(1);
            var bayNgayToi = homNay.AddDays(7);
            return await _context.DatPhongs
                .CountAsync(d =>
                    d.NgayNhanPhong >= homNay &&
                    d.NgayNhanPhong < bayNgayToi &&
                    d.TrangThai == "Đã xác nhận");
        }
        // Doanh thu thang nay
        public async Task<decimal> GetDoanhthuThangNayAsync()
        {
            var homNay = DateTime.Today;
            var dauThang = new DateTime(homNay.Year, homNay.Month, 1);
            var dauThangSau = dauThang.AddMonths(1);
            var bayNgayToi = homNay.AddDays(7);
            return await _context.HoaDons
                    .Where(h =>
                        h.TrangThai == TrangThaiTT.DaThanhToan &&
                        h.NgayLap >= dauThang &&
                        h.NgayLap < dauThangSau)
                    .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0m;
        }
        // Tong so hoa don
        public async Task<int> GettongHoaDonAsync()
        {
            return await _context.HoaDons.CountAsync();
        }
        // Hoa don chua thanh toan
        public async Task<int> GetHoaDonChuaThanhToanAsync()
        {
            return await _context.HoaDons.CountAsync(h => h.TrangThai == TrangThaiTT.ChuaThanhToan);
        }
        // Tong Tien dich vu phat sinh
        public async Task<decimal> GetTongTienDichVuPhatSinhAsync()
        {
            return await _context.DichVuPhatSinhs
                .Where(d => d.TrangThai == TrangThaiDV.DaGhiNhan)
                .SumAsync(d => (decimal?)(d.SoLuong * d.DonGia)) ?? 0m;
        }
    }
}