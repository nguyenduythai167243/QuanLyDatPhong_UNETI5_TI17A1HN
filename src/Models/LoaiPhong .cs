using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string TenLoaiPhong { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; } = string.Empty;

        [Required]
        [Range(1, double.MaxValue)]
        public int SucChuaToiDa { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GiaThamKhao { get; set; }

        [Required]
        public bool TrangThai { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}
