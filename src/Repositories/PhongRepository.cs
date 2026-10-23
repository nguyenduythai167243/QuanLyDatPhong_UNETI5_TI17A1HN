using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class PhongRepository : IPhongRepository
    {
        private readonly ApplicationDbContext _context;

        public PhongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Phong>> GetAllAsync()
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Phong?> GetByIdAsync(int id)
        {
            return await _context.Phongs.FindAsync(id);
        }

        public async Task<Phong?> GetByIdWithLoaiPhongAsync(int id)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefaultAsync(p => p.MaPhong == id);
        }

        public async Task<Phong?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .Include(p => p.DatPhongs!.Where(dp => dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng"))
                .FirstOrDefaultAsync(m => m.MaPhong == id);
        }

        public async Task<Phong?> GetByIdWithBookingsAsync(int id)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .Include(p => p.DatPhongs)
                .FirstOrDefaultAsync(m => m.MaPhong == id);
        }

        public async Task AddAsync(Phong phong)
        {
            await _context.Phongs.AddAsync(phong);
        }

        public Task UpdateAsync(Phong phong)
        {
            _context.Phongs.Update(phong);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Phong phong)
        {
            _context.Phongs.Remove(phong);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Phongs.AnyAsync(p => p.MaPhong == id);
        }

        public async Task<bool> IsSoPhongExistsAsync(string soPhong, int? excludePhongId = null)
        {
            var normalized = soPhong.Trim().ToLower();
            if (excludePhongId.HasValue)
            {
                return await _context.Phongs.AnyAsync(p => p.MaPhong != excludePhongId.Value && p.SoPhong.ToLower() == normalized);
            }
            return await _context.Phongs.AnyAsync(p => p.SoPhong.ToLower() == normalized);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasActiveBookingsAsync(int phongId)
        {
            return await _context.DatPhongs.AnyAsync(dp =>
                dp.MaPhong == phongId &&
                (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                dp.NgayTraPhong >= DateTime.Today);
        }

        public async Task<bool> HasAnyBookingsAsync(int phongId)
        {
            return await _context.DatPhongs.AnyAsync(dp => dp.MaPhong == phongId);
        }

        public async Task<List<int>> GetDistinctFloorsAsync()
        {
            return await _context.Phongs
                .Select(p => p.Tang)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();
        }

        public async Task<List<int>> GetOccupiedRoomIdsAsync(DateTime checkIn, DateTime checkOut, bool includeChoXacNhan = true)
        {
            var query = _context.DatPhongs.AsQueryable();

            if (includeChoXacNhan)
            {
                query = query.Where(dp => dp.TrangThai == "Chờ xác nhận" || dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng");
            }
            else
            {
                query = query.Where(dp => dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng");
            }

            var cIn = checkIn.Date;
            var cOut = checkOut.Date;

            return await query
                .Where(dp => dp.NgayNhanPhong.Date < cOut && dp.NgayTraPhong.Date > cIn)
                .Select(dp => dp.MaPhong)
                .Distinct()
                .ToListAsync();
        }

        public async Task<PaginatedList<Phong>> GetFilteredPhongsAsync(
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
            int pageSize)
        {
            var query = _context.Phongs.Include(p => p.LoaiPhong).AsQueryable();

            // 1. Tìm kiếm
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(p => p.SoPhong.ToLower().Contains(kw) || p.LoaiPhong!.TenLoaiPhong.ToLower().Contains(kw));
            }

            // 2. Bộ lọc
            if (maLoaiPhong.HasValue && maLoaiPhong > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (tang.HasValue && tang > 0)
            {
                query = query.Where(p => p.Tang == tang.Value);
            }

            if (sucChuaToiThieu.HasValue && sucChuaToiThieu > 0)
            {
                query = query.Where(p => p.SucChua >= sucChuaToiThieu.Value);
            }

            if (giaTu.HasValue)
            {
                query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            }

            if (giaDen.HasValue)
            {
                query = query.Where(p => p.GiaMotDem <= giaDen.Value);
            }

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            if (ngayNhanPhong.HasValue && ngayTraPhong.HasValue && ngayTraPhong > ngayNhanPhong)
            {
                var phongDatTrungIds = await GetOccupiedRoomIdsAsync(ngayNhanPhong.Value, ngayTraPhong.Value, includeChoXacNhan: true);
                query = query.Where(p => !phongDatTrungIds.Contains(p.MaPhong));
            }

            // 3. Sắp xếp
            query = sortOrder switch
            {
                "so_phong_desc" => query.OrderByDescending(p => p.SoPhong),
                "gia_asc" => query.OrderBy(p => p.GiaMotDem),
                "gia_desc" => query.OrderByDescending(p => p.GiaMotDem),
                "suc_chua_asc" => query.OrderBy(p => p.SucChua),
                "suc_chua_desc" => query.OrderByDescending(p => p.SucChua),
                _ => query.OrderBy(p => p.SoPhong)
            };

            // 4. Phân trang
            return await PaginatedList<Phong>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<(PaginatedList<Phong> Phongs, Dictionary<int, bool> PhongTrongMap)> GetCustomerAvailablePhongsAsync(
            string? tuKhoa,
            int? maLoaiPhong,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int pageIndex,
            int pageSize)
        {
            var query = _context.Phongs.Include(p => p.LoaiPhong).AsQueryable();

            // Khách hàng chỉ xem phòng đang hoạt động (Sẵn sàng) và loại phòng còn hoạt động
            query = query.Where(p => p.TrangThai == "Sẵn sàng" && p.LoaiPhong!.TrangThai);

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(p => p.SoPhong.ToLower().Contains(kw) ||
                                         p.LoaiPhong!.TenLoaiPhong.ToLower().Contains(kw) ||
                                         (p.TienNghi != null && p.TienNghi.ToLower().Contains(kw)));
            }

            // Lọc theo loại phòng, sức chứa, giá
            if (maLoaiPhong.HasValue && maLoaiPhong > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (sucChuaToiThieu.HasValue && sucChuaToiThieu > 0)
            {
                query = query.Where(p => p.SucChua >= sucChuaToiThieu.Value);
            }

            if (giaTu.HasValue) query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            if (giaDen.HasValue) query = query.Where(p => p.GiaMotDem <= giaDen.Value);

            // Kiểm tra trạng thái Còn trống/Đã có người đặt theo khoảng ngày
            var phongTrongMap = new Dictionary<int, bool>();
            if (ngayNhanPhong.HasValue && ngayTraPhong.HasValue && ngayTraPhong > ngayNhanPhong)
            {
                var phongDaDatIds = await GetOccupiedRoomIdsAsync(ngayNhanPhong.Value, ngayTraPhong.Value, includeChoXacNhan: true);
                var tatCaPhongIds = await query.Select(p => p.MaPhong).ToListAsync();
                foreach (var id in tatCaPhongIds)
                {
                    phongTrongMap[id] = !phongDaDatIds.Contains(id);
                }
            }

            // Sắp xếp
            query = sortOrder switch
            {
                "so_phong_desc" => query.OrderByDescending(p => p.SoPhong),
                "gia_asc" => query.OrderBy(p => p.GiaMotDem),
                "gia_desc" => query.OrderByDescending(p => p.GiaMotDem),
                "suc_chua_asc" => query.OrderBy(p => p.SucChua),
                "suc_chua_desc" => query.OrderByDescending(p => p.SucChua),
                _ => query.OrderBy(p => p.GiaMotDem)
            };

            var paginatedPhongs = await PaginatedList<Phong>.CreateAsync(query, pageIndex, pageSize);
            return (paginatedPhongs, phongTrongMap);
        }

        public async Task<List<Phong>> GetFeaturedRoomsAsync(int? maLoaiPhong, int? soNguoi, int count = 6)
        {
            var query = _context.Phongs
                .Include(p => p.LoaiPhong)
                .AsQueryable();

            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (soNguoi.HasValue && soNguoi.Value > 0)
            {
                query = query.Where(p => p.SucChua >= soNguoi.Value);
            }

            return await query.Take(count).ToListAsync();
        }

        public async Task<List<Phong>> GetCustomerRoomsAsync(int? maLoaiPhong, int? soNguoi, decimal? giaTu, decimal? giaDen)
        {
            var query = _context.Phongs
                .Include(p => p.LoaiPhong)
                .AsQueryable();

            if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            if (soNguoi.HasValue && soNguoi.Value > 0)
            {
                query = query.Where(p => p.SucChua >= soNguoi.Value);
            }

            if (giaTu.HasValue)
            {
                query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            }

            if (giaDen.HasValue)
            {
                query = query.Where(p => p.GiaMotDem <= giaDen.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Phong>> GetRelatedRoomsAsync(int maLoaiPhong, int currentPhongId, int count = 3)
        {
            return await _context.Phongs
                .Include(p => p.LoaiPhong)
                .Where(p => p.MaLoaiPhong == maLoaiPhong && p.MaPhong != currentPhongId)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<LoaiPhong>> GetLoaiPhongsAsync(bool onlyActive = false)
        {
            var query = _context.LoaiPhongs.AsQueryable();
            if (onlyActive)
            {
                query = query.Where(lp => lp.TrangThai);
            }
            return await query.ToListAsync();
        }

        public async Task<LoaiPhong?> GetLoaiPhongByIdAsync(int id)
        {
            return await _context.LoaiPhongs.FindAsync(id);
        }
    }
}
