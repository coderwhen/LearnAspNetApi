# 学习笔记

### 一、MVC中如何使用SpringNet

##### 1、导入包

![image-20200709152039777](E:\TestOA\image-20200709152039777.png)

#### 2、添加配置容器信息

```xml
	<sectionGroup name="spring">
      <!--Spring.Net配置-->
      <section name="context" type="Spring.Context.Support.MvcContextHandler, Spring.Web.Mvc5" />
    </sectionGroup>
```

![image-20200709152228438](E:\TestOA\image-20200709152228438.png)

#### 3、添加依赖

![image-20200709152621954](E:\TestOA\image-20200709152621954.png)

```xml
 <spring>
    <!--Spring.Net配置-->
    <context>
      <!--
		配置分离
		<resource uri="file://~/Config/controllers.xml" />
	-->
      <resource uri="file://~/Config/controllers.xml" />
      <resource uri="file://~/Config/services.xml" />
    </context>
  </spring>
```



#### 4、设置自动实例化的对象

##### 1）设置实例化对象~/Config/controllers.xml

![image-20200709152826988](E:\TestOA\image-20200709152826988.png)

```XML
<!--
	设置自动实例化的对象
-->
<?xml version="1.0" encoding="utf-8" ?>
<objects xmlns="http://www.springframework.net">
  <!--
	<object type="[对象的全命名空间],[程序集]" singleton="true" >
    	<property name="[对象中的属性]" ref="[如果是一个特殊类型，则使用ref指向相同值name的object]" value=“[简单值]” />
  	</object>
	-->
  <object type="TestOA.WebApp.Controllers.UserInfoController,TestOA.WebApp" singleton="true" >
    <property name="UserInfoService" ref="UserInfoService" />
  </object>
</objects>

```

##### 2）设置实例化依赖对象~/Config/services.xml

```xml
<?xml version="1.0" encoding="utf-8" ?>
<objects xmlns="http://www.springframework.net">
  <object name="UserInfoService" type="TestOA.BLL.UserInfoService,TestOA.BLL"></object>
</objects>
```

![image-20200709153419303](E:\TestOA\image-20200709153419303.png)

#### 5、修改Global文件继承SpringMvcApplication

```c#
 public class Global : SpringMvcApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
```

