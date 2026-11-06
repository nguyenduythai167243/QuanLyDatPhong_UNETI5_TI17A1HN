using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IDatPhongRepository
    {
        Task<KhachHang?> GetKhachHangAsync(int maTaiKhoan);
        Task<Phong?> GetPhongAsync(int maPhong);
        Task<bool> KiemTraTrungLichAsync(int maPhong, DateTime ngayNhan, DateTime ngayTra);
        Task AddAsync(DatPhong datPhong);
        Task<DatPhong?> GetDonDatPhongAsync(int id, int maTaiKhoan);
        Task CapNhatDatPhongAsync(DatPhong datPhong);
        Task<List<DatPhong>> GetLichSuAsync(int maTaiKhoan);
        Task SaveAsync();
    }
}