using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public interface IPhongRepository
    {
        // CRUD cơ bản cho Phong
        Task<IEnumerable<Phong>> GetAllAsync();
        Task<Phong?> GetByIdAsync(int id);
        Task<Phong?> GetByIdWithLoaiPhongAsync(int id);
        Task<Phong?> GetByIdWithDetailsAsync(int id);
        Task<Phong?> GetByIdWithBookingsAsync(int id);
        Task AddAsync(Phong phong);
        Task UpdateAsync(Phong phong);
        Task DeleteAsync(Phong phong);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsSoPhongExistsAsync(string soPhong, int? excludePhongId = null);
        Task<bool> SaveChangesAsync();

        // Kiểm tra nghiệp vụ phòng & ràng buộc đặt phòng
        Task<bool> HasActiveBookingsAsync(int phongId);
        Task<bool> HasAnyBookingsAsync(int phongId);
        Task<List<int>> GetDistinctFloorsAsync();
        Task<List<int>> GetOccupiedRoomIdsAsync(DateTime checkIn, DateTime checkOut, bool includeChoXacNhan = true);

        // Lọc và phân trang cho Quản trị viên / Lễ tân (PhongController - Index)
        Task<PaginatedList<Phong>> GetFilteredPhongsAsync(
            string? tuKhoa,
            int? maLoaiPhong,
            int? tang,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            string? trangThai,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int pageIndex,
            int pageSize);

        // Lọc và phân trang cho Khách hàng (PhongController - DanhSachPhong)
        Task<(PaginatedList<Phong> Phongs, Dictionary<int, bool> PhongTrongMap)> GetCustomerAvailablePhongsAsync(
            string? tuKhoa,
            int? maLoaiPhong,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int pageIndex,
            int pageSize);

        // Trang chủ & Cổng thông tin khách sạn (HomeController)
        Task<List<Phong>> GetFeaturedRoomsAsync(int? maLoaiPhong, int? soNguoi, int count = 6);
        Task<List<Phong>> GetCustomerRoomsAsync(int? maLoaiPhong, int? soNguoi, decimal? giaTu, decimal? giaDen);
        Task<List<Phong>> GetRelatedRoomsAsync(int maLoaiPhong, int currentPhongId, int count = 3);

        // Hỗ trợ truy vấn Loại phòng liên quan tới Phòng
        Task<List<LoaiPhong>> GetLoaiPhongsAsync(bool onlyActive = false);
        Task<LoaiPhong?> GetLoaiPhongByIdAsync(int id);
    }
}
