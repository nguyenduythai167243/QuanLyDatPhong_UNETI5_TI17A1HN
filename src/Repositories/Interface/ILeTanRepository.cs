// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Interface Repository quản lý Lễ tân và Lưu trú (ILeTanRepository)

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Repositories
{
    /// <summary>
    /// Giao diện Repository phục vụ toàn bộ nghiệp vụ Tiếp nhận, Xác nhận và Quản lý lưu trú của Lễ tân (Module 4)
    /// </summary>
    public interface ILeTanRepository
    {
        #region 1. Thống kê & Danh sách đặt phòng
        /// <summary>
        /// Thống kê nhanh các chỉ số KPI đặt phòng dành cho Lễ tân
        /// </summary>
        Task<ThongKeKpiLeTanDto> GetThongKeKpiAsync();

        /// <summary>
        /// Lấy danh sách đặt phòng có tìm kiếm, lọc theo nhiều tiêu chí, sắp xếp và phân trang
        /// </summary>
        Task<PaginatedList<DatPhong>> GetDanhSachDatPhongAsync(
            string? tuKhoa,
            int? maLoaiPhong,
            string? trangThai,
            DateTime? ngayNhanPhong,
            string? sortOrder,
            bool? canhBaoQuaHan,
            int page,
            int pageSize);
        #endregion

        #region 2. Chi tiết & Kiểm tra điều kiện nghiệp vụ (LINQ)
        /// <summary>
        /// Lấy thông tin chi tiết đặt phòng theo mã
        /// </summary>
        Task<DatPhong?> GetDatPhongByIdAsync(int id, bool includeFullDetails = true);

        /// <summary>
        /// Kiểm tra toàn bộ điều kiện nghiệp vụ theo thời gian thực trước khi xác nhận đặt phòng (Mục 8.3 & 8.5)
        /// </summary>
        Task<KiemTraDatPhongResult> KiemTraDieuKienDatPhongAsync(DatPhong datPhong);

        /// <summary>
        /// Kiểm tra điều kiện nghiệp vụ đặt phòng theo mã đặt phòng
        /// </summary>
        Task<KiemTraDatPhongResult> KiemTraDieuKienDatPhongAsync(int maDatPhong);
        #endregion

        #region 3. Xác nhận, Từ chối và Hủy đặt phòng
        /// <summary>
        /// Xác nhận đặt phòng sau khi kiểm tra đầy đủ điều kiện hợp lệ
        /// </summary>
        Task<(bool Success, string Message)> XacNhanDatPhongAsync(int id, string? ghiChuLeTan);

        /// <summary>
        /// Từ chối đặt phòng và ghi rõ lý do
        /// </summary>
        Task<(bool Success, string Message)> TuChoiDatPhongAsync(int id, string lyDoTuChoi);

        /// <summary>
        /// Lễ tân hủy đặt phòng quá hạn nhận phòng (No-show)
        /// </summary>
        Task<(bool Success, string Message)> HuyQuaHanAsync(int id, string? lyDo);

        /// <summary>
        /// Hủy đặt phòng khi ở trạng thái hợp lệ (Chờ xác nhận hoặc Đã xác nhận)
        /// </summary>
        Task<(bool Success, string Message)> HuyDatPhongAsync(int id, string? lyDo);
        #endregion

        #region 4. Quy trình nhận phòng (Check-in) & trả phòng (Check-out)
        /// <summary>
        /// Làm thủ tục nhận phòng (Check-in), cập nhật thời gian thực tế và trạng thái phòng
        /// </summary>
        Task<(bool Success, string Message)> NhanPhongAsync(int id, string? ghiChu);

        /// <summary>
        /// Làm thủ tục trả phòng (Check-out), cập nhật thời gian thực tế và đưa phòng về sẵn sàng
        /// </summary>
        Task<(bool Success, string Message)> TraPhongAsync(int id, string? ghiChu);
        #endregion

        #region 5. Kiểm soát công suất phòng theo loại phòng (Mục 8.5)
        /// <summary>
        /// Tra cứu và kiểm tra tỷ lệ lấp đầy, số phòng kín lịch theo loại phòng trong khoảng ngày bằng LINQ
        /// </summary>
        Task<List<CongSuatLoaiPhongDto>> KiemTraCongSuatAsync(DateTime checkIn, DateTime checkOut);
        #endregion

        #region 6. Danh mục phụ & Thao tác CRUD bổ trợ
        Task<List<KhachHang>> GetDanhSachKhachHangHoatDongAsync();
        Task<List<Phong>> GetDanhSachPhongSanSangAsync();
        Task<List<LoaiPhong>> GetDanhSachLoaiPhongAsync();
        Task<Phong?> GetPhongByIdAsync(int maPhong);
        Task<bool> IsPhongTrungLichAsync(int maPhong, DateTime checkIn, DateTime checkOut, int? excludeMaDatPhong = null);
        Task<bool> CreateDatPhongAsync(DatPhong datPhong);
        Task<bool> UpdateDatPhongAsync(DatPhong datPhong);
        Task<(bool Success, string Message)> DeleteDatPhongAsync(int id);
        #endregion
    }

    /// <summary>
    /// Alias interface để tương thích linh hoạt cho cả ILeTanRepository và ILeTanDatPhongRepository
    /// </summary>
    public interface ILeTanDatPhongRepository : ILeTanRepository
    {
    }

    /// <summary>
    /// DTO chứa thông tin các chỉ số thống kê KPI của Lễ tân
    /// </summary>
    public class ThongKeKpiLeTanDto
    {
        public int TongDon { get; set; }
        public int SoChoXacNhan { get; set; }
        public int SoDaXacNhan { get; set; }
        public int SoDangLuuTru { get; set; }
        public int SoDaTraPhong { get; set; }
        public int SoQuaHanChuaNhan { get; set; }
    }

    /// <summary>
    /// DTO báo cáo công suất theo từng loại phòng trong khoảng ngày
    /// </summary>
    public class CongSuatLoaiPhongDto
    {
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; } = string.Empty;
        public int SucChuaToiDa { get; set; }
        public decimal GiaThamKhao { get; set; }
        public int TongPhong { get; set; }
        public int PhongSanSang { get; set; }
        public int PhongDaDat { get; set; }
        public int PhongConTrong { get; set; }
        public double TyLeLapDay { get; set; }
        public bool KinLich { get; set; }
    }
}
