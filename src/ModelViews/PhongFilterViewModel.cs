using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels
{
    public class PhongFilterViewModel
    {
        // 1. Tìm kiếm
        public string? TuKhoa { get; set; } // Tìm theo số phòng hoặc tên loại phòng

        // 2. Bộ lọc
        public int? MaLoaiPhong { get; set; }
        public int? Tang { get; set; }
        public int? SucChuaToiThieu { get; set; }
        public decimal? GiaTu { get; set; }
        public decimal? GiaDen { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayNhanPhong { get; set; }
        public DateTime? NgayTraPhong { get; set; }

        public string? SortOrder { get; set; } = "so_phong_asc";

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;

        // Kết quả dữ liệu
        public PaginatedList<Phong>? DanhSachPhong { get; set; }

        // Dropdown cho bộ lọc
        public SelectList? LoaiPhongList { get; set; }
        public List<int>? DanhSachTang { get; set; }

        // Bổ sung thuộc tính kiểm tra xem phòng có trống trong ngày đã chọn không (dùng khi khách lọc ngày)
        public Dictionary<int, bool> PhongTrongMap { get; set; } = new Dictionary<int, bool>();
    }
}
