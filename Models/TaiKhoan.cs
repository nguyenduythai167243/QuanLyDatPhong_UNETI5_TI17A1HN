using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Models
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 ký tự.")]
        
        public string TenDangNhap { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, ErrorMessage = "Mật khẩu không được quá 100 ký tự.")]
        [DataType(DataType.Password)]
        
        public string MatKhau { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự.")]
       
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(100, ErrorMessage = "Email không được quá 100 ký tự.")]
        
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vai trò không được để trống.")]
        
        public string VaiTro { get; set; } = "KhachHang";

        public bool TrangThai { get; set; } = true;
    }
}