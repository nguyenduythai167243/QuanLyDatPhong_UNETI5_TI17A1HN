
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class DichVuPhatSinh
    {
        [Key]
        public int MaDichVuPhatSinh { get; set; }
        [Required]
        [ForeignKey("DatPhong")]
        public int MaDatPhong { get; set; }
        [Required]
        [StringLength(200)]
        public string TenDichVu { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int SoLuong { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải là số dương.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DonGia { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySuDung { get; set; }
        [StringLength(500)]
        public string? GhiChu { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
        [NotMapped]
        public decimal ThanhTien => SoLuong * DonGia;

    }
    
}
