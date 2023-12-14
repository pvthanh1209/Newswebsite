using Microsoft.AspNetCore.Mvc;
using News.Base;
using News.Base.Models;
using Newswebsite.Areas.Admin.Controllers.Base;
using System.Text.RegularExpressions;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController<CategoriesController>
    {
        public CategoriesController(ILogger<CategoriesController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            ViewBag.ListCate = _base.categories.Get(x => x.IsActive == true).ToList();
            return View();
        }
        public JsonResult GetCategoryListAll(string search)
        {
            int total = 0;
            var data = _base.categories.GetCategoryListAll(search);
            if(data != null && data.Count > 0)
            {
                total = data.Count; 
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeIsActive(int Id)
        {
            try
            {
                var entity = _base.categories.GetByID(Id);
                if(entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                entity.IsActive = (entity.IsActive ? false : true);
                _base.categories.Update(entity);
                _base.Commit();
                return Success("Cập nhật trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public JsonResult ChangeIsMenu(int Id)
        {
            try
            {
                var entity = _base.categories.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                entity.IsMenu = (entity.IsMenu ? false : true);
                _base.categories.Update(entity);
                _base.Commit();
                return Success("Cập nhật trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public JsonResult CreateOrEdit(Category model, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CateName))
                {
                    return Error("Vui lòng nhập loại tin tức");
                }
                if (string.IsNullOrEmpty(model.Title))
                {
                    return Error("Vui lòng nhập tiêu đề loại tin tức");
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
                var ckName = _base.categories.Read(x => x.CateName.Equals(model.CateName.Trim()) && x.Id != model.Id);
                if (ckName != null)
                {
                    return Error("Tên loại thể loại danh mục đã tồn tại");
                }
                if (model.Id == 0)
                {
                    model.IsActive = true;
                    model.CreatedDate = DateTime.Now;
                    model.Thumb = pathDb;
                    _base.categories.Insert(model);
                }
                else
                {
                    var entity = _base.categories.GetByID(model.Id);
                    if (entity == null)
                    {
                        return Error("Thông tin thể loại danh mục không tồn tại");
                    }
                    entity.CateName = model.CateName;
                    entity.Title = model.Title;
                    entity.Thumb = (!string.IsNullOrEmpty(pathDb) ? pathDb : string.Empty);
                    _base.categories.Update(entity);
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
