using Microsoft.AspNetCore.Mvc;
using News.Base;
using News.Base.Extension;
using News.Base.Models;
using Newswebsite.Areas.Admin.Controllers.Base;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        public AccountController(ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAdminData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.account.GetAllPaging(search, offset, limit);
            if (data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeIsActive(int Id)
        {
            try
            {
                var entity = _base.account.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin nhân viên không tồn tại");
                }
                entity.IsActive = (entity.IsActive ? false : true);
                _base.account.Update(entity);
                _base.Commit();
                return Success("Thay đổi trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public JsonResult CreateOrEdit(Account admin)
        {
            try
            {
                if (string.IsNullOrEmpty(admin.FullName) || string.IsNullOrEmpty(admin.Address) || string.IsNullOrEmpty(admin.PhoneNumber) || string.IsNullOrEmpty(admin.Username) || (admin.Id == 0 && string.IsNullOrEmpty(admin.Password)))
                {
                    return Error("Vui lòng nhập đầy đủ thông tin");
                }
                var ckUsername = _base.account.Read(x => x.Username.Equals(admin.Username.Trim()) && x.Id != admin.Id);
                if (ckUsername != null)
                {
                    return Error("Thông tin tài khoản đã tồn tại");
                }
                if (admin.Id == 0)
                {
                    var passHash = Hash.sha256(admin.Password);
                    admin.Password = passHash;
                    admin.IsActive = true;
                    _base.account.Insert(admin);
                }
                else
                {
                    var entity = _base.account.GetByID(admin.Id);
                    if (entity == null)
                    {
                        return Error("Cập nhật thông tin không thành công");
                    }
                    entity.FullName = admin.FullName;
                    entity.Address = admin.Address;
                    entity.PhoneNumber = admin.PhoneNumber;
                    entity.Password = (!string.IsNullOrEmpty(admin.Password) ? Hash.sha256(admin.Password) : entity.Password);
                    _base.account.Update(entity);
                }
                _base.Commit();
                return Success("Cập nhật thông tin thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
    }
}
