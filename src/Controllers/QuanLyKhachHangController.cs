
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Data;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

[SessionAuthorize("Admin", "LeTan", "Lễ tân")]
public class QuanLyKhachHangController : Controller
{
    private readonly IQuanLyKhachRepository _khachHangRepository;

    public QuanLyKhachHangController(IQuanLyKhachRepository khachHangRepository)
    {
        _khachHangRepository = khachHangRepository;
    }
    public async Task<IActionResult> Search(string? searchString)
    {
        ViewData["SearchString"] = searchString;

        return View(await _khachHangRepository.SearchAsync(searchString));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id)
    {
        var khachHang = await _khachHangRepository.GetByIdAsync(id);

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

        _khachHangRepository.Update(khachHang);
        await _khachHangRepository.SaveAsync();

        TempData["Success"] = "Cập nhật trạng thái khách hàng thành công.";

        return RedirectToAction(nameof(Index));
    }
    // GET: KHACHHANGS
    public async Task<IActionResult> Index(string? searchString)
    {
        ViewData["SearchString"] = searchString;

        return View(await _khachHangRepository.SearchAsync(searchString));
    }

    // GET: KHACHHANGS/Details/5
    public async Task<IActionResult> Details(int? makhachhang)
    {
        if (makhachhang == null)
        {
            return NotFound();
        }

        var khachhang = await _khachHangRepository.GetByIdAsync(makhachhang.Value);
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
            await _khachHangRepository.AddAsync(khachhang);
            await _khachHangRepository.SaveAsync();
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

        var khachhang = await _khachHangRepository.GetByIdAsync(makhachhang.Value);
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
            if (!await _khachHangRepository.ExistsAsync(khachhang.MaKhachHang))
            {
                return NotFound();
            }

            _khachHangRepository.Update(khachhang);
            await _khachHangRepository.SaveAsync();

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

        var khachhang = await _khachHangRepository.GetByIdAsync(makhachhang.Value);
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
        if (makhachhang == null)
        {
            return NotFound();
        }

        var khachhang = await _khachHangRepository.GetByIdAsync(makhachhang.Value);

        if (khachhang == null)
        {
            return NotFound();
        }

        _khachHangRepository.Delete(khachhang);
        await _khachHangRepository.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

}
