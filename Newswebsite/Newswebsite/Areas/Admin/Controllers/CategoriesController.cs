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
            return View();
        }
        public JsonResult GetCategoryListAll(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.categories.GetCategoryListAll(search, offset, limit);
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
                var entity = _base.categories.GetByID(Id);
                if (entity == null)
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
        public JsonResult ChangeIsHome(int Id)
        {
            try
            {
                var entity = _base.categories.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                entity.IsHome = (entity.IsHome ? false : true);
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
        public IActionResult CategroiesDetail(int cateId)
        {
            Category model = new Category();
            var cate = _base.categories.GetByID(cateId);
            if(cate != null)
            {
                model = cate;
            }
            return View(model);
        }
        public JsonResult GetDataCateDetail(int cateId)
        {
            int total = 0;
            var data = _base.categoriesDetail.Get(x => x.CateId == cateId).ToList();
            if(data != null && data.Count > 0)
            {
                total = data.Count;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult CreateOrEditDetail(CategoriesDetail model, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Error("Vui lòng nhập tên chi tiết danh mục");
                }
            
                var ckName = _base.categoriesDetail.Read(x => x.Name.ToLower().Equals(model.Name.ToLower().Trim()) && x.Id != model.Id);
                if (ckName != null)
                {
                    return Error("Tên loại thể loại danh mục đã tồn tại");
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
                    model.Thumb = pathDb;
                    _base.categoriesDetail.Insert(model);
                }
                else
                {
                    var entity = _base.categoriesDetail.GetByID(model.Id);
                    if (entity == null)
                    {
                        return Error("Thông tin danh mục chi tiết không tồn tại");
                    }
                    entity.Thumb = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Thumb);
                    entity.Name = model.Name;
                    entity.Title = model.Title;
                    _base.categoriesDetail.Update(entity);
                }
                _base.Commit();
                return Success("Cập nhật thông tin thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public JsonResult DeleteCateDetail(int Id)
        {
            try
            {
                var entity = _base.categoriesDetail.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                _base.categoriesDetail.Delete(entity);
                _base.Commit();
                return Success("Xóa thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public JsonResult ChangeIsActiveDetail(int Id)
        {
            try
            {
                var entity = _base.categoriesDetail.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin thể loại không tồn tại");
                }
                entity.IsHome = (entity.IsHome ? false : true);
                _base.categoriesDetail.Update(entity);
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
