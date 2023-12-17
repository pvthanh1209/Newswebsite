using Microsoft.AspNetCore.Mvc;
using News.Base;
using News.Base.Models;
using Newswebsite.Models;

namespace Newswebsite.Component
{
    public class ViewHeader : ViewComponent
    {
        private IBase _base;
        public ViewHeader(IBase @base)
        {
            _base = @base;
        }
        public IViewComponentResult Invoke()
        {
            var model = new CategoryModel();
             model.ListCateParent = _base.categories.Get(x => x.IsActive == true && x.IsMenu == true ).Take(3).ToList();
            model.ListCateDetail = _base.categoriesDetail.Get().ToList();
            return View(model);
        }
    }
}
