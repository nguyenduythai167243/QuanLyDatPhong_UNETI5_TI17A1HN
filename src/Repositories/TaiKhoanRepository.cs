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
        /// Tìm kiếm và lọc tài khoản theo từ khóa và vai trò.
        /// </summary>
        public async Task<List<TaiKhoan>> SearchAsync(string? tuKhoa, string? vaiTro)
        {
            var query = _context.TaiKhoans.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim();
                query = query.Where(t =>
                    t.TenDangNhap.Contains(tuKhoa) ||
                    t.HoTen.Contains(tuKhoa) ||
                    t.Email.Contains(tuKhoa));
            }

            if (!string.IsNullOrWhiteSpace(vaiTro))
            {
                vaiTro = vaiTro.Trim();
                if (string.Equals(vaiTro, "LeTan", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(t => t.VaiTro == "LeTan" || t.VaiTro == "Lễ tân");
                }
                else
                {
                    query = query.Where(t => t.VaiTro == vaiTro);
                }
            }

            return await query
                .OrderBy(t => t.MaTaiKhoan)
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
        /// Kiểm tra tên đăng nhập đã tồn tại ở tài khoản khác hay chưa.
        /// </summary>
        public async Task<bool> ExistsTenDangNhapExceptAsync(string tenDangNhap, int excludeMaTaiKhoan)
        {
            return await _context.TaiKhoans
                .AnyAsync(t => t.TenDangNhap == tenDangNhap && t.MaTaiKhoan != excludeMaTaiKhoan);
        }

        /// <summary>
        /// Kiểm tra tài khoản đã liên kết với hồ sơ khách hàng hay chưa.
        /// </summary>
        public async Task<bool> HasKhachHangAsync(int maTaiKhoan)
        {
            return await _context.KhachHangs
                .AnyAsync(k => k.MaTaiKhoan == maTaiKhoan);
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

