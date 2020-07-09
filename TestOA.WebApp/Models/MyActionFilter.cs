using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace TestOA.WebApp.Models
{
    public class MyActionFilter : ActionFilterAttribute//, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ///调用前
            base.OnActionExecuting(filterContext);
            filterContext.Result = new RedirectResult("/Error.html");
            //filterContext.HttpContext.Response.Redirect("/Error.html");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //调用之后
            base.OnActionExecuted(filterContext);
            //filterContext.HttpContext.Response.Redirect("/Error.html");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //执行前
            base.OnResultExecuting(filterContext);
            //filterContext.HttpContext.Response.Redirect("/Error.html");
            //filterContext.Result = new RedirectResult("/Error.html");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //执行完
            base.OnResultExecuted(filterContext);
            //filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}