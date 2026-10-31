using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IDichVuPhatSinhRespository
    {
        Task<List<DichVuPhatSinh>> GetAllAsync();
        Task<DichVuPhatSinh?> GetDetailsAsync(int id);
        Task<DichVuPhatSinh?> GetByIdAsync(int id);
        Task<DatPhong?> GetDatPhongAsync(int? MaDatPhong);
        Task AddAsync(DichVuPhatSinh dichVu);
        Task SaveChangesAsync();
    }
}