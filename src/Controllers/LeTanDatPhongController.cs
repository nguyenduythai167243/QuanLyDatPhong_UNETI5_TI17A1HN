// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: MODULE 4 - TIẾP NHẬN, XÁC NHẬN VÀ QUẢN LÝ TRẠNG THÁI LƯU TRÚ (LeTanDatPhongController)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    [SessionAuthorize("Admin", "LeTan", "Lễ tân")]
    public class LeTanDatPhongController : Controller
    {
        private readonly ILeTanRepository _leTanRepository;

        public LeTanDatPhongController(ILeTanRepository leTanRepository)
        {
            _leTanRepository = leTanRepository;
        }

        #region 8.1. DANH SÁCH ĐẶT PHÒNG CẦN XỬ LÝ (TÌM KIẾM, LỌC, SẮP XẾP, PHÂN TRANG)

        /// <summary>
        /// Màn hình danh sách đặt phòng dành cho Lễ tân
        /// Hỗ trợ tìm kiếm theo họ tên khách, số phòng; lọc theo loại phòng, trạng thái, ngày nhận phòng;
        /// lọc cảnh báo quá hạn nhận phòng (no-show); sắp xếp theo ngày đặt hoặc ngày nhận; phân trang EF Core.
        /// </summary>
        public async Task<IActionResult> Index(
            string? tuKhoa,
            int? maLoaiPhong,
            string? trangThai,
            DateTime? ngayNhanPhong,
            string? sortOrder,
            bool? canhBaoQuaHan,
            int page = 1)
        {
            // 1. Lấy thống kê KPI nhanh cho Lễ tân qua Repository
            var kpi = await _leTanRepository.GetThongKeKpiAsync();

            // 2. Phân trang dữ liệu đặt phòng qua Repository
            const int pageSize = 10;
            var pagedList = await _leTanRepository.GetDanhSachDatPhongAsync(
                tuKhoa,
                maLoaiPhong,
                trangThai,
                ngayNhanPhong,
                sortOrder,
                canhBaoQuaHan,
                page,
                pageSize);

            // 3. Danh sách loại phòng phục vụ SelectList
            var loaiPhongs = await _leTanRepository.GetDanhSachLoaiPhongAsync();

            var viewModel = new DatPhongFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoaiPhong = maLoaiPhong,
                TrangThai = trangThai,
                NgayNhanPhong = ngayNhanPhong,
                SortOrder = sortOrder,
                CanhBaoQuaHan = canhBaoQuaHan,
                Page = page,
                PageSize = pageSize,
                DanhSach = pagedList,
                LoaiPhongSelectList = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                TongDon = kpi.TongDon,
                SoChoXacNhan = kpi.SoChoXacNhan,
                SoDaXacNhan = kpi.SoDaXacNhan,
                SoDangLuuTru = kpi.SoDangLuuTru,
                SoDaTraPhong = kpi.SoDaTraPhong,
                SoQuaHanChuaNhan = kpi.SoQuaHanChuaNhan
            };

            return View(viewModel);
        }

        #endregion

        #region 8.2 & 8.3. XEM CHI TIẾT & KIỂM TRA ĐIỀU KIỆN ĐẶT PHÒNG

        /// <summary>
        /// Xem chi tiết đặt phòng kèm báo cáo kiểm tra điều kiện nghiệp vụ theo thời gian thực (LINQ)
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datPhong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datPhong == null)
            {
                return NotFound();
            }

            // Kiểm tra điều kiện nghiệp vụ theo LINQ qua Repository
            var kiemTraResult = await _leTanRepository.KiemTraDieuKienDatPhongAsync(datPhong);

            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var viewModel = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTraResult,
                SoDem = soDem,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(viewModel);
        }

        #endregion

        #region 8.2. XÁC NHẬN ĐẶT PHÒNG

        /// <summary>
        /// GET: LeTanDatPhong/XacNhan/5
        /// Hiển thị màn hình xem xét và xác nhận đặt phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> XacNhan(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Chờ xác nhận' mới được xác nhận. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var kiemTra = await _leTanRepository.KiemTraDieuKienDatPhongAsync(datPhong);
            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTra,
                SoDem = soDem,
                GhiChuLeTan = string.IsNullOrWhiteSpace(datPhong.GhiChuLeTan) ? "Đã liên hệ khách và xác nhận đặt phòng." : datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/XacNhan/5
        /// Thực hiện xác nhận đặt phòng sau khi kiểm tra đầy đủ điều kiện bằng LINQ (8.3 & 8.5)
        /// Ghi nhận NgayXuLy và GhiChuLeTan (8.2)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhan(int id, string? ghiChuLeTan)
        {
            var (success, message) = await _leTanRepository.XacNhanDatPhongAsync(id, ghiChuLeTan);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(XacNhan), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.2. TỪ CHỐI ĐẶT PHÒNG

        /// <summary>
        /// GET: LeTanDatPhong/TuChoi/5
        /// Hiển thị màn hình từ chối đặt phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TuChoi(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Chờ xác nhận' mới được từ chối. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var kiemTra = await _leTanRepository.KiemTraDieuKienDatPhongAsync(datPhong);
            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                KiemTra = kiemTra,
                SoDem = soDem,
                LyDoTuChoi = "Phòng không đủ điều kiện đáp ứng trong khoảng thời gian đã chọn."
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/TuChoi/5
        /// Cập nhật kết quả xử lý sang "Từ chối", ghi NgayXuLy và GhiChuLeTan (Mục 8.2)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TuChoi(int id, string lyDoTuChoi)
        {
            var (success, message) = await _leTanRepository.TuChoiDatPhongAsync(id, lyDoTuChoi);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(TuChoi), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.4. LÀM THỦ TỤC NHẬN PHÒNG (CHECK-IN)

        /// <summary>
        /// GET: LeTanDatPhong/NhanPhong/5
        /// Hiển thị màn hình xác nhận nhận phòng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> NhanPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datPhong == null) return NotFound();

            // Kiểm tra ràng buộc luồng trạng thái (Mục 8.4)
            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Đã xác nhận' mới được làm thủ tục nhận phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            // Kiểm tra ngày nhận phòng
            if (datPhong.NgayNhanPhong.Date > DateTime.Today)
            {
                TempData["ThongBaoCanhBao"] = $"Chưa đến ngày nhận phòng ({datPhong.NgayNhanPhong:dd/MM/yyyy}). Hôm nay là {DateTime.Today:dd/MM/yyyy}.";
            }

            var soDem = (int)(datPhong.NgayTraPhong.Date - datPhong.NgayNhanPhong.Date).TotalDays;
            if (soDem <= 0) soDem = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                SoDem = soDem,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/NhanPhong/5
        /// Thực hiện thủ tục nhận phòng (Check-in)
        /// Ghi nhận NgayNhanThucTe = DateTime.Now và chuyển trạng thái sang "Đã nhận phòng" (Mục 8.4)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NhanPhong(int id, string? ghiChu)
        {
            var (success, message) = await _leTanRepository.NhanPhongAsync(id, ghiChu);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.4. LÀM THỦ TỤC TRẢ PHÒNG (CHECK-OUT)

        /// <summary>
        /// GET: LeTanDatPhong/TraPhong/5
        /// Hiển thị màn hình trả phòng và tính toán lưu trú thực tế
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TraPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datPhong == null) return NotFound();

            if (datPhong.TrangThai != "Đã nhận phòng")
            {
                TempData["ThongBaoLoi"] = $"Chỉ đặt phòng ở trạng thái 'Đã nhận phòng' mới được làm thủ tục trả phòng. Trạng thái hiện tại: {datPhong.TrangThai}.";
                return RedirectToAction(nameof(Details), new { id = datPhong.MaDatPhong });
            }

            var ngayNhan = datPhong.NgayNhanThucTe ?? datPhong.NgayNhanPhong;
            var soDemThucTe = (int)(DateTime.Today - ngayNhan.Date).TotalDays;
            if (soDemThucTe <= 0) soDemThucTe = 1;

            var vm = new XuLyDatPhongViewModel
            {
                DatPhong = datPhong,
                SoDem = soDemThucTe,
                GhiChuLeTan = datPhong.GhiChuLeTan
            };

            return View(vm);
        }

        /// <summary>
        /// POST: LeTanDatPhong/TraPhong/5
        /// Thực hiện thủ tục trả phòng (Check-out)
        /// Ghi nhận NgayTraThucTe = DateTime.Now và chuyển trạng thái sang "Đã trả phòng" (Mục 8.4)
        /// Trả phòng về trạng thái "Sẵn sàng"
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TraPhong(int id, string? ghiChu)
        {
            var (success, message) = await _leTanRepository.TraPhongAsync(id, ghiChu);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.4 & 8.5. HỦY ĐẶT PHÒNG & XỬ LÝ QUÁ HẠN NHẬN PHÒNG (NO-SHOW)

        /// <summary>
        /// POST: LeTanDatPhong/HuyQuaHan/5
        /// Lễ tân hủy đặt phòng đã xác nhận nhưng khách quá ngày nhận phòng mà không đến nhận (No-show)
        /// Đáp ứng mục 8.5: Đánh dấu cảnh báo để Lễ tân quyết định hủy, không tự ý xóa dữ liệu.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyQuaHan(int id, string? lyDo)
        {
            var (success, message) = await _leTanRepository.HuyQuaHanAsync(id, lyDo);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Index), new { canhBaoQuaHan = true });
        }

        /// <summary>
        /// POST: LeTanDatPhong/HuyDatPhong/5
        /// Lễ tân hủy đặt phòng khi ở trạng thái phù hợp (Chờ xác nhận hoặc Đã xác nhận).
        /// Nghiêm cấm hủy khi đã nhận phòng hoặc đã trả phòng (Mục 8.4).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyDatPhong(int id, string? lyDo)
        {
            var (success, message) = await _leTanRepository.HuyDatPhongAsync(id, lyDo);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Details), new { id });
        }

        #endregion

        #region 8.5. TRA CỨU & KIỂM SOÁT CÔNG SUẤT PHÒNG THEO LOẠI PHÒNG (LINQ)

        /// <summary>
        /// GET: LeTanDatPhong/KiemTraCongSuat
        /// Action phục vụ tra cứu nhanh công suất phòng của toàn bộ khách sạn trong khoảng ngày bằng LINQ
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> KiemTraCongSuat(DateTime? tuNgay, DateTime? denNgay)
        {
            var checkIn = tuNgay?.Date ?? DateTime.Today;
            var checkOut = denNgay?.Date ?? DateTime.Today.AddDays(1);

            if (checkOut <= checkIn)
            {
                checkOut = checkIn.AddDays(1);
            }

            // Truy vấn công suất theo từng loại phòng qua Repository
            var danhSachBaoCao = await _leTanRepository.KiemTraCongSuatAsync(checkIn, checkOut);

            ViewBag.TuNgay = checkIn;
            ViewBag.DenNgay = checkOut;

            return View(danhSachBaoCao);
        }

        #endregion

        #region CÁC ACTION CRUD PHỤ TRỢ (TƯƠNG THÍCH HỆ THỐNG)

        // GET: LeTanDatPhong/Create (Tạo đặt phòng trực tiếp tại quầy của Lễ tân)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var khachHangs = await _leTanRepository.GetDanhSachKhachHangHoatDongAsync();
            var phongs = await _leTanRepository.GetDanhSachPhongSanSangAsync();

            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen");
            ViewBag.MaPhong = new SelectList(phongs.Select(p => new {
                p.MaPhong,
                TenHienThi = $"{p.SoPhong} - {p.LoaiPhong?.TenLoaiPhong} ({p.GiaMotDem:N0} đ/đêm)"
            }), "MaPhong", "TenHienThi");

            var model = new DatPhong
            {
                NgayDat = DateTime.Now,
                NgayNhanPhong = DateTime.Today,
                NgayTraPhong = DateTime.Today.AddDays(1),
                SoNguoi = 1,
                TrangThai = "Chờ xác nhận"
            };

            return View(model);
        }

        // POST: LeTanDatPhong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DatPhong datphong)
        {
            if (datphong.NgayTraPhong.Date <= datphong.NgayNhanPhong.Date)
            {
                ModelState.AddModelError(nameof(datphong.NgayTraPhong), "Ngày trả phòng phải sau ngày nhận phòng.");
            }

            if (datphong.NgayNhanPhong.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(datphong.NgayNhanPhong), "Ngày nhận phòng không được nằm trong quá khứ.");
            }

            var phong = await _leTanRepository.GetPhongByIdAsync(datphong.MaPhong);
            if (phong == null || phong.TrangThai != "Sẵn sàng")
            {
                ModelState.AddModelError(nameof(datphong.MaPhong), "Phòng đã chọn không tồn tại hoặc không ở trạng thái sẵn sàng.");
            }
            else if (datphong.SoNguoi > phong.SucChua)
            {
                ModelState.AddModelError(nameof(datphong.SoNguoi), $"Số người ({datphong.SoNguoi}) vượt quá sức chứa tối đa ({phong.SucChua}) của phòng {phong.SoPhong}.");
            }

            var coTrung = await _leTanRepository.IsPhongTrungLichAsync(datphong.MaPhong, datphong.NgayNhanPhong, datphong.NgayTraPhong);
            if (coTrung)
            {
                ModelState.AddModelError(nameof(datphong.MaPhong), "Phòng này đã có người đặt trong khoảng ngày đã chọn.");
            }

            if (ModelState.IsValid)
            {
                var soDem = (int)(datphong.NgayTraPhong.Date - datphong.NgayNhanPhong.Date).TotalDays;
                if (soDem <= 0) soDem = 1;

                datphong.NgayDat = DateTime.Now;
                datphong.TongTienDuKien = (phong?.GiaMotDem ?? 0) * soDem;
                datphong.TrangThai = "Chờ xác nhận";

                await _leTanRepository.CreateDatPhongAsync(datphong);

                TempData["ThongBaoThanhCong"] = $"Tạo đặt phòng mới #{datphong.MaDatPhong} thành công.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            var khachHangs = await _leTanRepository.GetDanhSachKhachHangHoatDongAsync();
            var phongs = await _leTanRepository.GetDanhSachPhongSanSangAsync();
            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(phongs.Select(p => new {
                p.MaPhong,
                TenHienThi = $"{p.SoPhong} - {p.LoaiPhong?.TenLoaiPhong} ({p.GiaMotDem:N0} đ/đêm)"
            }), "MaPhong", "TenHienThi", datphong.MaPhong);

            return View(datphong);
        }

        // GET: LeTanDatPhong/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var datphong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datphong == null) return NotFound();

            if (datphong.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = "Nghiệp vụ quy định: Không được phép chỉnh sửa đặt phòng đã 'Đã trả phòng'.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            var khachHangs = await _leTanRepository.GetDanhSachKhachHangHoatDongAsync();
            var phongs = await _leTanRepository.GetDanhSachPhongSanSangAsync();
            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(phongs, "MaPhong", "SoPhong", datphong.MaPhong);
            return View(datphong);
        }

        // POST: LeTanDatPhong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DatPhong datphong)
        {
            if (id != datphong.MaDatPhong) return NotFound();

            var existing = await _leTanRepository.GetDatPhongByIdAsync(id, false);
            if (existing == null) return NotFound();

            if (existing.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = "Nghiệp vụ quy định: Không được phép chỉnh sửa đặt phòng đã 'Đã trả phòng'.";
                return RedirectToAction(nameof(Details), new { id });
            }

            if (ModelState.IsValid)
            {
                var success = await _leTanRepository.UpdateDatPhongAsync(datphong);
                if (success)
                {
                    TempData["ThongBaoThanhCong"] = $"Cập nhật thành công thông tin đặt phòng #{id}.";
                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            var khachHangs = await _leTanRepository.GetDanhSachKhachHangHoatDongAsync();
            var phongs = await _leTanRepository.GetDanhSachPhongSanSangAsync();
            ViewBag.MaKhachHang = new SelectList(khachHangs, "MaKhachHang", "HoTen", datphong.MaKhachHang);
            ViewBag.MaPhong = new SelectList(phongs, "MaPhong", "SoPhong", datphong.MaPhong);
            return View(datphong);
        }

        // GET: LeTanDatPhong/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var datphong = await _leTanRepository.GetDatPhongByIdAsync(id.Value);
            if (datphong == null) return NotFound();

            if (datphong.TrangThai == "Đã nhận phòng" || datphong.TrangThai == "Đã trả phòng")
            {
                TempData["ThongBaoLoi"] = $"Không được xóa đặt phòng ở trạng thái '{datphong.TrangThai}'.";
                return RedirectToAction(nameof(Details), new { id = datphong.MaDatPhong });
            }

            return View(datphong);
        }

        // POST: LeTanDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, message) = await _leTanRepository.DeleteDatPhongAsync(id);
            if (!success)
            {
                TempData["ThongBaoLoi"] = message;
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData["ThongBaoThanhCong"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}