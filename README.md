# 学习笔记

### 一、MVC中如何使用全局过滤器捕获异常

#### 1、首先新建一个过滤类

```C#
  public class MyExceptionAttribute : HandleErrorAttribute
    {
      	/// <summary>
        /// 创建一个队列用来储存错误信息
        /// </summary>
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        /// <summary>
        /// 可以捕获异常数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            //写入队列
            ExceptionQueue.Enqueue(ex);
            //跳转到错误页面
            filterContext.HttpContext.Response.Redirect("/Error.html");

        }
    }
```

#### 2、App_Start中的FilterConfig文件添加新建好的全局过滤错误器

````c#
 public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());
        }
    }
````

#### 3、在项目Global.asax文件中添加过滤器配置

```C#

        protected void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            // 在应用程序启动的时候添加全局过滤配置
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
```

#### 4、在项目中创建一个线程用来捕获异常信息

```C#

        protected void Application_Start(object sender, EventArgs e)
        {
            //其他配置精简见第3、
            
            //使用log4net包读取配置文件
            log4net.Config.XmlConfigurator.Configure();//读取了配置文件中的Log4Net配置信息
            //读取日志文件夹的服务器绝对路径
            string filePath = Server.MapPath("~/Log/");
            //开启一个线程,扫描异常信息队列
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
                            //根据log4net配置信息写入日志
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
```

#### [注意事项]

##### 1、添加配置信息（用于读取2步骤的配置信息）

```xml
  <configSections>
    <!--Log4Net配置-->
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
  </configSections>
```

##### 2、添加日志设置配置信息

###### 1)配置动态信息

```XML
 <log4net>
    <!-- OFF, FATAL, ERROR, WARN, INFO, DEBUG, ALL -->
    <!-- Set root logger level to ERROR and its appenders -->
    <root>
      <level value="ERROR"/>
      <appender-ref ref="SysAppender"/>
    </root>

    <!-- Print only messages of level DEBUG or above in the packages -->
    <logger name="WebLogger">
      <level value="ERROR"/>
    </logger>
    <appender name="SysAppender" type="log4net.Appender.RollingFileAppender,log4net" >
      <param name="File" value="App_Data/Logs/" />
      <param name="AppendToFile" value="true" />
      <param name="RollingStyle" value="Date" />
      <param name="DatePattern" value="&quot;Logs_&quot;yyyyMMdd&quot;.txt&quot;" />
      <param name="StaticLogFileName" value="false" />
      <layout type="log4net.Layout.PatternLayout,log4net">
        <param name="ConversionPattern" value="%d [%t] %-5p %c - %m%n" />
        <param name="Header" value="&#13;&#10;----------------------header--------------------------&#13;&#10;" />
        <param name="Footer" value="&#13;&#10;----------------------footer--------------------------&#13;&#10;" />
      </layout>
    </appender>
    <appender name="consoleApp" type="log4net.Appender.ConsoleAppender,log4net">
      <layout type="log4net.Layout.PatternLayout,log4net">
        <param name="ConversionPattern" value="%d [%t] %-5p %c - %m%n" />
      </layout>
    </appender>
  </log4net>
  <!--Log4Net配置结束-->
```

###### [注意]可配置异常时发送邮件给管理员

