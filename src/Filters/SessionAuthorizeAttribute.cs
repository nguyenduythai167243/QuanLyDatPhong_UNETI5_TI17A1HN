using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Filters
{
    public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _vaiTroChoPhep;

        public SessionAuthorizeAttribute(params string[] vaiTroChoPhep)
        {
            _vaiTroChoPhep = vaiTroChoPhep;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;

            // Kiểm tra người dùng đã đăng nhập chưa.
            var maTaiKhoan = session.GetInt32("MaTaiKhoan");

            if (maTaiKhoan == null)
            {
                context.Result = new RedirectToActionResult(
                    "Login", "Account", null);

                return;
            }

            // Nếu có yêu cầu vai trò, kiểm tra quyền truy cập.
            if (_vaiTroChoPhep.Length > 0)
            {
                var vaiTro = session.GetString("VaiTro");

                if (string.IsNullOrEmpty(vaiTro)
                    || !_vaiTroChoPhep.Contains(
                        vaiTro,
                        StringComparer.OrdinalIgnoreCase))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }
}
