using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class DatPhong
    {
        [Key]
        public int MaDatPhong {  get; set; }
        [Required]
        public int MaKhachHang {  get; set; }
        [Required]
        public int MaPhong {  get; set; }
        [Required(ErrorMessage ="Ngày nhận phòng không được để trống")]
        [DataType(DataType.Date)]
        public DateTime NgayNhanPhong { get; set; }
        [Required(ErrorMessage ="Ngày trả phòng không được để trống")]
        [DataType(DataType.Date)]
        public DateTime NgayTraPhong { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Số người phải lớn hơn 0")]
        public int SoNguoi {  get; set; }
        [StringLength(10)]
        public String? YeuCauDacBiet { get; set;  }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime NgayDat {  get; set; }
        [Column(TypeName ="decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal TongTienDuKien { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? NgayNhanThucTe {  get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? NgayTraThucTe { get; set; }
        [Required]
        [StringLength(10)]
        public String TrangThai {  get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? NgayXuLy {  get; set; }
        [StringLength(10)]
        public String GhiChuLeTan {  get; set; }
        [ForeignKey("MaKhachHang")]
        public virtual KhachHang? KhachHang { get; set; }
        [ForeignKey("MaPhong")]
        public virtual Phong? Phong { get; set; }
        public virtual ICollection<DichVuPhatSinh> DichVuPhatSinhs { get; set; } = new List<DichVuPhatSinh>();
        public virtual HoaDon? HoaDon { get; set; }
    }
}
