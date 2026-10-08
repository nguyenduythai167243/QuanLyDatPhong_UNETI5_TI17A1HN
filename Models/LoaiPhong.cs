using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }

        [Required(ErrorMessage = "Tên loại phòng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên loại phòng không được quá 100 ký tự.")]
        public string TenLoaiPhong { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự.")]
        public string? MoTa { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sức chứa tối đa phải lớn hơn 0.")]
        public int SucChuaToiDa { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá tham khảo phải lớn hơn hoặc bằng 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaThamKhao { get; set; }

        public bool TrangThai { get; set; } = true;
    }
}