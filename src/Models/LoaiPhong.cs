using System.ComponentModel.DataAnnotations;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoaiPhong { get; set; } = string.Empty;

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SucChuaToiDa { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GiaThamKhao { get; set; }

        [Required]
        public bool TrangThai { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}