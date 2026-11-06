
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class DatPhongRepository : IDatPhongRepository
    {
        private readonly ApplicationDbContext _context;

        public DatPhongRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Lấy khách hàng theo mã tài khoản
        public async Task<KhachHang?> GetKhachHangAsync(int maTaiKhoan)
        {
            return await _context.KhachHangs.FirstOrDefaultAsync(k => k.MaTaiKhoan == maTaiKhoan);
        }
        // Lấy thông tin phòng và loại phòng
        public async Task<Phong?> GetPhongAsync(int maPhong)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);
        }

        // Kiểm tra phòng có bị trùng lịch không
        public async Task<bool> KiemTraTrungLichAsync(
            int maPhong,
            DateTime ngayNhan,
            DateTime ngayTra)
        {
            string[] trangThaiHieuLuc =
            {
                "Chờ xác nhận",
                "Đã xác nhận",
                "Đã nhận phòng"
            };

            return await _context.DatPhongs.AnyAsync(d =>
                d.MaPhong == maPhong &&
                trangThaiHieuLuc.Contains(d.TrangThai) &&
                ngayNhan < d.NgayTraPhong &&
                ngayTra > d.NgayNhanPhong);
        }

        // Thêm đơn đặt phòng
        public async Task AddAsync(DatPhong datPhong)
        {
            await _context.DatPhongs.AddAsync(datPhong);
            await _context.SaveChangesAsync();
        }

        // Lấy đơn đặt phòng thuộc tài khoản đang đăng nhập
        public async Task<DatPhong?> GetDonDatPhongAsync(
            int id,
            int maTaiKhoan)
        {
            return await _context.DatPhongs
                .FirstOrDefaultAsync(d =>
                    d.MaDatPhong == id &&
                    _context.KhachHangs.Any(k =>
                        k.MaKhachHang == d.MaKhachHang &&
                        k.MaTaiKhoan == maTaiKhoan));
        }
        // Cập nhật đơn đặt phòng
        public async Task CapNhatDatPhongAsync(DatPhong datPhong)
        {
            _context.DatPhongs.Update(datPhong);
            await _context.SaveChangesAsync();
        }
        // Lấy lịch sử đặt phòng của tài khoản
        public async Task<List<DatPhong>> GetLichSuAsync(int maTaiKhoan)
        {
            return await _context.DatPhongs
                .Include(d => d.Phong)
                    .ThenInclude(p => p!.LoaiPhong)
                .Include(d => d.HoaDon)
                .Where(d => _context.KhachHangs.Any(k =>
                    k.MaKhachHang == d.MaKhachHang &&
                    k.MaTaiKhoan == maTaiKhoan))
                .OrderByDescending(d => d.NgayDat)
                .ToListAsync();
        }

        // Lưu thay đổi
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
