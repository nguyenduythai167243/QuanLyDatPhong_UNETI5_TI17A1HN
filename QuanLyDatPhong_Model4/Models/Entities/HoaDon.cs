// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Quản lý hóa đơn kết thúc quy trình trả phòng

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_Model4.Models.Entities
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDon { get; set; }

        [Required]
        [Display(Name = "Mã đặt phòng")]
        public int MaDatPhong { get; set; }

        [Required]
        [Range(1, 365, ErrorMessage = "Số đêm tối thiểu là 1")]
        [Display(Name = "Số đêm")]
        public int SoDem { get; set; } = 1;

        [Required]
        [Display(Name = "Tiền phòng (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TienPhong { get; set; }

        [Required]
        [Display(Name = "Tiền dịch vụ (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TienDichVu { get; set; }

        [Display(Name = "Giảm giá (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiamGia { get; set; } = 0;

        [Required]
        [Display(Name = "Tổng thanh toán (VNĐ)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TongThanhToan { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Hình thức thanh toán")]
        public string HinhThucThanhToan { get; set; } = "Tiền mặt"; // Tiền mặt, Chuyển khoản, Thẻ

        [Display(Name = "Ngày lập hóa đơn")]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái thanh toán")]
        public string TrangThai { get; set; } = "Chưa thanh toán"; // Chưa thanh toán, Đã thanh toán

        [ForeignKey("MaDatPhong")]
        public virtual DatPhong? DatPhong { get; set; }
    }
}
