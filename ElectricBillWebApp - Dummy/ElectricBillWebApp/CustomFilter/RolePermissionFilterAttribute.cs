using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElectricBillWebApp.CustomFilter
{
    public class RolePermissionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string? name = filterContext.HttpContext.Session.GetString("name");
            string? email = filterContext.HttpContext.Session.GetString("email");
            long? id = Convert.ToInt64(filterContext.HttpContext.Session.GetString("id"));

            if (id == null || id < 1)
            {
                var route_values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Login",
                });
                filterContext.Result = new RedirectToActionResult("Index", "Login", route_values);
                return;
            }            

            base.OnActionExecuting(filterContext);
        }
    }
}
