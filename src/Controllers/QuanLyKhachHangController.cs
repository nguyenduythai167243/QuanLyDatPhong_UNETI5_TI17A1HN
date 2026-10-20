
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;

public class QuanLyKhachHangController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuanLyKhachHangController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Search(string? searchString)
    {
        string? vaiTro = HttpContext.Session.GetString("VaiTro");
        var query = _context.KhachHangs.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(kh => kh.SoGiayToTuyThan.Contains(searchString.Trim()));
        }
        ViewData["SearchString"] = searchString;
        return View(await query.ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id)
    {
        string? vaiTro = HttpContext.Session.GetString("VaiTro");
        var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.MaKhachHang == id);
        if (khachHang == null)
        {
            return NotFound();
        }
        if (khachHang.TrangThai == "Hoạt động")
        {
            khachHang.TrangThai = "Ngừng hoạt động";
        }
        else
        {
            khachHang.TrangThai = "Hoạt động";
        }
        await _context.SaveChangesAsync();
        TempData["Success"] = "Cập nhật trạng thái khách hàng thành công.";
        return RedirectToAction(nameof(Index));
    }
    // GET: KHACHHANGS
    public async Task<IActionResult> Index(string? searchString)
    {
        var query = _context.KhachHangs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            searchString = searchString.Trim();

            query = query.Where(kh =>
                kh.SoGiayToTuyThan.Contains(searchString));
        }

        ViewData["SearchString"] = searchString;

        return View(await query.ToListAsync());
    }

    // GET: KHACHHANGS/Details/5
    public async Task<IActionResult> Details(int? makhachhang)
    {
        if (makhachhang == null)
        {
            return NotFound();
        }

        var khachhang = await _context.KhachHangs
            .FirstOrDefaultAsync(m => m.MaKhachHang == makhachhang);
        if (khachhang == null)
        {
            return NotFound();
        }

        return View(khachhang);
    }

    // GET: KHACHHANGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: KHACHHANGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaKhachHang,MaTaiKhoan,HoTen,NgaySinh,GioiTinh,SoDienThoai,Email,DiaChi,SoGiayToTuyThan,QuocTich,TrangThai,TaiKhoan,DatPhongs")] KhachHang khachhang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(khachhang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(khachhang);
    }

    // GET: KHACHHANGS/Edit/5
    public async Task<IActionResult> Edit(int? makhachhang)
    {
        if (makhachhang == null)
        {
            return NotFound();
        }

        var khachhang = await _context.KhachHangs.FindAsync(makhachhang);
        if (khachhang == null)
        {
            return NotFound();
        }
        return View(khachhang);
    }

    // POST: KHACHHANGS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? makhachhang, [Bind("MaKhachHang,MaTaiKhoan,HoTen,NgaySinh,GioiTinh,SoDienThoai,Email,DiaChi,SoGiayToTuyThan,QuocTich,TrangThai,TaiKhoan,DatPhongs")] KhachHang khachhang)
    {
        if (makhachhang != khachhang.MaKhachHang)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(khachhang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(khachhang.MaKhachHang))
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
        return View(khachhang);
    }

    // GET: KHACHHANGS/Delete/5
    public async Task<IActionResult> Delete(int? makhachhang)
    {
        if (makhachhang == null)
        {
            return NotFound();
        }

        var khachhang = await _context.KhachHangs
            .FirstOrDefaultAsync(m => m.MaKhachHang == makhachhang);
        if (khachhang == null)
        {
            return NotFound();
        }

        return View(khachhang);
    }

    // POST: KHACHHANGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? makhachhang)
    {
        var khachhang = await _context.KhachHangs.FindAsync(makhachhang);
        if (khachhang != null)
        {
            _context.KhachHangs.Remove(khachhang);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool KhachHangExists(int? makhachhang)
    {
        return _context.KhachHangs.Any(e => e.MaKhachHang == makhachhang);
    }

