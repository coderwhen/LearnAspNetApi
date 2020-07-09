# 控制器过滤器

### 一、设置MVC控制器过滤器（通用于未登入的情况下，访问了需要登录的控制器Action）

#### 1、创建一个BaseController

```C#
    public class BaseController : Controller
    {
        //重新当前方法用于进行业务身份验证
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
```

2、需要进行身份验证的控制器继承BaseController

```c#
 public class HomeController : BaseController//Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
```

这样我们的HomeController的所有方法都会进行登录身份验证

### 二、由于上述方法对控制器都需要登录，但是如果我们只需要对控制器的部分方法进行身份验证

#### 1、我们可以新建一个类继承ActionFilterAttribute类(我们可以选定部分方法重写达到验证身份) 或 IActionFilter接口(需要实现全部方法)



```C#
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
```

#### 2、对应的控制器上加入特性[MyActionFilter]

```C#
  	//控制器上加入代表整个控制器斗需要验证Session
  	//[MyActionFilter]
    public class HomeController : Controller
    {
        //控制器Action上加入代表当前Action要验证Session
        [MyActionFilter]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [MyActionFilter]
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
    }
```

