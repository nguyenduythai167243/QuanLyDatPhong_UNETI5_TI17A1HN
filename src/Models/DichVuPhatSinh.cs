using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class DichVuPhatSinh
    {
        [Key]
        public int MaDichVuPhatSinh { get; set; }
        //[ForeignKey]
        //public int MaDatPhong { get; set; }
        [Required, MaxLength(100)]
        public string TenDichVu { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải là lớn hơn 0.")]
        public int SoLuong { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải là số dương.")]
        public decimal DonGia { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySuDung { get; set; }
        [Required, MaxLength(200)]
        public string GhiChu { get; set; } = string.Empty;
        [Required]
        public TrangThai TrangThai { get; set; }
        [NotMapped]
        public decimal ThanhTien => SoLuong * DonGia;
    }
    public enum TrangThai
    {
        DaGhiNhan,
        DaHuy,
    }
}
