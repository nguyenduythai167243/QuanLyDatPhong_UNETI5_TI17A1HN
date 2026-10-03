// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Quản lý phòng lưu trú, trạng thái và kiểm tra trùng lịch

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_Model4.Models.Entities
{
    [Table("Phong")]
    public class Phong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Số phòng không được để trống")]
        [StringLength(20, ErrorMessage = "Số phòng tối đa 20 ký tự")]
        [Display(Name = "Số phòng")]
        public string SoPhong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn loại phòng")]
        [Display(Name = "Loại phòng")]
        public int MaLoaiPhong { get; set; }

        [Required(ErrorMessage = "Tầng không được để trống")]
        [Range(1, 50, ErrorMessage = "Tầng phải từ 1 trở lên")]
        [Display(Name = "Tầng")]
        public int Tang { get; set; }

        [Required(ErrorMessage = "Sức chứa không được để trống")]
        [Range(1, 20, ErrorMessage = "Sức chứa phải lớn hơn 0")]
        [Display(Name = "Sức chứa")]
        public int SucChua { get; set; }

        [Required(ErrorMessage = "Giá một đêm không được để trống")]
        [Range(1000, 100000000, ErrorMessage = "Giá một đêm phải lớn hơn 0")]
        [Display(Name = "Giá một đêm (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaMotDem { get; set; }

        [Display(Name = "Diện tích (m²)")]
        [Range(5, 500, ErrorMessage = "Diện tích từ 5m² đến 500m²")]
        public double DienTich { get; set; }

        [Display(Name = "Tiện nghi")]
        [DataType(DataType.MultilineText)]
        public string? TienNghi { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        [StringLength(30)]
        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; } = "Sẵn sàng"; // Sẵn sàng, Đang bảo trì, Ngừng kinh doanh

        // Navigations
        [ForeignKey("MaLoaiPhong")]
        public virtual LoaiPhong? LoaiPhong { get; set; }

        public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}
