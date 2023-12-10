using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Extension
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ThrottleAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Gets and sets the Throttling seconds
        /// </summary>
        public int Seconds { get; set; }

        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext c)
        {

            var ipAddress = c.HttpContext.Connection.RemoteIpAddress?.ToString();

            var key = ipAddress;

            if (!Cache.TryGetValue(key, out bool entry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(key, true, cacheEntryOptions);
            }
            else
            {
                string Message = "Spam yêu cầu quá nhiều, thử lại sau {n} giây.";
                if (c.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    c.Result = new OkObjectResult(new
                    {
                        status = false,
                        message = Message.Replace("{n}", Seconds.ToString())
                    });
                    c.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    c.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                    c.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
        }
    }
}
