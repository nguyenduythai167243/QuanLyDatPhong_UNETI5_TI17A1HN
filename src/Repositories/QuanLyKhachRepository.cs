using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class QuanLyKhachRepository : IQuanLyKhachRepository
    {
       private readonly ApplicationDbContext _context;

        public QuanLyKhachRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách khách hàng
        public async Task<List<KhachHang>> GetAllAsync()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        // Tìm kiếm khách hàng theo số giấy tờ tùy thân
        public async Task<List<KhachHang>> SearchAsync(string? searchString)
        {
            var query = _context.KhachHangs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();

                query = query.Where(kh =>
                    kh.SoGiayToTuyThan.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        // Lấy khách hàng theo mã
        public async Task<KhachHang?> GetByIdAsync(int maKhachHang)
        {
            return await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.MaKhachHang == maKhachHang);
        }

        // Thêm khách hàng
        public async Task AddAsync(KhachHang khachHang)
        {
            await _context.KhachHangs.AddAsync(khachHang);
        }

        // Cập nhật khách hàng
        public void Update(KhachHang khachHang)
        {
            _context.KhachHangs.Update(khachHang);
        }

        // Xóa khách hàng
        public void Delete(KhachHang khachHang)
        {
            _context.KhachHangs.Remove(khachHang);
        }

        // Kiểm tra khách hàng tồn tại
        public async Task<bool> ExistsAsync(int maKhachHang)
        {
            return await _context.KhachHangs
                .AnyAsync(kh => kh.MaKhachHang == maKhachHang);
        }

        // Lưu thay đổi
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        } 
    }
}