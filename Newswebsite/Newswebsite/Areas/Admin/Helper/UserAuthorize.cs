
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using MySqlX.XDevAPI;
using News.Base;
using Newswebsite.Areas.Admin.Helper;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace Doctorskin.Areas.Admin.Helper
{
    //public class UserAuthorize : TypeFilterAttribute
    //{
    //    public UserAuthorize(ActionModule[] modules, ActionTypeCustom[] actionTypeCustoms) : base(typeof(UserAuthorizeFilter))
    //    {
    //        Arguments = new object[] { modules, actionTypeCustoms };
    //    }
    //}
    //public class UserAuthorizeFilter : IAuthorizationFilter
    //{
    //    public ActionModule[] Modules;
    //    public ActionTypeCustom[] ActionTypeCustoms;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    protected IBase _ibase;
    //    public UserAuthorizeFilter(IBase @base, IHttpContextAccessor httpContextAccessor, ActionModule[] modules, ActionTypeCustom[] actionTypeCustoms)
    //    {
    //        Modules = modules;
    //        _ibase = @base;
    //        ActionTypeCustoms = actionTypeCustoms;
    //        _httpContextAccessor = httpContextAccessor;
    //    }
    //    //public void OnAuthorization(AuthorizationFilterContext context)
    //    //{
    //    //    var cookie = _httpContextAccessor.HttpContext.Session.GetString("manager");
    //    //    if (cookie != null)
    //    //    {
    //    //        var userAdmdin = JsonConvert.DeserializeObject<AdminSession>(cookie);
    //    //        var controller = Convert.ToString(context.RouteData.Values["controller"]);
    //    //        var action = Convert.ToString(context.RouteData.Values["action"]);
    //    //        Modules = Modules ?? new ActionModule[] { };
    //    //        ActionTypeCustoms = ActionTypeCustoms ?? new ActionTypeCustom[] { };
    //    //        if (Modules.Length == 0 && ActionTypeCustoms.Length == 0)
    //    //        {
    //    //            OnAuthorization(context);
    //    //            return;
    //    //        }
    //    //        try
    //    //        {
    //    //            var acc = _ibase.admins.GetByID(userAdmdin.UserId);
    //    //            if (acc == null)
    //    //            {
    //    //                context.Result = new RedirectResult("Admin/Login/Index");
    //    //                return;
    //    //            }
    //    //            Expression<Func<Doctors.Base.Models.Role, bool>> mypredicaterole = (_o => _o.RoleGroupId == (int)acc.RoleGroupId);
    //    //            List<Doctors.Base.Models.Role> lstRole = _ibase.roles.Get(mypredicaterole).ToList();
    //    //            if (lstRole != null && lstRole.Count > 0)
    //    //            {
    //    //                var checkAccess = AccessUser.checkAccess(lstRole, Modules, ActionTypeCustoms, (int)acc.RoleGroupId);
    //    //                if (!checkAccess)
    //    //                {
    //    //                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    //    //                    {
    //    //                        context.Result = new JsonResult(new
    //    //                        {
    //    //                            status = false,
    //    //                            code = 403,
    //    //                            message = Constant.MESSAGE_UNAUTHORISED
    //    //                        });
    //    //                    }
    //    //                    else
    //    //                    {
    //    //                        context.Result = new RedirectResult("/Admin/Unauthorised");
    //    //                    }
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    //    //                {
    //    //                    context.Result = new JsonResult(new
    //    //                    {
    //    //                        status = false,
    //    //                        code = 403,
    //    //                        message = Constant.MESSAGE_UNAUTHORISED
    //    //                    });
    //    //                }
    //    //                else
    //    //                {
    //    //                    context.Result = new RedirectResult("/Admin/Unauthorised");
    //    //                }
    //    //            }
    //    //        }
    //    //        catch (Exception ex)
    //    //        {

    //    //            context.Result = new JsonResult(new
    //    //            {
    //    //                status = false,
    //    //                code = 403,
    //    //                message = ex.Message
    //    //            });
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        context.Result = new RedirectResult("Admin/Login/Index");
    //    //    }
    //    //}
    //    //public class AccessUser
    //    //{
    //    //    public static bool checkAccess(List<Doctors.Base.Models.Role> AllRoleModuleUser, ActionModule[] Modules, ActionTypeCustom[] ActionTypeCustoms, int roleGroupId)
    //    //    {
    //    //        if (roleGroupId <= 0)
    //    //        {
    //    //            return false;
    //    //        }
    //    //        AllRoleModuleUser = AllRoleModuleUser ?? new List<Doctors.Base.Models.Role>();
    //    //        Modules = Modules ?? new ActionModule[] { };
    //    //        ActionTypeCustoms = ActionTypeCustoms ?? new ActionTypeCustom[] { };
    //    //        var ModulesList = Modules.Select(e => (int)e).ToList();
    //    //        if (ModulesList.Contains(-1))
    //    //        {
    //    //            return true;
    //    //        }
    //    //        var CheckUserModule = AllRoleModuleUser.Where(e => ModulesList.Contains(e.ModuleId)).ToList();
    //    //        var checkAccess = false;
    //    //        foreach (var actionType in ActionTypeCustoms)
    //    //        {
    //    //            if (actionType == ActionTypeCustom.All)
    //    //            {
    //    //                return true;
    //    //            }
    //    //            foreach (var module in CheckUserModule)
    //    //            {
    //    //                switch (actionType)
    //    //                {
    //    //                    case ActionTypeCustom.Add:
    //    //                        checkAccess = module.Add ? module.Add : false;
    //    //                        break;
    //    //                    case ActionTypeCustom.Edit:
    //    //                        checkAccess = module.Edit ? module.Edit : false;
    //    //                        break;
    //    //                    case ActionTypeCustom.View:
    //    //                        checkAccess = module.View ? module.View : false;
    //    //                        break;
    //    //                    case ActionTypeCustom.Delete:
    //    //                        checkAccess = module.Delete ? module.Delete : false;
    //    //                        break;
    //    //                    case ActionTypeCustom.Confirm:
    //    //                        checkAccess = module.Confirm ? module.Confirm : false;
    //    //                        break;                          
    //    //                    default:
    //    //                        checkAccess = false;
    //    //                        break;
    //    //                }
    //    //                if (checkAccess == true)
    //    //                {
    //    //                    break;
    //    //                }
    //    //            }
    //    //        }
    //    //        return checkAccess;
    //    //    }
    //    //}
    //}
}
