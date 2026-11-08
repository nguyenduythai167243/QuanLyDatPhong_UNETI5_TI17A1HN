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
        public async Task<int> GetTongDatPhongAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.DatPhongs.CountAsync(d =>
                d.NgayDat >= dauNam && d.NgayDat < dauNamSau);
        }

        // 2. Tổng số hóa đơn
        public async Task<int> GetTongHoaDonAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.HoaDons.CountAsync(h =>
                h.NgayLap >= dauNam && h.NgayLap < dauNamSau);
        }

        // 3. Số hóa đơn đã thanh toán
        public async Task<int> GetHoaDonDaThanhToanAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.HoaDons.CountAsync(
                h => h.TrangThai == TrangThaiTT.DaThanhToan &&
                     h.NgayLap >= dauNam && h.NgayLap < dauNamSau);
        }

        // 4. Số hóa đơn chưa thanh toán
        public async Task<int> GetHoaDonChuaThanhToanAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.HoaDons.CountAsync(
                h => h.TrangThai == TrangThaiTT.ChuaThanhToan &&
                     h.NgayLap >= dauNam && h.NgayLap < dauNamSau);
        }

        // 5. Tổng doanh thu thực thu lũy kế đến hết năm được chọn
        public async Task<decimal> GetTongDoanhThuAsync(int nam)
        {
            var mocKetThuc = new DateTime(nam + 1, 1, 1);
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan &&
                            h.NgayLap < mocKetThuc)
                .SumAsync(h => (decimal?)h.TongTienThanhToan) ?? 0m;
        }

        // 6. Tổng tiền phòng đã thu
        public async Task<decimal> GetDoanhThuPhongAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan &&
                            h.NgayLap >= dauNam && h.NgayLap < dauNamSau)
                .SumAsync(h => (decimal?)h.TienPhong) ?? 0m;
        }

        // 7. Tổng tiền dịch vụ trên hóa đơn đã thanh toán
        public async Task<decimal> GetDoanhThuDichVuAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.HoaDons
                .Where(h => h.TrangThai == TrangThaiTT.DaThanhToan &&
                            h.NgayLap >= dauNam && h.NgayLap < dauNamSau)
                .SumAsync(h => (decimal?)h.TienDichVu) ?? 0m;
        }

        // 8. Số lượt dịch vụ đã ghi nhận, không tính dịch vụ đã hủy
        public async Task<int> GetSoLuotDichVuAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.DichVuPhatSinhs
                .CountAsync(d => d.TrangThai == TrangThaiDV.DaGhiNhan &&
                                 d.NgaySuDung >= dauNam && d.NgaySuDung < dauNamSau);
        }

        // 9. Tổng tiền dịch vụ phát sinh đã ghi nhận
        public async Task<decimal> GetTongTienDichVuPhatSinhAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);
            return await _context.DichVuPhatSinhs
                .Where(d => d.TrangThai == TrangThaiDV.DaGhiNhan &&
                            d.NgaySuDung >= dauNam && d.NgaySuDung < dauNamSau)
                .SumAsync(d => (decimal?)(d.SoLuong * d.DonGia)) ?? 0m;
        }

        // 10. Doanh thu theo tháng của năm được chọn
        public async Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync(int nam)
        {
            var (dauNam, dauNamSau) = GetKhoangThoiGianNam(nam);

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

        private static (DateTime DauNam, DateTime DauNamSau) GetKhoangThoiGianNam(int nam)
        {
            var dauNam = new DateTime(nam, 1, 1);
            return (dauNam, dauNam.AddYears(1));
        }
    }
 }