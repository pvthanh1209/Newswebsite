using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Controllers.Base;

namespace Newswebsite.Controllers
{
    public class NewsController : BaseAuthController<NewsController>
    {
        private readonly IBase _base;
        public NewsController(ILogger<NewsController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
            _logger = logger;
            _base = @base;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<News.Base.Models.News> lstNews = _base.news.GetNewsIsActiveAll();
            List<News.Base.Models.News> model = lstNews.Take(pageSize).Skip(startIndex).ToList();
            // Truyền dữ liệu phân trang vào view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)lstNews.Count / pageSize);
            return View(model);
        }
        public IActionResult NewDetail(int Id)
        {
            News.Base.Models.News model = _base.news.GetNewsById(Id);
            List<News.Base.Models.News> lstNewRelated = new List<News.Base.Models.News>();
            ViewBag.ListNews = _base.news.GetNewsIsActiveAll().Where(x => x.IsHome == true).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            if (model != null)
            {
                lstNewRelated = _base.news.GetNewsIsActiveAll().Where(x => x.IsHome == true && x.CateId == model.CateId).Take(5).ToList();
            }
            ViewBag.ListNewRelated = lstNewRelated;
            return View(model);
        }
    }
}
