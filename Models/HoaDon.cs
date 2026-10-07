
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class HoaDon
    {
       [Key]
        public int MaHoaDon { get; set; }
        [Required]
        public int MaDatPhong { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Số đêm phải lớn hơn 0.")]
        public int SoDem { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tiền phòng phải là số dương.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TienPhong { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tiền dịch vụ phải là số dương.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TienDichVu { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giảm giá phải là số dương.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiamGia { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tổng thành toán phải là số dương.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TongThanhToan { get; set; }
        [Required]
        public HinhThucThanhToan HinhThucThanhToan { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        [Required]
        public TrangThaiHoaDon TrangThai { get; set; }
    }
    public enum HinhThucThanhToan
    {
        TienMat,
        ChuyenKhoan,
        TheTinDung
    }
    public enum TrangThaiHoaDon
    {
        ChuaThanhToan,
        DaThanhToan
    }
}