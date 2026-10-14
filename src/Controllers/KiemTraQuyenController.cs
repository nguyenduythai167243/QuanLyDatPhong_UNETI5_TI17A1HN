
using Microsoft.AspNetCore.Mvc;
using QuanLyDatPhong_UNETI5_TI17A1HN.Filters;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Controllers
{
    [SessionAuthorize("Admin")]
    public class KiemTraQuyenController : Controller
    {
        public IActionResult Index()
        {
            return Content(
                "Bạn đã đăng nhập và có quyền Admin.");
        }
    }
}
