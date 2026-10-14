
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    [SessionAuthorize("Admin")]
    public class LoaiPhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiPhongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách loại phòng
        public async Task<IActionResult> Index()
        {
            var danhSachLoaiPhong = await _context.LoaiPhongs
                .AsNoTracking()
                .ToListAsync();

            return View(danhSachLoaiPhong);
        }

        // Hiển thị form thêm loại phòng
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý dữ liệu khi Admin gửi form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiPhong loaiPhong)
        {
            if (!ModelState.IsValid)
            {
                return View(loaiPhong);
            }

            _context.LoaiPhongs.Add(loaiPhong);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Hiển thị form sửa loại phòng
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .FindAsync(id);

            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong);
        }

        // Xử lý dữ liệu sau khi sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, LoaiPhong loaiPhong)
        {
            if (id != loaiPhong.MaLoaiPhong)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(loaiPhong);
            }

            try
            {
                _context.LoaiPhongs.Update(loaiPhong);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.LoaiPhongs
                    .AnyAsync(e => e.MaLoaiPhong == id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        // Hiển thị chi tiết loại phòng
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .AsNoTracking()
                .FirstOrDefaultAsync(lp => lp.MaLoaiPhong == id);

            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong);
        }


    }
}
