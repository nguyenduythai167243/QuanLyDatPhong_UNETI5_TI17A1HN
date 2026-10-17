using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
public class DatPhongController : Controller
{
    private readonly ApplicationDbContext _context;

    public DatPhongController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DATPHONGS
    public async Task<IActionResult> Index()
    {
        return View(await _context.DatPhongs.ToListAsync());
    }

    // GET: DATPHONGS/Details/5
    public async Task<IActionResult> Details(int? madatphong)
    {
        if (madatphong == null)
        {
            return NotFound();
        }

        var datphong = await _context.DatPhongs
            .FirstOrDefaultAsync(m => m.MaDatPhong == madatphong);

        if (datphong == null)
        {
            return NotFound();
        }

        return View(datphong);
    }

    // GET: DATPHONGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DATPHONGS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("MaDatPhong,MaKhachHang,MaPhong,NgayNhanPhong,NgayTraPhong,SoNguoi,YeuCauDacBiet,NgayDat,TongTienDuKien,NgayNhanThucTe,NgayTraThucTe,TrangThai,NgayXuLy,GhiChuLeTan,KhachHang,Phong,DichVuPhatSinhs,HoaDon")]
        DatPhong datphong)
    {
        if (ModelState.IsValid)
        {
            _context.Add(datphong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(datphong);
    }

    // GET: DATPHONGS/Edit/5
    public async Task<IActionResult> Edit(int? madatphong)
    {
        if (madatphong == null)
        {
            return NotFound();
        }

        var datphong = await _context.DatPhongs.FindAsync(madatphong);

        if (datphong == null)
        {
            return NotFound();
        }

        return View(datphong);
    }

    // POST: DATPHONGS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? madatphong,
        [Bind("MaDatPhong,MaKhachHang,MaPhong,NgayNhanPhong,NgayTraPhong,SoNguoi,YeuCauDacBiet,NgayDat,TongTienDuKien,NgayNhanThucTe,NgayTraThucTe,TrangThai,NgayXuLy,GhiChuLeTan,KhachHang,Phong,DichVuPhatSinhs,HoaDon")]
        DatPhong datphong)
    {
        if (madatphong != datphong.MaDatPhong)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(datphong);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatPhongExists(datphong.MaDatPhong))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(datphong);
    }

    // GET: DATPHONGS/Delete/5
    public async Task<IActionResult> Delete(int? madatphong)
    {
        if (madatphong == null)
        {
            return NotFound();
        }

        var datphong = await _context.DatPhongs
            .FirstOrDefaultAsync(m => m.MaDatPhong == madatphong);

        if (datphong == null)
        {
            return NotFound();
        }

        return View(datphong);
    }

    // POST: DATPHONGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? madatphong)
    {
        var datphong = await _context.DatPhongs.FindAsync(madatphong);

        if (datphong != null)
        {
            _context.DatPhongs.Remove(datphong);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DatPhongExists(int? madatphong)
    {
        return _context.DatPhongs.Any(e => e.MaDatPhong == madatphong);
    }
}