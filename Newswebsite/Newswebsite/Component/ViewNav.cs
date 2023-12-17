using Microsoft.AspNetCore.Mvc;
using News.Base;
using Newswebsite.Models;

namespace Newswebsite.Component
{
    public class ViewNav : ViewComponent
    {
        private IBase _base;
        public ViewNav(IBase @base)
        {
            _base = @base;
        }
        public IViewComponentResult Invoke()
        {
            var model = new CategoryModel();
            model.ListCateParent = _base.categories.Get(x => x.IsActive == true && x.IsMenu == true).Take(3).ToList();
            model.ListCateDetail = _base.categoriesDetail.Get().ToList();
            return View(model);
        }
    }
}
