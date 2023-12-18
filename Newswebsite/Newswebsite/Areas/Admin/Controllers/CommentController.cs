using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Areas.Admin.Controllers.Base;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class CommentController : BaseController<CommentController>
    {
        public CommentController(ILogger<CommentController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetCommentData(string search, string startDate, string endDate, int offset, int limit)
        {
            int total = 0;
            var data = _base.comments.GetCommentListAllPaging(search, startDate, endDate, offset, limit);
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeIsActive(int Id)
        {
            try
            {
                var entity = _base.comments.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin bình luận không tồn tại");
                }
                entity.IsActive = (entity.IsActive ? false : true);
                _base.comments.Update(entity);
                _base.Commit();
                return Success("Cập nhật trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var entity = _base.comments.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin bình luận không tồn tại");
                }              
                _base.comments.Delete(entity);
                _base.Commit();
                return Success("Xóa thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
