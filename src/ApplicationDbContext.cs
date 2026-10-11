using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.Phong> Phong { get; set; } = default!;
    public DbSet<QuanLyDatPhong_UNETI5_TI17A1HN.Models.LoaiPhong> LoaiPhong { get; set; } = default!;
}
