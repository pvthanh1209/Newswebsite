using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Models;

namespace Newswebsite.Component
{
    public class ViewHeaderLoad: ViewComponent
    {
        private IBase _base;
        public ViewHeaderLoad(IBase @base)
        {
            _base = @base;
        }
        public IViewComponentResult Invoke()
        {
            var model = _base.news.GetNewsIsHotAndIsHome();
            return View(model);
        }
    }
}
