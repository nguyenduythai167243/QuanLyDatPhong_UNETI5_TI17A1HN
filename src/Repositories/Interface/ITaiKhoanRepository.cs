using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    /// <summary>
    /// Interface định nghĩa các thao tác truy xuất dữ liệu cho tài khoản người dùng.
    /// </summary>
    public interface ITaiKhoanRepository
    {
        Task<TaiKhoan?> GetByTenDangNhapAsync(string tenDangNhap);
        Task<TaiKhoan?> GetByIdAsync(int maTaiKhoan);
        Task<List<TaiKhoan>> GetAllAsync();
        Task<bool> ExistsTenDangNhapAsync(string tenDangNhap);
        Task AddAsync(TaiKhoan taiKhoan);
        Task UpdateAsync(TaiKhoan taiKhoan);
        Task DeleteAsync(int maTaiKhoan);
        Task SaveAsync();
    }
}

