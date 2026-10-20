// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - ViewModels phục vụ Tiếp nhận, Lọc/Tìm kiếm, Kiểm tra điều kiện và Luồng trạng thái Đặt phòng

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels
{
    /// <summary>
    /// ViewModel phục vụ tìm kiếm, lọc, sắp xếp, phân trang và thống kê nhanh danh sách đặt phòng
    /// </summary>
    public class DatPhongFilterViewModel
    {
        // 1. Tiêu chí tìm kiếm & lọc
        public string? TuKhoa { get; set; }
        public int? MaLoaiPhong { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayNhanPhong { get; set; }
        public string? SortOrder { get; set; }
        public bool? CanhBaoQuaHan { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // 2. Dữ liệu danh mục hỗ trợ SelectList
        public SelectList? LoaiPhongSelectList { get; set; }
        public List<string> TrangThaiList { get; set; } = new()
        {
            "Chờ xác nhận",
            "Đã xác nhận",
            "Đã nhận phòng",
            "Đã trả phòng",
            "Từ chối",
            "Đã hủy"
        };

        // 3. Kết quả phân trang
        public PaginatedList<DatPhong>? DanhSach { get; set; }

        // 4. Thống kê nhanh KPI theo dõi của Lễ tân
        public int TongDon { get; set; }
        public int SoChoXacNhan { get; set; }
        public int SoDaXacNhan { get; set; }
        public int SoDangLuuTru { get; set; }
        public int SoDaTraPhong { get; set; }
        public int SoQuaHanChuaNhan { get; set; }
    }

    /// <summary>
    /// Kết quả kiểm tra nghiệp vụ và điều kiện trước khi xác nhận / xử lý đặt phòng (LINQ)
    /// </summary>
    public class KiemTraDatPhongResult
    {
        public int MaDatPhong { get; set; }
        public bool HopLeDeXacNhan { get; set; }

        // Checklist từng điều kiện bắt buộc
        public bool DatPhongTonTai { get; set; }
        public bool DangChoXacNhan { get; set; }
        public bool PhongSanSang { get; set; }
        public string? TrangThaiHienTaiCuaPhong { get; set; }
        public bool KhachHangHoatDong { get; set; }
        public bool CoBiTrungLich { get; set; }
        public List<DatPhong> DanhSachTrungLich { get; set; } = new();

        // Kiểm soát công suất phòng theo loại phòng
        public bool LoaiPhongBiKinLich { get; set; }
        public int TongPhongSanSangCuaLoai { get; set; }
        public int SoPhongDaKinLichTrongKhoang { get; set; }

        // Trạng thái ngày nhận/trả
        public bool DaDenNgayNhan { get; set; }
        public bool QuaHanNhanPhong { get; set; }

        // Danh sách thông điệp chi tiết
        public List<string> DanhSachLoi { get; set; } = new();
        public List<string> DanhSachCanhBao { get; set; } = new();
    }

    /// <summary>
    /// ViewModel màn hình chi tiết và xử lý thao tác nghiệp vụ đặt phòng của Lễ tân
    /// </summary>
    public class XuLyDatPhongViewModel
    {
        public DatPhong DatPhong { get; set; } = null!;
        public KiemTraDatPhongResult KiemTra { get; set; } = new();

        public int SoDem { get; set; }
        public string? GhiChuLeTan { get; set; }
        public string? LyDoTuChoi { get; set; }
        public string? HanhDong { get; set; }

        // Kiểm tra xem phòng có thể nhận ngay hôm nay không
        public bool CoTheNhanPhong => DatPhong.TrangThai == "Đã xác nhận" &&
                                      DatPhong.NgayNhanPhong.Date <= DateTime.Today;

        // Đơn đã xác nhận nhưng quá ngày nhận phòng mà chưa nhận
        public bool LaDonQuaHan => DatPhong.TrangThai == "Đã xác nhận" &&
                                   DatPhong.NgayNhanPhong.Date < DateTime.Today;
    }
}