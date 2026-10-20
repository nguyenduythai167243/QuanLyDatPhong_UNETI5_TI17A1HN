
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;

public class KhachHangController : Controller
{
    private readonly ApplicationDbContext _context;

    public KhachHangController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: KHACHHANGS
    public async Task<IActionResult> Index()    
    {
        int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");

        if (maTaiKhoan == null)
            return RedirectToAction("Login", "TaiKhoan");

        var khachHang = await _context.KhachHangs
            .FirstOrDefaultAsync(kh => kh.MaTaiKhoan == maTaiKhoan);

        if (khachHang == null)
            return NotFound("Không tìm thấy thông tin khách hàng.");

        return View(khachHang);
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
        int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");

        if (maTaiKhoan == null)
            return RedirectToAction("Login", "TaiKhoan");

        var khachHang = await _context.KhachHangs
            .FirstOrDefaultAsync(kh => kh.MaTaiKhoan == maTaiKhoan);

        if (khachHang == null)
            return NotFound("Không tìm thấy thông tin khách hàng.");

        return View(khachHang);
    }

    // POST: KHACHHANGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? makhachhang, [Bind("MaKhachHang,HoTen,NgaySinh,GioiTinh,SoDienThoai,Email,DiaChi,SoGiayToTuyThan,QuocTich")] KhachHang khachhang)
    {
        int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");

            if (maTaiKhoan == null)
                return RedirectToAction("Login", "TaiKhoan");

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.MaTaiKhoan == maTaiKhoan.Value);

            if (khachHang == null)
                return NotFound("Không tìm thấy thông tin khách hàng.");

        // Chỉ cập nhật các trường được phép sửa
            // Không kiểm tra TrangThai vì khách hàng không được sửa trường này
            ModelState.Remove("TrangThai");

        if (!ModelState.IsValid)
            return View("Edit", khachhang);

        khachHang.HoTen = khachhang.HoTen;
            khachHang.NgaySinh = khachhang.NgaySinh;
            khachHang.GioiTinh = khachhang.GioiTinh;
            khachHang.SoDienThoai = khachhang.SoDienThoai;
            khachHang.Email = khachhang.Email;
            khachHang.DiaChi = khachhang.DiaChi;
            khachHang.SoGiayToTuyThan = khachhang.SoGiayToTuyThan;
            khachHang.QuocTich = khachhang.QuocTich;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cập nhật thông tin cá nhân thành công.";

        return RedirectToAction("Index", "KhachHang");
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
}
