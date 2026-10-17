using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
// using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.ViewModels;

namespace QuanLyDatPhong_UNETI.Controllers
{
    public class PhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhongController(ApplicationDbContext context)
        {
            _context = context;
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
            // Bắt đầu truy vấn dữ liệu bằng IQueryable
            var query = _context.Phongs.Include(p => p.LoaiPhong).AsQueryable();

            // 1. TÌM KIẾM (LINQ) theo Số phòng hoặc Tên loại phòng
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(p => p.SoPhong.ToLower().Contains(kw) || p.LoaiPhong!.TenLoaiPhong.ToLower().Contains(kw));
            }

            // 2. LỌC (LINQ)
            // Lọc theo Loại phòng
            if (maLoaiPhong.HasValue && maLoaiPhong > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            // Lọc theo Tầng
            if (tang.HasValue && tang > 0)
            {
                query = query.Where(p => p.Tang == tang.Value);
            }

            // Lọc theo Sức chứa tối thiểu
            if (sucChuaToiThieu.HasValue && sucChuaToiThieu > 0)
            {
                query = query.Where(p => p.SucChua >= sucChuaToiThieu.Value);
            }

            // Lọc theo Khoảng giá một đêm
            if (giaTu.HasValue)
            {
                query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            }
            if (giaDen.HasValue)
            {
                query = query.Where(p => p.GiaMotDem <= giaDen.Value);
            }

            // Lọc theo Trạng thái phòng
            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            // Lọc theo Khoảng ngày nhận phòng - ngày trả phòng (phòng trống)
            if (ngayNhanPhong.HasValue && ngayTraPhong.HasValue && ngayTraPhong > ngayNhanPhong)
            {
                var checkIn = ngayNhanPhong.Value.Date;
                var checkOut = ngayTraPhong.Value.Date;

                // Các đặt phòng đang hiệu lực và trùng khoảng thời gian:
                // Thuật toán: newCheckIn < existingCheckOut && newCheckOut > existingCheckIn
                var phongDatTrungIds = await _context.DatPhongs
                    .Where(dp => (dp.TrangThai == "Chờ xác nhận" || dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                                && dp.NgayNhanPhong.Date < checkOut
                                && dp.NgayTraPhong.Date > checkIn)
                    .Select(dp => dp.MaPhong)
                    .Distinct()
                    .ToListAsync();

                query = query.Where(p => !phongDatTrungIds.Contains(p.MaPhong));
            }

            // 3. SẮP XẾP (LINQ)
            query = sortOrder switch
            {
                "so_phong_desc" => query.OrderByDescending(p => p.SoPhong),
                "gia_asc" => query.OrderBy(p => p.GiaMotDem),
                "gia_desc" => query.OrderByDescending(p => p.GiaMotDem),
                "suc_chua_asc" => query.OrderBy(p => p.SucChua),
                "suc_chua_desc" => query.OrderByDescending(p => p.SucChua),
                _ => query.OrderBy(p => p.SoPhong) // Mặc định số phòng tăng dần
            };

            // 4. PHÂN TRANG (LINQ bằng Skip, Take)
            const int pageSize = 8;
            var paginatedPhongs = await PaginatedList<Phong>.CreateAsync(query, page, pageSize);

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
                LoaiPhongList = new SelectList(await _context.LoaiPhongs.Where(lp => lp.TrangThai).ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                DanhSachTang = await _context.Phongs.Select(p => p.Tang).Distinct().OrderBy(t => t).ToListAsync()
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
            var query = _context.Phongs.Include(p => p.LoaiPhong).AsQueryable();

            // Khách hàng chỉ xem phòng đang hoạt động (Sẵn sàng) trừ khi lọc trạng thái
            query = query.Where(p => p.TrangThai == "Sẵn sàng" && p.LoaiPhong!.TrangThai);

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                var kw = tuKhoa.Trim().ToLower();
                query = query.Where(p => p.SoPhong.ToLower().Contains(kw) || p.LoaiPhong!.TenLoaiPhong.ToLower().Contains(kw) || (p.TienNghi != null && p.TienNghi.ToLower().Contains(kw)));
            }

            // Lọc theo loại phòng
            if (maLoaiPhong.HasValue && maLoaiPhong > 0)
            {
                query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
            }

            // Lọc theo sức chứa
            if (sucChuaToiThieu.HasValue && sucChuaToiThieu > 0)
            {
                query = query.Where(p => p.SucChua >= sucChuaToiThieu.Value);
            }

            // Lọc theo khoảng giá
            if (giaTu.HasValue) query = query.Where(p => p.GiaMotDem >= giaTu.Value);
            if (giaDen.HasValue) query = query.Where(p => p.GiaMotDem <= giaDen.Value);

            // Kiểm tra trạng thái Còn trống/Đã có người đặt theo khoảng ngày
            var phongTrongMap = new Dictionary<int, bool>();

            if (ngayNhanPhong.HasValue && ngayTraPhong.HasValue && ngayTraPhong > ngayNhanPhong)
            {
                var checkIn = ngayNhanPhong.Value.Date;
                var checkOut = ngayTraPhong.Value.Date;

                // Lấy danh sách ID các phòng đã bị đặt trong khoảng thời gian này
                var phongDaDatIds = await _context.DatPhongs
                    .Where(dp => (dp.TrangThai == "Chờ xác nhận" || dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng")
                                && dp.NgayNhanPhong.Date < checkOut
                                && dp.NgayTraPhong.Date > checkIn)
                    .Select(dp => dp.MaPhong)
                    .Distinct()
                    .ToListAsync();

                // Lấy toàn bộ ID phòng thỏa mãn điều kiện lọc để đánh dấu trạng thái trống
                var tatCaPhongIds = await query.Select(p => p.MaPhong).ToListAsync();
                foreach (var id in tatCaPhongIds)
                {
                    phongTrongMap[id] = !phongDaDatIds.Contains(id);
                }
            }

            // Sắp xếp
            query = sortOrder switch
            {
                "so_phong_desc" => query.OrderByDescending(p => p.SoPhong),
                "gia_asc" => query.OrderBy(p => p.GiaMotDem),
                "gia_desc" => query.OrderByDescending(p => p.GiaMotDem),
                "suc_chua_asc" => query.OrderBy(p => p.SucChua),
                "suc_chua_desc" => query.OrderByDescending(p => p.SucChua),
                _ => query.OrderBy(p => p.GiaMotDem) // Khách hàng ưu tiên giá tăng dần
            };

            // Phân trang
            const int pageSize = 6;
            var paginatedPhongs = await PaginatedList<Phong>.CreateAsync(query, page, pageSize);

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
                LoaiPhongList = new SelectList(await _context.LoaiPhongs.Where(lp => lp.TrangThai).ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", maLoaiPhong),
                PhongTrongMap = phongTrongMap
            };

            return View(vm);
        }

        // GET: Phong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _context.Phongs
                .Include(p => p.LoaiPhong)
                .Include(p => p.DatPhongs.Where(dp => dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng"))
                .FirstOrDefaultAsync(m => m.MaPhong == id);

            if (phong == null) return NotFound();

            return View(phong);
        }

        // GET: Phong/Create (Chỉ Lễ tân / Admin)
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Create()
        {
            ViewBag.MaLoaiPhong = new SelectList(await _context.LoaiPhongs.Where(lp => lp.TrangThai).ToListAsync(), "MaLoaiPhong", "TenLoaiPhong");
            return View();
        }

        // POST: Phong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Create(Phong phong)
        {
            // Kiểm tra trùng số phòng
            if (await _context.Phongs.AnyAsync(p => p.SoPhong.ToLower() == phong.SoPhong.Trim().ToLower()))
            {
                ModelState.AddModelError("SoPhong", "Số phòng này đã tồn tại trong hệ thống.");
            }

            // Lấy thông tin loại phòng để kiểm tra sức chứa
            var loaiPhong = await _context.LoaiPhongs.FindAsync(phong.MaLoaiPhong);
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
                _context.Add(phong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Tạo mới phòng {phong.SoPhong} thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoaiPhong = new SelectList(await _context.LoaiPhongs.Where(lp => lp.TrangThai).ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
            return View(phong);
        }

        // GET: Phong/Edit/5
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null) return NotFound();

            ViewBag.MaLoaiPhong = new SelectList(await _context.LoaiPhongs.ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
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
            if (await _context.Phongs.AnyAsync(p => p.MaPhong != id && p.SoPhong.ToLower() == phong.SoPhong.Trim().ToLower()))
            {
                ModelState.AddModelError("SoPhong", "Số phòng này đã tồn tại.");
            }

            var loaiPhong = await _context.LoaiPhongs.FindAsync(phong.MaLoaiPhong);
            if (loaiPhong != null && phong.SucChua > loaiPhong.SucChuaToiDa)
            {
                ModelState.AddModelError("SucChua", $"Sức chứa phòng không được vượt quá sức chứa loại phòng ({loaiPhong.SucChuaToiDa} người).");
            }

            // Kiểm tra nghiệp vụ chuyển trạng thái:
            // "Không cho chuyển phòng sang Đang bảo trì hoặc Ngừng kinh doanh nếu phòng còn đặt phòng đang hiệu lực trong tương lai"
            if (phong.TrangThai == "Đang bảo trì" || phong.TrangThai == "Ngừng kinh doanh")
            {
                bool coDatPhongHieuLuc = await _context.DatPhongs.AnyAsync(dp =>
                    dp.MaPhong == id &&
                    (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                    dp.NgayTraPhong >= DateTime.Today);

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
                    _context.Update(phong);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Cập nhật thông tin phòng {phong.SoPhong} thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Phongs.AnyAsync(e => e.MaPhong == phong.MaPhong))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoaiPhong = new SelectList(await _context.LoaiPhongs.ToListAsync(), "MaLoaiPhong", "TenLoaiPhong", phong.MaLoaiPhong);
            return View(phong);
        }

        // POST: Phong/CapNhatTrangThai
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin", "LeTan")]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThaiMoi)
        {
            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null) return NotFound();

            if (trangThaiMoi == "Đang bảo trì" || trangThaiMoi == "Ngừng kinh doanh")
            {
                bool coDatPhongHieuLuc = await _context.DatPhongs.AnyAsync(dp =>
                    dp.MaPhong == id &&
                    (dp.TrangThai == "Đã xác nhận" || dp.TrangThai == "Đã nhận phòng") &&
                    dp.NgayTraPhong >= DateTime.Today);

                if (coDatPhongHieuLuc)
                {
                    TempData["ErrorMessage"] = $"Không thể đổi trạng thái phòng {phong.SoPhong} vì đang có đặt phòng hiệu lực trong tương lai!";
                    return RedirectToAction(nameof(Index));
                }
            }

            phong.TrangThai = trangThaiMoi;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phòng {phong.SoPhong} sang '{trangThaiMoi}'.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Phong/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[RoleAuthorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var phong = await _context.Phongs
                .Include(p => p.DatPhongs)
                .FirstOrDefaultAsync(p => p.MaPhong == id);

            if (phong == null) return NotFound();

            // Kiểm tra lịch sử đặt phòng
            if (phong.DatPhongs.Any())
            {
                TempData["ErrorMessage"] = $"Không thể xóa phòng {phong.SoPhong} vì đã có {phong.DatPhongs.Count} lịch sử đặt phòng. Bạn có thể đổi trạng thái sang 'Ngừng kinh doanh'.";
                return RedirectToAction(nameof(Index));
            }

            _context.Phongs.Remove(phong);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa phòng {phong.SoPhong} thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
