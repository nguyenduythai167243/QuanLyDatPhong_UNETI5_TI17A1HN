
using Microsoft.EntityFrameworkCore;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<DichVuPhatSinh> DichVuPhatSinhs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
    }
}
