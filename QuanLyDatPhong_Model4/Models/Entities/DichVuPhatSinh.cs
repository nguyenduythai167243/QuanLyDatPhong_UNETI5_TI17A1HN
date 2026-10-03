// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Quản lý dịch vụ phát sinh gắn với quy trình lưu trú và trả phòng

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_Model4.Models.Entities
{
    [Table("DichVuPhatSinh")]
    public class DichVuPhatSinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDichVuPhatSinh { get; set; }

        [Required]
        [Display(Name = "Mã đặt phòng")]
        public int MaDatPhong { get; set; }

        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        [StringLength(100, ErrorMessage = "Tên dịch vụ tối đa 100 ký tự")]
        [Display(Name = "Tên dịch vụ")]
        public string TenDichVu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, 1000, ErrorMessage = "Số lượng phải lớn hơn 0")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; } = 1;

        [Required(ErrorMessage = "Đơn giá không được để trống")]
        [Range(0, 100000000, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Đơn giá (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [Required(ErrorMessage = "Ngày sử dụng không được để trống")]
        [Display(Name = "Ngày sử dụng")]
        public DateTime NgaySuDung { get; set; } = DateTime.Now;

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Trạng thái dịch vụ")]
        public string TrangThai { get; set; } = "Đã ghi nhận"; // Đã ghi nhận, Đã hủy

        [ForeignKey("MaDatPhong")]
        public virtual DatPhong? DatPhong { get; set; }

        [NotMapped]
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
