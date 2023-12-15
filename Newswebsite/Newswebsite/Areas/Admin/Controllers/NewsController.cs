using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Areas.Admin.Controllers.Base;
using System.Text.RegularExpressions;

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
        [HttpGet]
        public IActionResult CreateOrEdit(int Id)
        {
            ViewDefault();  
            News.Base.Models.News model = new News.Base.Models.News();
            var data = _base.news.GetByID(Id);
            if(data != null)
            {
                model = data;
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateOrEdit(News.Base.Models.News model, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    return Error("Vui lòng nhập tiêu đề bài viết");
                }
                if(model.CateId == 0)
                {
                    return Error("Vui lòng chọn thể loại bài viết");
                }
                if (string.IsNullOrEmpty(model.ShortDescription))
                {
                    return Error("Vui lòng nhập mô tả ngắn");
                }
                var pathDb = string.Empty;
                if (fileUpload != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jpg|bmp|png|gif|jfif|JPG|PNG|BMP|GIF|JFIF)$");
                    if (!rgx.IsMatch(fileUpload.FileName))
                    {
                        return Error("Định dạnh ảnh không hợp lệ");
                    }
                    string filename = DateTime.Now.ToString("yyyyMMdd") + '_' + fileUpload.FileName;
                    string folderName = DateTime.Now.ToString("yyMMdd");
                    string newPath = Path.Combine(folderName);
                    string SavePath = Path.Combine("wwwroot/Uploads/Images", newPath, filename);
                    var fi = new FileInfo(SavePath);
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        fileUpload.CopyTo(stream);
                    }
                    pathDb = folderName + "\\" + filename;
                }
                if (model.Id == 0)
                {
                    model.CreatedDate = DateTime.Now;
                    model.IsActive = true;
                    model.Thumb = pathDb;
                    model.AccountId = GetUser().UserId;
                    _base.news.Insert(model);
                }
                else
                {
                    var entity = _base.news.GetByID(model.Id);
                    if(entity == null)
                    {
                        return Error("Không tìm thấy thông tin bài viết");
                    }
                    entity.CateId = model.CateId;
                    entity.Title = model.Title;
                    entity.Thumb = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Thumb);
                    entity.ShortDescription = model.ShortDescription;
                    entity.Description = model.Description;
                    _base.news.Update(entity);
                }
                _base.Commit();
                return Success("Cập nhật thông tin thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var entity = _base.news.GetByID(Id);
                if(entity == null)
                {
                    return Error("Thông tin bài viết không tồn tại");
                }
                _base.news.Delete(entity);
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
