using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IQuanLyKhachRepository
    {
        Task<List<KhachHang>> GetAllAsync();
        Task<List<KhachHang>> SearchAsync(string? searchString);
        Task<KhachHang?> GetByIdAsync(int maKhachHang);
        Task AddAsync(KhachHang khachHang);
        void Update(KhachHang khachHang);
        void Delete(KhachHang khachHang);
        Task<bool> ExistsAsync(int maKhachHang);
        Task SaveAsync();
    }
}