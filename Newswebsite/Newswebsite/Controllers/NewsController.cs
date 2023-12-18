using Microsoft.AspNetCore.Mvc;
using News.Base;
using News.Base.Models;
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
            List<Comment> lstComment = new List<Comment>();
            List<News.Base.Models.News> lstNewRelated = new List<News.Base.Models.News>();
            ViewBag.ListNews = _base.news.GetNewsIsActiveAll().Where(x => x.IsHome == true).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            if (model != null)
            {
                lstNewRelated = _base.news.GetNewsIsActiveAll().Where(x => x.IsHome == true && x.CateId == model.CateId).Take(5).ToList();
                lstComment = _base.comments.GetCommentByNewsId(model.Id);
                InsertHistoryNews(model.Id);
            }
            ViewBag.ListNewRelated = lstNewRelated;
            ViewBag.ListCommnet = lstComment;
            return View(model);
        }
        public IActionResult NewsIstHot(int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<News.Base.Models.News> lstNews = _base.news.GetNewsIsActiveAll().Where(x => x.IsHot == true).ToList();
            List<News.Base.Models.News> model = lstNews.Take(pageSize).Skip(startIndex).ToList();
            // Truyền dữ liệu phân trang vào view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)lstNews.Count / pageSize);
            return View(model);
        }
        public JsonResult SendComment(int newsId, string comment)
        {
            try
            {
                var user = GetUser();
                if(user == null)
                {
                    return Error("Vui lòng đăng nhập trước khi bình luận");
                }
                if (newsId == 0)
                {
                    return Error("Không tìm thấy thông tin chi tiết bài viết");
                }
                if (string.IsNullOrEmpty(comment))
                {
                    return Error("Vui lòng nhập bình luận của bạn");
                }
                var obj = new Comment
                {
                    NewId = newsId,
                    UserId = user.CustomerId,
                    ContentComment = comment,
                    CreatedDate = DateTime.Now,
                    IsActive = false
                };
                _base.comments.Insert(obj);
                _base.Commit();
                return Success("Gửi bình luận thành công. Vui lòng chờ phê duyệt");
            }
            catch (Exception ex)
            {
                return Error("Có lỗi xảy ra. Vui lòng thử lại sau");
            }
        }
        public IActionResult HistoryViewNews(int? page)
        {
            int userId = 0;
            var user = GetUser();
            if(user != null)
            {
                userId = user.CustomerId;
            }
            int pageSize = 15;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<News.Base.Models.News> lstNews = _base.HistoryViewNews.GetHistoryNewsByUserId(userId);
            List<News.Base.Models.News> model = lstNews.Take(pageSize).Skip(startIndex).ToList();
            // Truyền dữ liệu phân trang vào view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)lstNews.Count / pageSize);
            return View(model);
        }
        public bool InsertHistoryNews(int newsId)
        {
            try
            {
                var user = GetUser();
                if (user != null)
                {
                    var entity = _base.HistoryViewNews.Read(x => x.UserId == user.CustomerId && x.NewsId == newsId);
                    if (entity == null)
                    {
                        var obj = new HistoryViewNews
                        {
                            NewsId = newsId,   
                            UserId = user.CustomerId,
                        };
                        _base.HistoryViewNews.Insert(obj);
                        _base.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
    }
}
