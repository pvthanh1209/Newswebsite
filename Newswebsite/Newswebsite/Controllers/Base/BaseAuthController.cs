using Microsoft.AspNetCore.Mvc;
using News.Base.Session;
using News.Base;
using Newtonsoft.Json;

namespace Newswebsite.Controllers.Base
{
    public class BaseAuthController<T> : Controller where T : BaseAuthController<T>
    {
        #region Contructor
        protected IBase _base;
        protected IHttpContextAccessor _contextAccessor;
        protected IConfiguration _configuration { get; }
        protected ILogger<T> _logger;
        public BaseAuthController(IBase @base, IHttpContextAccessor contextAccessor, IConfiguration configuration, ILogger<T> logger)
        {
            _base = @base;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }
        #endregion
        #region Session
        protected CustomerSession GetUser()
        {
            var userSession = _contextAccessor.HttpContext.Session.GetString("customer");
            if (userSession != null)
            {
                return JsonConvert.DeserializeObject<CustomerSession>(userSession);
            }
            return null;
        }
        protected void SetUser(CustomerSession data)
        {
            _contextAccessor.HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(data));
        }
        protected void ClearUser()
        {
            _contextAccessor.HttpContext.Session.Remove("customer");
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
