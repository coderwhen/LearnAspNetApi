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
using log4net;

namespace TestOA.WebApp
{
    public class Global : SpringMvcApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();//读取了配置文件中的Log4Net配置信息


            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //开启一个线程,扫描异常信息队列
            string filePath = Server.MapPath("~/Log/");
            ThreadPool.QueueUserWorkItem(a =>
            {
                ILog logger = LogManager.GetLogger("errorMsg");
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count() > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            logger.Error(ex.ToString());
                            ////将异常信息写到日志文件中
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                            //File.AppendAllText(a + fileName + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message + "\n", System.Text.Encoding.UTF8);
                        }
                    }
                    //如果队列中没有数据，休息3S
                    Thread.Sleep(3000);
                }
            }, filePath);
        }
    }
}