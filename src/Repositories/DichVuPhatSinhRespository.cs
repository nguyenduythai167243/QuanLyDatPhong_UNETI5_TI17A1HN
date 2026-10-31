using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    public class DichVuPhatSinhRespository : IDichVuPhatSinhRespository
    {
    private readonly QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext _context;

    public DichVuPhatSinhRespository(QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<DichVuPhatSinh>> GetAllAsync()
        {
           return await _context.DichVuPhatSinhs
            .Include(d => d.DatPhong)
            .OrderByDescending(d => d.NgaySuDung)
            .ToListAsync();
        }
    public async Task<DichVuPhatSinh?> GetDetailsAsync(int id)
        {
            return await _context.DichVuPhatSinhs
            .Include(d => d.DatPhong)
            .FirstOrDefaultAsync(d => d.MaDichVuPhatSinh == id);
        }
     public async Task<DichVuPhatSinh?> GetByIdAsync(int id)
        {
            return await _context.DichVuPhatSinhs
                .FindAsync(id);
        }

    public async Task<DatPhong?> GetDatPhongAsync(int? MaDatPhong)
        {
            return await _context.DatPhongs
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatPhong == MaDatPhong);
        }

        // Thêm dịch vụ
        public async Task AddAsync(DichVuPhatSinh dichVu)
        {
            await _context.DichVuPhatSinhs.AddAsync(dichVu);
            await _context.SaveChangesAsync();
        }

        // Lưu các thay đổi của dịch vụ
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}