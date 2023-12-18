using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Controllers.Base;

namespace Newswebsite.Controllers
{
    public class CategoryController : BaseAuthController<CategoryController>
    {
        private readonly IBase _base;
        public CategoryController(ILogger<CategoryController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
            _base = @base;
        }
        public IActionResult Index(int Id, int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<News.Base.Models.News> lstNews = _base.news.GetNewsIsActiveAll().Where(x => x.CateId == Id).ToList();
            List<News.Base.Models.News> model = lstNews.Take(pageSize).Skip(startIndex).ToList();
            // Truyền dữ liệu phân trang vào view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)lstNews.Count / pageSize);
            return View(model);
        }
        public IActionResult CateDetail(int Id, int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<News.Base.Models.News> lstNews = _base.news.GetNewsIsActiveAll().Where(x => x.CateDetaiId == Id).ToList();
            List<News.Base.Models.News> model = lstNews.Take(pageSize).Skip(startIndex).ToList();
            // Truyền dữ liệu phân trang vào view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)lstNews.Count / pageSize);
            return View(model);
        }
    }
}
