using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class ThongKeRepository : IThongkeRepository
    {
        private readonly ApplicationDbContext _context;

        public ThongKeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // 1. Tổng số lượt đặt phòng
        public async Task<int> GetTongDatPhongAsync()
        {
            return await _context.DatPhongs.CountAsync();
        }

        // 2. Tổng số hóa đơn
        public async Task<int> GetTongHoaDonAsync()
        {
            return await _context.HoaDons.CountAsync();
        }

        // 3. Số hóa đơn đã thanh toán
        public async Task<int> GetHoaDonDaThanhToanAsync()
        {
            return await _context.HoaDons.CountAsync(
                h => h.TrangThai == TrangThaiTT.DaThanhToan);
        }

        // 4. Số hóa đơn chưa thanh toán
        public async Task<int> GetHoaDonChuaThanhToanAsync()
        {
            return await _context.HoaDons.CountAsync(
                h => h.TrangThai == TrangThaiTT.ChuaThanhToan);
        }

        // 5. Tổng doanh thu thực thu từ hóa đơn đã thanh toán
        public async Task<decimal> GetTongDoanhThuAsync()
        {
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan)
                .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0m;
        }

        // 6. Tổng tiền phòng đã thu
        public async Task<decimal> GetDoanhThuPhongAsync()
        {
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan)
                .SumAsync(h => (decimal?)h.TienPhong) ?? 0m;
        }

        // 7. Tổng tiền dịch vụ trên hóa đơn đã thanh toán
        public async Task<decimal> GetDoanhThuDichVuAsync()
        {
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan)
                .SumAsync(h => (decimal?)h.TienDichVu) ?? 0m;
        }

        // 8. Số lượt dịch vụ đã ghi nhận, không tính dịch vụ đã hủy
        public async Task<int> GetSoLuotDichVuAsync()
        {
            return await _context.DichVuPhatSinhs
                .CountAsync(d => d.TrangThai == TrangThaiDV.DaGhiNhan);
        }

        // 9. Tổng tiền dịch vụ phát sinh đã ghi nhận
        public async Task<decimal> GetTongTienDichVuPhatSinhAsync()
        {
            return await _context.DichVuPhatSinhs
                .Where(d => d.TrangThai == TrangThaiDV.DaGhiNhan)
                .SumAsync(d => (decimal?)(d.SoLuong * d.DonGia)) ?? 0m;
        }

        // 10. Doanh thu theo tháng của năm được chọn
        public async Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync(int nam)
        {
            var dauNam = new DateTime(nam, 1, 1);
            var dauNamSau = dauNam.AddYears(1);

            var doanhThuTrongNam = await _context.HoaDons
                .Where(h =>
                    h.TrangThai == TrangThaiTT.DaThanhToan &&
                    h.NgayLap >= dauNam &&
                    h.NgayLap < dauNamSau)
                .GroupBy(h => h.NgayLap.Month)
                .Select(g => new DoanhThuThangDto
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(h => h.TongTienThanhToan)
                })
                .ToListAsync();

            // Đảm bảo trả đủ 12 tháng, kể cả tháng chưa có doanh thu
            return Enumerable.Range(1, 12)
                .Select(thang => new DoanhThuThangDto
                {
                    Thang = thang,
                    DoanhThu = doanhThuTrongNam
                        .Where(x => x.Thang == thang)
                        .Select(x => x.DoanhThu)
                        .FirstOrDefault()
                })
                .ToList();
        }
    }
 }