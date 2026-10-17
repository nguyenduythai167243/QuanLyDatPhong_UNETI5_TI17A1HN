using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

public class DichVuPhatSinhController : Controller
{
    private readonly QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext _context;

    public DichVuPhatSinhController(
        QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DichVuPhatSinhs
    public async Task<IActionResult> Index()
    {
        var danhSach = await _context.DichVuPhatSinhs
            .Include(d => d.DatPhong)
            .OrderByDescending(d => d.NgaySuDung)
            .ToListAsync();

        return View(danhSach);
    }

    // GET: DichVuPhatSinhs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var dichVu = await _context.DichVuPhatSinhs
            .Include(d => d.DatPhong)
            .FirstOrDefaultAsync(d => d.MaDichVuPhatSinh == id);

        if (dichVu == null)
            return NotFound();

        return View(dichVu);
    }

    // GET: DichVuPhatSinhs/Create
    public IActionResult Create()
    {
        return View(new DichVuPhatSinh
        {
            SoLuong = 1,
            DonGia = 0,
            NgaySuDung = DateTime.Today,
            TrangThai = TrangThaiDV.DaGhiNhan
        });
    }

    // POST: DichVuPhatSinhs/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("MaDatPhong,TenDichVu,SoLuong,DonGia,NgaySuDung,GhiChu")]
        DichVuPhatSinh dichVu)
    {
        var datPhong = await _context.DatPhongs
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatPhong == dichVu.MaDatPhong);

        if (datPhong == null)
        {
            ModelState.AddModelError(
                nameof(dichVu.MaDatPhong),
                "Đặt phòng không tồn tại.");
        }
        else
        {
            if (!LaTrangThaiDaNhanPhong(datPhong.TrangThai))
            {
                ModelState.AddModelError(
                    nameof(dichVu.MaDatPhong),
                    "Chỉ đặt phòng đã nhận phòng mới được ghi nhận dịch vụ.");
            }

            if (datPhong.NgayTraThucTe.HasValue)
            {
                ModelState.AddModelError(
                    nameof(dichVu.MaDatPhong),
                    "Đặt phòng đã trả phòng, không thể thêm dịch vụ.");
            }

            if (datPhong.HoaDon != null)
            {
                ModelState.AddModelError(
                    nameof(dichVu.MaDatPhong),
                    "Đặt phòng đã có hóa đơn, không thể thêm dịch vụ.");
            }

            if (!datPhong.NgayNhanThucTe.HasValue)
            {
                ModelState.AddModelError(
                    nameof(dichVu.NgaySuDung),
                    "Chưa có ngày nhận phòng thực tế.");
            }
            else if (
                dichVu.NgaySuDung.Date < datPhong.NgayNhanThucTe.Value.Date
                || dichVu.NgaySuDung.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(dichVu.NgaySuDung),
                    "Ngày sử dụng phải từ ngày nhận phòng thực tế đến hôm nay.");
            }
        }

        if (ModelState.IsValid)
        {
            dichVu.TrangThai = TrangThaiDV.DaGhiNhan;

            _context.DichVuPhatSinhs.Add(dichVu);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(dichVu);
    }

    // GET: DichVuPhatSinhs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var dichVu = await _context.DichVuPhatSinhs.FindAsync(id);

        if (dichVu == null)
            return NotFound();

        if (dichVu.TrangThai != TrangThaiDV.DaGhiNhan)
            return BadRequest("Dịch vụ đã hủy không thể sửa.");

        return View(dichVu);
    }

    // POST: DichVuPhatSinhs/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("MaDichVuPhatSinh,MaDatPhong,TenDichVu,SoLuong,DonGia,NgaySuDung,GhiChu")]
        DichVuPhatSinh dichVu)
    {
        if (id != dichVu.MaDichVuPhatSinh)
            return NotFound();

        var dichVuCu = await _context.DichVuPhatSinhs.FindAsync(id);

        if (dichVuCu == null)
            return NotFound();

        if (dichVuCu.TrangThai != TrangThaiDV.DaGhiNhan)
            return BadRequest("Dịch vụ đã hủy không thể sửa.");

        var datPhong = await _context.DatPhongs
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatPhong == dichVu.MaDatPhong);

        if (datPhong == null)
        {
            ModelState.AddModelError(
                nameof(dichVu.MaDatPhong),
                "Đặt phòng không tồn tại.");
        }
        else
        {
            if (!LaTrangThaiDaNhanPhong(datPhong.TrangThai)
                || datPhong.NgayTraThucTe.HasValue
                || datPhong.HoaDon != null)
            {
                ModelState.AddModelError(
                    nameof(dichVu.MaDatPhong),
                    "Đặt phòng không còn đủ điều kiện sửa dịch vụ.");
            }

            if (!datPhong.NgayNhanThucTe.HasValue)
            {
                ModelState.AddModelError(
                    nameof(dichVu.NgaySuDung),
                    "Chưa có ngày nhận phòng thực tế.");
            }
            else if (
                dichVu.NgaySuDung.Date < datPhong.NgayNhanThucTe.Value.Date
                || dichVu.NgaySuDung.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(dichVu.NgaySuDung),
                    "Ngày sử dụng không hợp lệ.");
            }
        }

        if (ModelState.IsValid)
        {
            dichVuCu.MaDatPhong = dichVu.MaDatPhong;
            dichVuCu.TenDichVu = dichVu.TenDichVu;
            dichVuCu.SoLuong = dichVu.SoLuong;
            dichVuCu.DonGia = dichVu.DonGia;
            dichVuCu.NgaySuDung = dichVu.NgaySuDung;
            dichVuCu.GhiChu = dichVu.GhiChu;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(dichVu);
    }

    // GET: DichVuPhatSinhs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var dichVu = await _context.DichVuPhatSinhs
            .Include(d => d.DatPhong)
            .FirstOrDefaultAsync(d => d.MaDichVuPhatSinh == id);

        if (dichVu == null)
            return NotFound();

        return View(dichVu);
    }

    // POST: DichVuPhatSinhs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
            return NotFound();

        var dichVu = await _context.DichVuPhatSinhs.FindAsync(id);

        if (dichVu == null)
            return NotFound();

        if (dichVu.TrangThai == TrangThaiDV.DaHuy)
            return RedirectToAction(nameof(Index));

        var datPhong = await _context.DatPhongs
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatPhong == dichVu.MaDatPhong);

        if (datPhong == null
            || !LaTrangThaiDaNhanPhong(datPhong.TrangThai)
            || datPhong.NgayTraThucTe.HasValue
            || datPhong.HoaDon != null)
        {
            return BadRequest("Không thể hủy dịch vụ của đặt phòng này.");
        }

        // Hủy mềm: giữ dữ liệu để phục vụ thống kê.
        dichVu.TrangThai = TrangThaiDV.DaHuy;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Kiểm tra trạng thái đặt phòng
    private static bool LaTrangThaiDaNhanPhong(string? trangThai)
    {
        return string.Equals(
            trangThai?.Trim(),
            "Đã nhận phòng",
            StringComparison.OrdinalIgnoreCase);
    }
}