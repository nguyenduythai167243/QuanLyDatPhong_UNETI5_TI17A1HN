
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

public class LoaiPhongsController : Controller
{
    private readonly ApplicationDbContext _context;

    public LoaiPhongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: LOAIPHONGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LoaiPhong.ToListAsync());
    }

    // GET: LOAIPHONGS/Details/5
    public async Task<IActionResult> Details(int? maloaiphong)
    {
        if (maloaiphong == null)
        {
            return NotFound();
        }

        var loaiphong = await _context.LoaiPhong
            .FirstOrDefaultAsync(m => m.MaLoaiPhong == maloaiphong);
        if (loaiphong == null)
        {
            return NotFound();
        }

        return View(loaiphong);
    }

    // GET: LOAIPHONGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LOAIPHONGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaLoaiPhong,TenLoaiPhong,MoTa,SucChuaToiDa,GiaThamKhao,TrangThai,Phongs")] LoaiPhong loaiphong)
    {
        if (ModelState.IsValid)
        {
            _context.Add(loaiphong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(loaiphong);
    }

    // GET: LOAIPHONGS/Edit/5
    public async Task<IActionResult> Edit(int? maloaiphong)
    {
        if (maloaiphong == null)
        {
            return NotFound();
        }

        var loaiphong = await _context.LoaiPhong.FindAsync(maloaiphong);
        if (loaiphong == null)
        {
            return NotFound();
        }
        return View(loaiphong);
    }

    // POST: LOAIPHONGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? maloaiphong, [Bind("MaLoaiPhong,TenLoaiPhong,MoTa,SucChuaToiDa,GiaThamKhao,TrangThai,Phongs")] LoaiPhong loaiphong)
    {
        if (maloaiphong != loaiphong.MaLoaiPhong)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(loaiphong);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiPhongExists(loaiphong.MaLoaiPhong))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(loaiphong);
    }

    // GET: LOAIPHONGS/Delete/5
    public async Task<IActionResult> Delete(int? maloaiphong)
    {
        if (maloaiphong == null)
        {
            return NotFound();
        }

        var loaiphong = await _context.LoaiPhong
            .FirstOrDefaultAsync(m => m.MaLoaiPhong == maloaiphong);
        if (loaiphong == null)
        {
            return NotFound();
        }

        return View(loaiphong);
    }

    // POST: LOAIPHONGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? maloaiphong)
    {
        var loaiphong = await _context.LoaiPhong.FindAsync(maloaiphong);
        if (loaiphong != null)
        {
            _context.LoaiPhong.Remove(loaiphong);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LoaiPhongExists(int? maloaiphong)
    {
        return _context.LoaiPhong.Any(e => e.MaLoaiPhong == maloaiphong);
    }
}
