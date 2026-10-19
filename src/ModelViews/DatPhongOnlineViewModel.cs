using System;
using System.ComponentModel.DataAnnotations;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels
{
    public class DatPhongOnlineViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn phòng cần đặt")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày nhận phòng")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày nhận phòng")]
        public DateTime NgayNhanPhong { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn ngày trả phòng")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả phòng")]
        public DateTime NgayTraPhong { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Vui lòng nhập số người")]
        [Range(1, 20, ErrorMessage = "Số người từ 1 đến 20")]
        [Display(Name = "Số người")]
        public int SoNguoi { get; set; } = 1;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên của bạn")]
        [StringLength(50, ErrorMessage = "Họ tên tối đa 50 ký tự")]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(20, ErrorMessage = "Số điện thoại tối đa 20 ký tự")]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số CCCD hoặc Hộ chiếu")]
        [StringLength(30, ErrorMessage = "Số giấy tờ tùy thân tối đa 30 ký tự")]
        [Display(Name = "Số CCCD / Hộ chiếu")]
        public string SoGiayToTuyThan { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(50, ErrorMessage = "Email tối đa 50 ký tự")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(100, ErrorMessage = "Địa chỉ tối đa 100 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [StringLength(255, ErrorMessage = "Yêu cầu đặc biệt tối đa 255 ký tự")]
        [Display(Name = "Yêu cầu đặc biệt")]
        public string? YeuCauDacBiet { get; set; }

        // Dữ liệu hiển thị chi tiết phòng
        public Phong? Phong { get; set; }
        public int SoDem => Math.Max(1, (int)(NgayTraPhong.Date - NgayNhanPhong.Date).TotalDays);
        public decimal TongTienDuKien => (Phong?.GiaMotDem ?? 0) * SoDem;
    }
}
