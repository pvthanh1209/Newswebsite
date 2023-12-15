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
            model.ListCateParent = _base.categories.Get(x => x.IsActive == true && x.IsMenu == true && x.Parents == 0).Take(3).ToList();
            model.ListCateParentOne = _base.categories.Get(x => x.IsActive == true && x.IsMenu == true && x.Parents > 0).ToList();
            return View(model);
        }
    }
}
