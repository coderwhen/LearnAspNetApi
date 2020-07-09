using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestOA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //添加一个Session过滤基类控制器
            if (Session["userInfo"] == null)
            {
                ViewBag.msg = "您当前没有登录！";
                //此重定向不会截断http请求管道
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                //http请求拿到了ActionResult管道就不会继续执行了，从而实现请求拦截
                filterContext.Result = Redirect("/Error.html");
            }
        }
    }
}