using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Areas.Admin.Controllers.Base;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class UserController : BaseController<UserController>
    {
        public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetUserData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.users.GetUserListAllPaging(search, offset, limit);
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
                var entity = _base.users.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin khách hàng không tồn tại");
                }
                entity.IsActive = (entity.IsActive ? false : true);
                _base.users.Update(entity);
                _base.Commit();
                return Success("Thay đổi trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
    }
}
