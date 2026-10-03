// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Cấu hình DbContext, quan hệ thực thể, ràng buộc nghiệp vụ lưu trú

using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_Model4.Models.Entities;

namespace QuanLyDatPhong_Model4.Data
{
    public class QuanLyDatPhongDbContext : DbContext
    {
        public QuanLyDatPhongDbContext(DbContextOptions<QuanLyDatPhongDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public DbSet<LoaiPhong> LoaiPhongs { get; set; } = null!;
        public DbSet<Phong> Phongs { get; set; } = null!;
        public DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public DbSet<DatPhong> DatPhongs { get; set; } = null!;
        public DbSet<DichVuPhatSinh> DichVuPhatSinhs { get; set; } = null!;
        public DbSet<HoaDon> HoaDons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. TaiKhoan - Tên đăng nhập duy nhất
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasIndex(e => e.TenDangNhap).IsUnique();
            });

            // 2. LoaiPhong - Tên loại phòng duy nhất
            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.HasIndex(e => e.TenLoaiPhong).IsUnique();
            });

            // 3. Phong - Số phòng duy nhất
            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasIndex(e => e.SoPhong).IsUnique();

                entity.HasOne(p => p.LoaiPhong)
                      .WithMany(lp => lp.Phongs)
                      .HasForeignKey(p => p.MaLoaiPhong)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 4. KhachHang - Quan hệ 1 - 0..1 với TaiKhoan
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasOne(kh => kh.TaiKhoan)
                      .WithOne(tk => tk.KhachHang)
                      .HasForeignKey<KhachHang>(kh => kh.MaTaiKhoan)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // 5. DatPhong - Quan hệ với KhachHang và Phong
            modelBuilder.Entity<DatPhong>(entity =>
            {
                entity.HasOne(dp => dp.KhachHang)
                      .WithMany(kh => kh.DatPhongs)
                      .HasForeignKey(dp => dp.MaKhachHang)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dp => dp.Phong)
                      .WithMany(p => p.DatPhongs)
                      .HasForeignKey(dp => dp.MaPhong)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 6. DichVuPhatSinh - Quan hệ với DatPhong
            modelBuilder.Entity<DichVuPhatSinh>(entity =>
            {
                entity.HasOne(dv => dv.DatPhong)
                      .WithMany(dp => dp.DichVuPhatSinhs)
                      .HasForeignKey(dv => dv.MaDatPhong)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 7. HoaDon - Quan hệ 1 - 0..1 với DatPhong (Mã đặt phòng duy nhất trên hóa đơn)
            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasIndex(hd => hd.MaDatPhong).IsUnique();

                entity.HasOne(hd => hd.DatPhong)
                      .WithOne(dp => dp.HoaDon)
                      .HasForeignKey<HoaDon>(hd => hd.MaDatPhong)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
