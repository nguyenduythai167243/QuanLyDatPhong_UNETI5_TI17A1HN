using System;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class HoaDonRespository : IHoaDonRespository
    {
    private readonly QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext _context;

    public HoaDonRespository(QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<HoaDon>> GetAllAsync()
        {
           return await _context.HoaDons
            .Include(h => h.DatPhong)
            .OrderByDescending(h => h.NgayLap)
            .ToListAsync();
        }
    public async Task<HoaDon?> GetDetailsAsync(int? id)
        {
           return await _context.HoaDons
            .Include(h => h.DatPhong)
                .ThenInclude(d => d!.Phong)
            .Include(h => h.DatPhong)
                .ThenInclude(d => d!.KhachHang)
            .FirstOrDefaultAsync(h => h.MaHoaDon == id);
        }
         public async Task<DatPhong?> GetDatPhongLapHoaDonAsync(int maDatPhong)
        {
           return await _context.DatPhongs
            .Include(d => d.Phong)
            .Include(d => d.DichVuPhatSinhs)
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatPhong == maDatPhong);
        }
        public async Task<bool> ExistsByDatPhongAsync(int maDatPhong)
        {
            return await _context.HoaDons.AnyAsync(
                    h => h.MaDatPhong == maDatPhong);
        }
         public async Task<List<int>> GetDanhSachDatPhongDuDieuKienAsync()
        {
            return await _context.DatPhongs
                .Where(d =>
                    d.TrangThai == "Đã trả phòng" &&
                    d.NgayNhanThucTe != null &&
                    d.NgayTraThucTe != null &&
                    d.HoaDon == null &&
                    !_context.HoaDons.Any(h => h.MaDatPhong == d.MaDatPhong))
                .OrderByDescending(d => d.MaDatPhong)
                .Select(d => d.MaDatPhong)
                .ToListAsync();
        }
        // 6. Thêm hóa đơn
        public async Task AddAsync(HoaDon hoaDon)
        {
            await _context.HoaDons.AddAsync(hoaDon);
            await _context.SaveChangesAsync();
        }

        // 7. Cập nhật hóa đơn
        public async Task UpdateAsync(HoaDon hoaDon)
        {
            _context.HoaDons.Update(hoaDon);
            await _context.SaveChangesAsync();
        }
    }
}