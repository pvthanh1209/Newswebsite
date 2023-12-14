using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Areas.Admin.Controllers.Base;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class NewsController : BaseController<NewsController>
    {
        public NewsController(ILogger<NewsController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            ViewDefault();
            return View();
        }
        public void ViewDefault()
        {
            ViewBag.ListCate = _base.categories.Get(x => x.IsActive == true).ToList();
        }
        public JsonResult GetNewsData(string search, int cateId, int offset, int limit)
        {
            var data = _base.news.GetNewListAllPaging(search, cateId, offset, limit);
            int total = 0;
            if (data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeStatusAll(int Id, int type)
        {
            try
            {
                var entity = _base.news.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                if(type == 1)
                {
                    entity.IsActive = (entity.IsActive ? false : true);
                }
                else if(type == 2)
                {
                    entity.IsHome = (entity.IsHome ? false : true);
                }
                else
                {
                    entity.IsHot = (entity.IsHot ? false : true);
                }
                _base.news.Update(entity);
                _base.Commit();
                return Success("Cập nhật trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
