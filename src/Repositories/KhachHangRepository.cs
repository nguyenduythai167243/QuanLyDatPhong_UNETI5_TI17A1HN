
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly ApplicationDbContext _context;

        public KhachHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy khách hàng theo mã tài khoản
        public async Task<KhachHang?> GetByMaTaiKhoanAsync(int maTaiKhoan)
        {
            return await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.MaTaiKhoan == maTaiKhoan);
        }

        // Lấy khách hàng theo mã khách hàng
        public async Task<KhachHang?> GetByIdAsync(int maKhachHang)
        {
            return await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.MaKhachHang == maKhachHang);
        }

        // Lấy danh sách khách hàng
        public async Task<List<KhachHang>> GetAllAsync()
        {
            return await _context.KhachHangs
                .ToListAsync();
        }

        // Thêm khách hàng
        public async Task AddAsync(KhachHang khachHang)
        {
            await _context.KhachHangs.AddAsync(khachHang);
            await _context.SaveChangesAsync();
        }

        // Cập nhật khách hàng
        public async Task Update(KhachHang khachHang)
        {
            _context.KhachHangs.Update(khachHang);
            await _context.SaveChangesAsync();
        }

        // Xóa khách hàng theo mã
        public async Task DeleteAsync(int maKhachHang)
        {
            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.MaKhachHang == maKhachHang);

            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra khách hàng tồn tại
        public async Task<bool> ExistsAsync(int maKhachHang)
        {
            return await _context.KhachHangs
                .AnyAsync(kh => kh.MaKhachHang == maKhachHang);
        }

        // Lưu thay đổi vào Database
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
