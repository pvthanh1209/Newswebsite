using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Controllers.Base;
using Newswebsite.Models;
using System.Diagnostics;

namespace Newswebsite.Controllers
{
    public class HomeController : BaseAuthController<HomeController>
    {
        private readonly IBase _base;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
            _logger = logger;
            _base = @base;
        }

        public IActionResult Index()
        {
            var model = new HomeNews();
            model.ListNewsIsHot = _base.news.GetNewsIsHotAndIsHome();
            model.ListCategorieIsHome = _base.categories.Get(x => x.IsActive == true && x.IsHome == true).ToList();
            model.ListNewsIsHotHome = _base.news.GetNewsIsHotHome();
            model.ListNewsIsActiveAll = _base.news.GetNewsIsActiveAll();
            model.ListNewsByNew = _base.news.GetNewsIsActiveAll().Where(x => x.IsHome == true).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
