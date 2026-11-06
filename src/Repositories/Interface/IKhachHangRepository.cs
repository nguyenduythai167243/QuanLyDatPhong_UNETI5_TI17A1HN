using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IKhachHangRepository
    {
        Task<KhachHang?> GetByMaTaiKhoanAsync(int maTaiKhoan);
        Task<KhachHang?> GetByIdAsync(int maKhachHang);
        Task<List<KhachHang>> GetAllAsync();
        Task AddAsync(KhachHang khachHang);
        Task Update(KhachHang khachHang);
        Task DeleteAsync(int maKhachHang);
        Task<bool> ExistsAsync(int maKhachHang);
        Task SaveAsync();
    }
}