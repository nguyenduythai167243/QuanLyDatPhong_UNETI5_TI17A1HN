
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.Phong> Phong { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.LoaiPhong> LoaiPhong { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.KhachHang> KhachHang { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.DatPhong> DatPhong { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.DichVuPhatSinh> DichVuPhatSinh { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.HoaDon> HoaDon { get; set; } = default!;
}
