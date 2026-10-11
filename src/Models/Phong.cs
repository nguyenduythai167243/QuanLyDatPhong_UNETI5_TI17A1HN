using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }

        [Required]
        public int MaLoaiPhong { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public int SoPhong { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int Tang { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SucChua { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal GiaMotDem { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal DienTich { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string TienNghi { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string TrangThai { get; set; } = string.Empty;

        [ForeignKey("MaLoaiPhong")]
        public virtual LoaiPhong? LoaiPhong { get; set; }
        // public virtual ICollection<DatPhong> ?DatPhongs { get; set; }
    }
}
