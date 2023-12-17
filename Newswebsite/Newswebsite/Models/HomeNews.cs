using News.Base.Models;

namespace Newswebsite.Models
{
    public class HomeNews
    {
        public List<News.Base.Models.News> ListNewsIsHot { get; set;}
        public List<News.Base.Models.News> ListNewsIsHotHome { get; set;}
        public List<Category> ListCategorieIsHome { get; set;}   
        public List<News.Base.Models.News> ListNewsByNew { get; set; }
        public List<News.Base.Models.News> ListNewsIsActiveAll { get; set; }

    }
}
