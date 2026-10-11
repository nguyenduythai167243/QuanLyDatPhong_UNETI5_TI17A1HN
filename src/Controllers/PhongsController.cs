using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI.ViewModels;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;

public class PhongsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PhongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PHONGS
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
        int page = 1
    )
    {
        var query = _context.Phong.Include(p => p.LoaiPhong).AsQueryable();

        if (!string.IsNullOrEmpty(tuKhoa))
        {
            var kw = tuKhoa.Trim().ToLower();
            query = query.Where(p =>
                p.SoPhong.ToString().ToLower().Contains(tuKhoa.ToLower())
                || p.LoaiPhong!.TenLoaiPhong.ToLower().Contains(tuKhoa.ToLower())
            );
        }
        if (maLoaiPhong.HasValue)
        {
            query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
        }
        if (tang.HasValue)
        {
            query = query.Where(p => p.Tang == tang.Value);
        }
        if (sucChuaToiThieu.HasValue)
        {
            query = query.Where(p => p.SucChua >= sucChuaToiThieu.Value);
        }
        if (giaTu.HasValue)
        {
            query = query.Where(p => p.GiaMotDem >= giaTu.Value);
        }
        if (giaDen.HasValue)
        {
            query = query.Where(p => p.GiaMotDem <= giaDen.Value);
        }
        if (!string.IsNullOrEmpty(trangThai))
        {
            query = query.Where(p => p.TrangThai == trangThai);
        }
        if (
            ngayNhanPhong.HasValue
            && ngayTraPhong.HasValue
            && ngayNhanPhong.Value <= ngayTraPhong.Value
        )
        {
            query = query.Where(p =>
                !p.DatPhongs!.Any(dp =>
                    (
                        dp.NgayNhanPhong <= ngayTraPhong.Value
                        && dp.NgayTraPhong >= ngayNhanPhong.Value
                    )
                )
            );
        }

        query = sortOrder switch
        {
            "gia_desc" => query.OrderByDescending(p => p.GiaMotDem),
            "gia_asc" => query.OrderBy(p => p.GiaMotDem),
            "tang_desc" => query.OrderByDescending(p => p.Tang),
            "tang_asc" => query.OrderBy(p => p.Tang),
            _ => query.OrderBy(p => p.MaPhong),
        };

        const int pageSize = 10;
        var pagedResult = await PaginatedList<Phong>.CreateAsync(
            query.AsNoTracking(),
            page,
            pageSize
        );

        return View(await _context.Phong.ToListAsync());
    }

    // GET: PHONGS/Details/5
    public async Task<IActionResult> Details(int? maphong)
    {
        if (maphong == null)
        {
            return NotFound();
        }

        var phong = await _context.Phong.FirstOrDefaultAsync(m => m.MaPhong == maphong);
        if (phong == null)
        {
            return NotFound();
        }

        return View(phong);
    }

    // GET: PHONGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PHONGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "MaPhong,SoPhong,Tang,SucChua,GiaMotDem,DienTich,TienNghi,MoTa,TrangThai,LoaiPhong,DatPhongs"
        )]
            Phong phong
    )
    {
        if (ModelState.IsValid)
        {
            _context.Add(phong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(phong);
    }

    // GET: PHONGS/Edit/5
    public async Task<IActionResult> Edit(int? maphong)
    {
        if (maphong == null)
        {
            return NotFound();
        }

        var phong = await _context.Phong.FindAsync(maphong);
        if (phong == null)
        {
            return NotFound();
        }
        return View(phong);
    }

    // POST: PHONGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? maphong,
        [Bind(
            "MaPhong,SoPhong,Tang,SucChua,GiaMotDem,DienTich,TienNghi,MoTa,TrangThai,LoaiPhong,DatPhongs"
        )]
            Phong phong
    )
    {
        if (maphong != phong.MaPhong)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(phong);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongExists(phong.MaPhong))
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
        return View(phong);
    }

    // GET: PHONGS/Delete/5
    public async Task<IActionResult> Delete(int? maphong)
    {
        if (maphong == null)
        {
            return NotFound();
        }

        var phong = await _context.Phong.FirstOrDefaultAsync(m => m.MaPhong == maphong);
        if (phong == null)
        {
            return NotFound();
        }

        return View(phong);
    }

    // POST: PHONGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? maphong)
    {
        var phong = await _context.Phong.FindAsync(maphong);
        if (phong != null)
        {
            _context.Phong.Remove(phong);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PhongExists(int? maphong)
    {
        return _context.Phong.Any(e => e.MaPhong == maphong);
    }
}
