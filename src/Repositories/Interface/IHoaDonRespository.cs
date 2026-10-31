using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IHoaDonRespository
    {
        Task<List<HoaDon>> GetAllAsync();
        Task<HoaDon?> GetDetailsAsync(int? id);
        Task<DatPhong?> GetDatPhongLapHoaDonAsync(int maDatPhong);
        Task<bool> ExistsByDatPhongAsync(int maDatPhong);
        Task<List<int>> GetDanhSachDatPhongDuDieuKienAsync();
        Task AddAsync(HoaDon hoaDon);
        Task UpdateAsync(HoaDon hoaDon);
    }
}