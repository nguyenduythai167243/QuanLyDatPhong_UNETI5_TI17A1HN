// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Quản lý trạng thái lưu trú và công suất loại phòng

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_Model4.Models.Entities
{
    [Table("LoaiPhong")]
    public class LoaiPhong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoaiPhong { get; set; }

        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên loại phòng tối đa 100 ký tự")]
        [Display(Name = "Tên loại phòng")]
        public string TenLoaiPhong { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Sức chứa tối đa không được để trống")]
        [Range(1, 20, ErrorMessage = "Sức chứa tối đa phải từ 1 đến 20 người")]
        [Display(Name = "Sức chứa tối đa")]
        public int SucChuaToiDa { get; set; }

        [Required(ErrorMessage = "Giá tham khảo không được để trống")]
        [Range(0, 100000000, ErrorMessage = "Giá tham khảo phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Giá tham khảo")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaThamKhao { get; set; }

        [Display(Name = "Trạng thái kinh doanh")]
        public bool TrangThai { get; set; } = true;

        // Navigation
        public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}
