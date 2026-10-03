// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - ViewModel quản lý danh sách đặt phòng, lọc, tìm kiếm, sắp xếp, phân trang và kiểm tra điều kiện

using System.ComponentModel.DataAnnotations;
using QuanLyDatPhong_Model4.Models.Entities;

namespace QuanLyDatPhong_Model4.Models.ViewModels
{
    // ViewModel cho Danh sách tiếp nhận đặt phòng (Section 8.1)
    public class DatPhongListViewModel
    {
        // 1. Tìm kiếm & Lọc
        [Display(Name = "Tìm kiếm khách hàng / số phòng")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Lọc loại phòng")]
        public int? MaLoaiPhong { get; set; }

        [Display(Name = "Lọc trạng thái")]
        public string? TrangThai { get; set; }

        [Display(Name = "Lọc ngày nhận phòng")]
        [DataType(DataType.Date)]
        public DateTime? NgayNhanPhong { get; set; }

        [Display(Name = "Chỉ xem đặt phòng quá hạn nhận")]
        public bool ChiXemQuaHan { get; set; } = false;

        // 2. Sắp xếp
        [Display(Name = "Sắp xếp theo")]
        public string SortOrder { get; set; } = "NgayDat_Desc";

        // 3. Phân trang
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        // 4. Danh sách dữ liệu sau khi lọc, sắp xếp, phân trang bằng LINQ
        public List<DatPhong> Items { get; set; } = new List<DatPhong>();

        // 5. Thống kê nhanh trạng thái cho thanh badge/tab
        public int CountChoXacNhan { get; set; }
        public int CountDaXacNhan { get; set; }
        public int CountDaNhanPhong { get; set; }
        public int CountDaTraPhong { get; set; }
        public int CountTuChoi { get; set; }
        public int CountDaHuy { get; set; }
        public int CountQuaHanNoShow { get; set; }
    }

    // ViewModel cho Xác nhận / Từ chối đặt phòng (Section 8.2 & 8.3)
    public class XacNhanDatPhongViewModel
    {
        public int MaDatPhong { get; set; }
        public DatPhong? DatPhong { get; set; }

        // Kết quả kiểm tra điều kiện tự động bằng LINQ
        public bool PhongSanSang { get; set; }
        public bool KhachHangHoatDong { get; set; }
        public bool CoDatPhongTrungLich { get; set; }
        public List<DatPhong> DanhSachTrungLich { get; set; } = new List<DatPhong>();
        public bool LoaiPhongKinLich { get; set; }
        public int SoPhongSanSangCuaLoai { get; set; }
        public int SoPhongDaKinhCuaLoai { get; set; }

        public bool DuDieuKienXacNhan => PhongSanSang && KhachHangHoatDong && !CoDatPhongTrungLich && !LoaiPhongKinLich;

        // Dữ liệu nhập từ Lễ tân
        [Required(ErrorMessage = "Vui lòng chọn quyết định xử lý")]
        [Display(Name = "Quyết định")]
        public string HanhDong { get; set; } = "XacNhan"; // "XacNhan" hoặc "TuChoi"

        [Required(ErrorMessage = "Vui lòng nhập ghi chú của lễ tân khi xử lý")]
        [StringLength(500, ErrorMessage = "Ghi chú tối đa 500 ký tự")]
        [Display(Name = "Ghi chú lễ tân")]
        public string GhiChuLeTan { get; set; } = string.Empty;
    }

    // ViewModel kiểm soát công suất phòng theo loại phòng (Section 8.5)
    public class KiemSoatCongSuatViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày")]
        public DateTime TuNgay { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày")]
        public DateTime DenNgay { get; set; } = DateTime.Today.AddDays(7);

        public List<LoaiPhongCongSuatItem> ThongKeLoaiPhong { get; set; } = new List<LoaiPhongCongSuatItem>();
        public List<PhongGridItem> SoDoPhong { get; set; } = new List<PhongGridItem>();
    }

    public class LoaiPhongCongSuatItem
    {
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; } = string.Empty;
        public int TongSoPhong { get; set; }
        public int SoPhongSanSang { get; set; }
        public int SoPhongBaoTri { get; set; }
        public int SoPhongDaCoLich { get; set; }
        public int SoPhongConTrong => Math.Max(0, SoPhongSanSang - SoPhongDaCoLich);
        public double TyLeLapDay => SoPhongSanSang > 0 ? Math.Round((double)SoPhongDaCoLich / SoPhongSanSang * 100, 1) : 0;
        public bool IsKinLich => SoPhongConTrong <= 0 && SoPhongSanSang > 0;
    }

    public class PhongGridItem
    {
        public int MaPhong { get; set; }
        public string SoPhong { get; set; } = string.Empty;
        public int Tang { get; set; }
        public string TenLoaiPhong { get; set; } = string.Empty;
        public decimal GiaMotDem { get; set; }
        public string TrangThaiVatLy { get; set; } = "Sẵn sàng"; // Sẵn sàng, Đang bảo trì, Ngừng kinh doanh
        public string TrangThaiHienTai { get; set; } = "Trống"; // Trống, Đang có khách, Sắp nhận hôm nay, Đang bảo trì
        public DatPhong? DatPhongHienTai { get; set; }
    }
}
