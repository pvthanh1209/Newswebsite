using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News.Base.Session;
using News.Base;
using Newtonsoft.Json;

namespace Newswebsite.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        #region Contructor
        protected IBase _base;
        protected IHttpContextAccessor _contextAccessor;
        protected IConfiguration _configuration { get; }
        protected ILogger<T> _logger;
        public BaseController(IBase @base, IHttpContextAccessor contextAccessor, IConfiguration configuration, ILogger<T> logger)
        {
            _base = @base;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }
        #endregion
        #region Session
        protected AdminSession GetUser()
        {
            try
            {
                var userSession = _contextAccessor.HttpContext.Session.GetString("manager");
                if (userSession != null)
                {
                    return JsonConvert.DeserializeObject<AdminSession>(userSession);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected void SetUser(AdminSession data)
        {
            _contextAccessor.HttpContext.Session.SetString("manager", JsonConvert.SerializeObject(data));
        }
        protected void ClearUser()
        {
            _contextAccessor.HttpContext.Session.Remove("manager");
        }
        #endregion
        #region Authorize
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ActionDescriptor.RouteValues["Controller"].ToLower() != "login")
            {
                if (GetUser() != null)
                {
                    base.OnActionExecuted(context);
                }
                else
                {
                    context.Result = new RedirectResult("/Admin/Login/Index");
                }
            }
            else
            {
                base.OnActionExecuted(context);
            }
        }
        #endregion
        #region JsonResult
        protected JsonResult Success(string message)
        {
            return new JsonResult(new
            {
                status = true,
                message = message
            });
        }
        protected JsonResult Error(string message)
        {
            return new JsonResult(new
            {
                status = false,
                message = message
            });
        }
        protected JsonResult Data(object data)
        {
            return new JsonResult(data);
        }
        #endregion
    }
}
