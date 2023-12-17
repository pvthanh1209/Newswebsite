using News.Base.Models;

namespace Newswebsite.Models
{
    public class CategoryModel
    {
        public List<Category> ListCateParent { get; set; }
        public List<CategoriesDetail> ListCateDetail { get; set;}
    }
}
