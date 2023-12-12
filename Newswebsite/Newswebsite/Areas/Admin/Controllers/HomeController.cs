using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Areas.Admin.Controllers.Base;

namespace Newswebsite.Areas.Admin.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
