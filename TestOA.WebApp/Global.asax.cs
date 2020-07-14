using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Spring.Web.Mvc;
using System.Threading;
using TestOA.WebApp.Models;
using System.IO;

namespace TestOA.WebApp
{
    public class Global : SpringMvcApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //配置自己的线程任务
            IndexManager.GetInstance().StartThread();
        }
    }
}