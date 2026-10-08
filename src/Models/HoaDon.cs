using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }
        [Required]
        public int MaDatPhong { get; set; }
        [Range (1, int.MaxValue)]
        public int SoDem { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TienPhong { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TienDichVu { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiamGia { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TongTienThanhToan { get; set; }
        [Required]
        public HinhThucThanhToan HinhThucThanhToan { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        [Required]
        public TrangThaiTT TrangThai { get; set; }

    }
    public enum HinhThucThanhToan
    {
        TienMat,
        ChuyenKhoan,
        The,
    }
    public enum TrangThaiTT
    {
        DaThanhToan,
        ChuaThanhToan,
    }
}
