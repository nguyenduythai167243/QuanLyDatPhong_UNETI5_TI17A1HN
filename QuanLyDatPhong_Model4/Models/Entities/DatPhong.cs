// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Tiếp nhận đặt phòng, xác nhận/từ chối, nhận phòng, trả phòng, quản lý trạng thái lưu trú

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_Model4.Models.Entities
{
    [Table("DatPhong")]
    public class DatPhong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDatPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
        [Display(Name = "Khách hàng")]
        public int MaKhachHang { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phòng")]
        [Display(Name = "Phòng")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày nhận phòng")]
        public DateTime NgayNhanPhong { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng không được để trống")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả phòng")]
        public DateTime NgayTraPhong { get; set; }

        [Required(ErrorMessage = "Số người không được để trống")]
        [Range(1, 20, ErrorMessage = "Số người lưu trú phải từ 1 đến 20")]
        [Display(Name = "Số người")]
        public int SoNguoi { get; set; }

        [Display(Name = "Yêu cầu đặc biệt")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string? YeuCauDacBiet { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Display(Name = "Tổng tiền dự kiến (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTienDuKien { get; set; }

        [Display(Name = "Ngày nhận thực tế")]
        public DateTime? NgayNhanThucTe { get; set; }

        [Display(Name = "Ngày trả thực tế")]
        public DateTime? NgayTraThucTe { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái đặt phòng")]
        // Trạng thái: "Chờ xác nhận", "Đã xác nhận", "Đã nhận phòng", "Đã trả phòng", "Từ chối", "Đã hủy"
        public string TrangThai { get; set; } = "Chờ xác nhận";

        [Display(Name = "Ngày xử lý")]
        public DateTime? NgayXuLy { get; set; }

        [Display(Name = "Ghi chú lễ tân")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string? GhiChuLeTan { get; set; }

        // Navigation Properties
        [ForeignKey("MaKhachHang")]
        public virtual KhachHang? KhachHang { get; set; }

        [ForeignKey("MaPhong")]
        public virtual Phong? Phong { get; set; }

        public virtual ICollection<DichVuPhatSinh> DichVuPhatSinhs { get; set; } = new List<DichVuPhatSinh>();

        public virtual HoaDon? HoaDon { get; set; }

        // Helper properties for UI
        [NotMapped]
        public int SoDemDuKien => (NgayTraPhong - NgayNhanPhong).Days > 0 ? (NgayTraPhong - NgayNhanPhong).Days : 1;

        [NotMapped]
        public bool IsQuaHanNhanPhong => TrangThai == "Đã xác nhận" && DateTime.Today > NgayNhanPhong.Date;
    }
}
