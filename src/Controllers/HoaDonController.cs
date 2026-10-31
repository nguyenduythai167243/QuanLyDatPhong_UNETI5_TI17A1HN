using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;
using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

public class HoaDonController : Controller
{
    private readonly IHoaDonRespository _respository;

    public HoaDonController(IHoaDonRespository respository)
    {
        _respository = respository;
    }

    // GET: HoaDons
    public async Task<IActionResult> Index()
    {
        var hoaDons = await _respository.GetAllAsync();

        return View(hoaDons);
    }

    // GET: HoaDons/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var hoaDon = await _respository.GetDetailsAsync(id.Value);

        if (hoaDon == null)
            return NotFound();

        return View(hoaDon);
    }

    // GET: HoaDons/Create
    // Chỉ hiển thị đặt phòng đã trả phòng và chưa có hóa đơn
    public async Task<IActionResult> Create()
    {
        await NapDanhSachDatPhong();
        return View();
    }

    // POST: HoaDons/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int maDatPhong,
        decimal giamGia,
        HinhThucThanhToan hinhThucThanhToan)
    {
        var datPhong = await _respository.GetDatPhongLapHoaDonAsync(maDatPhong);

        if (datPhong == null)
        {
            ModelState.AddModelError(
                "",
                "Không tìm thấy thông tin đặt phòng.");
        }
        else
        {
            if (datPhong.TrangThai != "Đã trả phòng")
            {
                ModelState.AddModelError(
                    "",
                    "Chỉ được lập hóa đơn khi khách đã trả phòng.");
            }

            if (datPhong.HoaDon != null ||
                await _respository.ExistsByDatPhongAsync(maDatPhong))
            {
                ModelState.AddModelError(
                    "",
                    "Đặt phòng này đã có hóa đơn.");
            }

            if (datPhong.NgayNhanThucTe == null ||
                datPhong.NgayTraThucTe == null ||
                datPhong.Phong == null)
            {
                ModelState.AddModelError(
                    "",
                    "Thiếu thông tin nhận phòng, trả phòng hoặc phòng.");
            }
            else if (datPhong.NgayTraThucTe <= datPhong.NgayNhanThucTe)
            {
                ModelState.AddModelError(
                    "",
                    "Thời gian trả phòng phải sau thời gian nhận phòng.");
            }
        }

        if (giamGia < 0)
        {
            ModelState.AddModelError(
                nameof(giamGia),
                "Giảm giá không được nhỏ hơn 0.");
        }

        if (!Enum.IsDefined(typeof(HinhThucThanhToan),
            hinhThucThanhToan))
        {
            ModelState.AddModelError(
                nameof(hinhThucThanhToan),
                "Hình thức thanh toán không hợp lệ.");
        }

        if (!ModelState.IsValid)
        {
            await NapDanhSachDatPhong();
            return View();
        }

        // Tính số đêm, tối thiểu 1 đêm
        var thoiGianLuuTru =
            (datPhong!.NgayTraThucTe!.Value -
             datPhong.NgayNhanThucTe!.Value).TotalDays;

        int soDem = Math.Max(1, (int)Math.Ceiling(thoiGianLuuTru));

        // Tiền phòng do hệ thống tính
        decimal tienPhong = soDem * datPhong.Phong!.GiaMotDem;

        // Chỉ cộng dịch vụ đã ghi nhận, bỏ dịch vụ đã hủy
        decimal tienDichVu = datPhong.DichVuPhatSinhs
            .Where(dv => dv.TrangThai == TrangThaiDV.DaGhiNhan)
            .Sum(dv => dv.SoLuong * dv.DonGia);

        decimal tongTruocGiam = tienPhong + tienDichVu;

        if (giamGia > tongTruocGiam)
        {
            ModelState.AddModelError(
                nameof(giamGia),
                "Giảm giá không được vượt quá tổng tiền trước giảm giá.");

            await NapDanhSachDatPhong();
            return View();
        }

        var hoaDon = new HoaDon
        {
            MaDatPhong = maDatPhong,
            SoDem = soDem,
            TienPhong = tienPhong,
            TienDichVu = tienDichVu,
            GiamGia = giamGia,
            TongTienThanhToan = tongTruocGiam - giamGia,
            HinhThucThanhToan = hinhThucThanhToan,
            NgayLap = DateTime.Now,
            TrangThai = TrangThaiTT.ChuaThanhToan
        };

        await _respository.AddAsync(hoaDon);

        return RedirectToAction(
            nameof(Details),
            new { id = hoaDon.MaHoaDon });
    }

    // POST: HoaDons/ThanhToan/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ThanhToan(int id)
    {
        var hoaDon = await _respository.GetDetailsAsync(id);

        if (hoaDon == null)
            return NotFound();

        if (hoaDon.TrangThai == TrangThaiTT.DaThanhToan)
        {
            TempData["ThongBao"] = "Hóa đơn đã được thanh toán.";

            return RedirectToAction(
                nameof(Details),
                new { id = hoaDon.MaHoaDon });
        }

        hoaDon.TrangThai = TrangThaiTT.DaThanhToan;

        await _respository.UpdateAsync(hoaDon);

        TempData["ThongBao"] = "Cập nhật thanh toán thành công.";

        return RedirectToAction(
            nameof(Details),
            new { id = hoaDon.MaHoaDon });
    }

    // Nạp danh sách đặt phòng đủ điều kiện lập hóa đơn
    private async Task NapDanhSachDatPhong()
    {
        var danhSach = await _respository.GetDanhSachDatPhongDuDieuKienAsync();

        ViewBag.MaDatPhong = new SelectList(
            danhSach,
            null,
            null);
    }
}