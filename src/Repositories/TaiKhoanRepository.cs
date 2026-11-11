using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    /// <summary>
    /// Repository xử lý truy vấn và cập nhật dữ liệu bảng TaiKhoans.
    /// </summary>
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tìm tài khoản theo tên đăng nhập (phục vụ đăng nhập).
        /// </summary>
        public async Task<TaiKhoan?> GetByTenDangNhapAsync(string tenDangNhap)
        {
            return await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.TenDangNhap == tenDangNhap);
        }

        /// <summary>
        /// Lấy thông tin tài khoản theo mã tài khoản.
        /// </summary>
        public async Task<TaiKhoan?> GetByIdAsync(int maTaiKhoan)
        {
            return await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == maTaiKhoan);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách tài khoản.
        /// </summary>
        public async Task<List<TaiKhoan>> GetAllAsync()
        {
            return await _context.TaiKhoans
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại hay chưa.
        /// </summary>
        public async Task<bool> ExistsTenDangNhapAsync(string tenDangNhap)
        {
            return await _context.TaiKhoans
                .AnyAsync(t => t.TenDangNhap == tenDangNhap);
        }

        /// <summary>
        /// Thêm tài khoản mới vào cơ sở dữ liệu.
        /// </summary>
        public async Task AddAsync(TaiKhoan taiKhoan)
        {
            await _context.TaiKhoans.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản.
        /// </summary>
        public async Task UpdateAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Update(taiKhoan);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Xóa tài khoản theo mã tài khoản nếu tồn tại.
        /// </summary>
        public async Task DeleteAsync(int maTaiKhoan)
        {
            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == maTaiKhoan);

            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Lưu các thay đổi đang chờ vào cơ sở dữ liệu.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

