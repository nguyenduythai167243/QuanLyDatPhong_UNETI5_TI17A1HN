using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    public class PhongController : Controller
    {
        private readonly IPhongRepository _phongRepository;

        public PhongController(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }

        // GET: Phong (Dành cho Lễ tân / Quản trị viên)
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Index(
            string? tuKhoa,
            int? maLoaiPhong,
            int? tang,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            string? trangThai,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int page = 1)
        {
            const int pageSize = 8;
            var paginatedPhongs = await _phongRepository.GetFilteredPhongsAsync(
                tuKhoa,
                maLoaiPhong,
                tang,
                sucChuaToiThieu,
                giaTu,
                giaDen,
                trangThai,
                ngayNhanPhong,
                ngayTraPhong,
                sortOrder,
                page,
                pageSize);

            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);
            var danhSachTang = await _phongRepository.GetDistinctFloorsAsync();

            // Chuẩn bị ViewModel
            var vm = new PhongFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoaiPhong = maLoaiPhong,
                Tang = tang,
                SucChuaToiThieu = sucChuaToiThieu,
                GiaTu = giaTu,
                GiaDen = giaDen,
                TrangThai = trangThai,
                NgayNhanPhong = ngayNhanPhong,
                NgayTraPhong = ngayTraPhong,
                SortOrder = sortOrder ?? "so_phong_asc",
                PageIndex = page,
                PageSize = pageSize,
                DanhSachPhong = paginatedPhongs,
                LoaiPhongList = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                DanhSachTang = danhSachTang
            };

            return View(vm);
        }

        // GET: Phong/DanhSachPhong (Dành cho Khách hàng xem và tìm phòng đặt)
        public async Task<IActionResult> DanhSachPhong(
            string? tuKhoa,
            int? maLoaiPhong,
            int? sucChuaToiThieu,
            decimal? giaTu,
            decimal? giaDen,
            DateTime? ngayNhanPhong,
            DateTime? ngayTraPhong,
            string? sortOrder,
            int page = 1)
        {
            const int pageSize = 6;
            var (paginatedPhongs, phongTrongMap) = await _phongRepository.GetCustomerAvailablePhongsAsync(
                tuKhoa,
                maLoaiPhong,
                sucChuaToiThieu,
                giaTu,
                giaDen,
                ngayNhanPhong,
                ngayTraPhong,
                sortOrder,
                page,
                pageSize);

            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);

            var vm = new PhongFilterViewModel
            {
                TuKhoa = tuKhoa,
                MaLoaiPhong = maLoaiPhong,
                SucChuaToiThieu = sucChuaToiThieu,
                GiaTu = giaTu,
                GiaDen = giaDen,
                NgayNhanPhong = ngayNhanPhong ?? DateTime.Today,
                NgayTraPhong = ngayTraPhong ?? DateTime.Today.AddDays(1),
                SortOrder = sortOrder ?? "gia_asc",
                PageIndex = page,
                PageSize = pageSize,
                DanhSachPhong = paginatedPhongs,
                LoaiPhongList = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                PhongTrongMap = phongTrongMap
            };

            return View(vm);
        }

        // GET: Phong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _phongRepository.GetByIdWithDetailsAsync(id.Value);
            if (phong == null) return NotFound();

            return View(phong);
        }

        // GET: Phong/Create (Chỉ Lễ tân / Admin)
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Create()
        {
            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);
            ViewBag.MaLoaiPhong = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong");
            return View();
        }

        // POST: Phong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Create(Phong phong)
        {
            // Kiểm tra trùng số phòng
            if (await _phongRepository.IsSoPhongExistsAsync(phong.SoPhong))
            {
                ModelState.AddModelError("SoPhong", "Số phòng này đã tồn tại trong hệ thống.");
            }

            // Lấy thông tin loại phòng để kiểm tra sức chứa
            var loaiPhong = await _phongRepository.GetLoaiPhongByIdAsync(phong.MaLoaiPhong);
            if (loaiPhong != null)
            {
                if (phong.SucChua > loaiPhong.SucChuaToiDa)
                {
                    ModelState.AddModelError("SucChua", $"Sức chứa phòng ({phong.SucChua}) không được vượt quá sức chứa tối đa của loại phòng '{loaiPhong.TenLoaiPhong}' ({loaiPhong.SucChuaToiDa} người).");
                }
            }

            if (phong.Tang < 1)
            {
                ModelState.AddModelError("Tang", "Tầng phải từ 1 trở lên.");
            }

            if (phong.GiaMotDem <= 0)
            {
                ModelState.AddModelError("GiaMotDem", "Giá một đêm phải lớn hơn 0.");
            }

            if (ModelState.IsValid)
            {
                phong.SoPhong = phong.SoPhong.Trim();
                await _phongRepository.AddAsync(phong);
                await _phongRepository.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Tạo mới phòng {phong.SoPhong} thành công!";
                return RedirectToAction(nameof(Index));
            }

            var activeLoaiPhongs = await _phongRepository.GetLoaiPhongsAsync(onlyActive: true);
            ViewBag.MaLoaiPhong = new SelectList(activeLoaiPhongs, "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
            return View(phong);
        }

        // GET: Phong/Edit/5
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _phongRepository.GetByIdAsync(id.Value);
            if (phong == null) return NotFound();

            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync();
            ViewBag.MaLoaiPhong = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
            return View(phong);
        }

        // POST: Phong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Edit(int id, Phong phong)
        {
            if (id != phong.MaPhong) return NotFound();

            // Kiểm tra trùng số phòng với phòng khác
            if (await _phongRepository.IsSoPhongExistsAsync(phong.SoPhong, id))
            {
                ModelState.AddModelError("SoPhong", "Số phòng này đã tồn tại.");
            }

            var loaiPhong = await _phongRepository.GetLoaiPhongByIdAsync(phong.MaLoaiPhong);
            if (loaiPhong != null && phong.SucChua > loaiPhong.SucChuaToiDa)
            {
                ModelState.AddModelError("SucChua", $"Sức chứa phòng không được vượt quá sức chứa loại phòng ({loaiPhong.SucChuaToiDa} người).");
            }

            // Kiểm tra nghiệp vụ chuyển trạng thái:
            // "Không cho chuyển phòng sang Đang bảo trì hoặc Ngừng kinh doanh nếu phòng còn đặt phòng đang hiệu lực trong tương lai"
            if (phong.TrangThai == "Đang bảo trì" || phong.TrangThai == "Ngừng kinh doanh")
            {
                bool coDatPhongHieuLuc = await _phongRepository.HasActiveBookingsAsync(id);
                if (coDatPhongHieuLuc)
                {
                    ModelState.AddModelError("TrangThai", "Không thể chuyển sang Đang bảo trì hoặc Ngừng kinh doanh vì phòng đang có lịch đặt phòng còn hiệu lực!");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    phong.SoPhong = phong.SoPhong.Trim();
                    await _phongRepository.UpdateAsync(phong);
                    await _phongRepository.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Cập nhật thông tin phòng {phong.SoPhong} thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _phongRepository.ExistsAsync(phong.MaPhong))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var loaiPhongs = await _phongRepository.GetLoaiPhongsAsync();
            ViewBag.MaLoaiPhong = new SelectList(loaiPhongs, "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
            return View(phong);
        }

        // POST: Phong/CapNhatTrangThai
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThaiMoi)
        {
            var phong = await _phongRepository.GetByIdAsync(id);
            if (phong == null) return NotFound();

            if (trangThaiMoi == "Đang bảo trì" || trangThaiMoi == "Ngừng kinh doanh")
            {
                bool coDatPhongHieuLuc = await _phongRepository.HasActiveBookingsAsync(id);
                if (coDatPhongHieuLuc)
                {
                    TempData["ErrorMessage"] = $"Không thể đổi trạng thái phòng {phong.SoPhong} vì đang có đặt phòng hiệu lực trong tương lai!";
                    return RedirectToAction(nameof(Index));
                }
            }

            phong.TrangThai = trangThaiMoi;
            await _phongRepository.UpdateAsync(phong);
            await _phongRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phòng {phong.SoPhong} sang '{trangThaiMoi}'.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Phong/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var phong = await _phongRepository.GetByIdWithBookingsAsync(id);
            if (phong == null) return NotFound();

            // Kiểm tra lịch sử đặt phòng
            if (phong.DatPhongs != null && phong.DatPhongs.Any())
            {
                TempData["ErrorMessage"] = $"Không thể xóa phòng {phong.SoPhong} vì đã có {phong.DatPhongs.Count} lịch sử đặt phòng. Bạn có thể đổi trạng thái sang 'Ngừng kinh doanh'.";
                return RedirectToAction(nameof(Index));
            }

            await _phongRepository.DeleteAsync(phong);
            await _phongRepository.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa phòng {phong.SoPhong} thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
