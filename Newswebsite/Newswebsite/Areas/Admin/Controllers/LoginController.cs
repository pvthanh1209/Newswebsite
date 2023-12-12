using Microsoft.AspNetCore.Mvc;
using News.Base;
using News.Base.Extension;
using News.Base.Session;
using Newswebsite.Areas.Admin.Controllers.Base;
using Newswebsite.Areas.Admin.Helper;
using Newswebsite.Areas.Admin.Models;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class LoginController : BaseController<LoginController>
    {
        public LoginController(ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Error("Vui lòng nhập tài khoản và mật khẩu");
            }
            string passHash = Hash.sha256(password.Trim());
            var data = _base.account.Read(x => x.Username.ToLower().Equals(username.ToLower().Trim()) && x.Password.Equals(passHash));
            if (data == null)
            {
                return Error("Tài khoản hoặc mật khẩu không đúng");
            }
            if(data.IsActive == false)
            {
                return Error("Tài khoản của bạn đã bị khóa. Vui lòng liện hệ với quản trị viên");
            }
            SetUser(new AdminSession
            {
                SessionId = _contextAccessor.HttpContext.Session.Id,
                Username = data.Username,
                UserId = data.Id
            });
            return Success("Đăng nhập thành công");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            ClearUser();
            return Redirect("/Admin/Login/Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var user = Sessions.GetUser(_contextAccessor);
                if (string.IsNullOrEmpty(changePassword.Password))
                {
                    return Error("Vui lòng nhập mật khẩu cũ!");
                }
                if (string.IsNullOrEmpty(changePassword.NewPassword))
                {
                    return Error("Vui lòng nhập mật khẩu mới!");
                }
                if (string.IsNullOrEmpty(changePassword.RePassword))
                {
                    return Error("Vui lòng nhập mật khẩu xác nhận!");
                }
                if (!changePassword.NewPassword.ToLower().Trim().Equals(changePassword.RePassword.ToLower().Trim()))
                {
                    return Error("Xác nhận mật khẩu không đúng!");
                }
                string password = Hash.sha256(changePassword.Password.Trim());
                string newPassword = Hash.sha256(changePassword.NewPassword.Trim());
                if (user != null)
                {
                    var entity = _base.account.GetByID(user.UserId);
                    if (entity != null)
                    {
                        if (!entity.Password.Equals(password))
                        {
                            return Error("Mật khẩu cũ không chính xác!");
                        }
                        if (entity.Password.Equals(newPassword))
                        {
                            return Error("Mật khẩu mới không thể trùng với mật khẩu cũ!");
                        }
                        entity.Password = newPassword;
                        _base.account.Update(entity);
                        _base.Commit();
                        return Success("Đôỉ mật khẩu thành công, chuyển hướng đến đăng nhập!");
                    }
                    else
                    {
                        return Error("Cập nhật mật khẩu không thành công!");
                    }
                }
                else
                {
                    return Error("Cập nhật mật khẩu không thành công!");
                }
            }
            catch (Exception ex)
            {
                return Error("Error " + ex);
            }
        }
    }
}
