using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKhachHang { get; set; }
        [Required]
        public int MaTaiKhoan {  get; set; }
        [Required(ErrorMessage ="Họ tên không được để trống")]
        [StringLength (15)]
        public String HoTen { get; set; }
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        [Phone(ErrorMessage ="Số điện thoại không hợp lệ")]
        [StringLength(10)]
        public String SoDienThoai { get; set; }
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        [StringLength(15)]
        public String Email { get; set; }
        [StringLength (20)]
        public String DiaChi { get; set; }
        [Required(ErrorMessage ="Số giấy tờ tùy thân không hợp lệ")]
        [StringLength(10)]
        public String SoGiayToTuyThan {  get; set; }
        [StringLength(5)]
        public String QuocTich {  get; set; }
        public bool TrangThai { get; set; }
        [ForeignKey("MaTaiKhoan")]
        public virtual TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}
