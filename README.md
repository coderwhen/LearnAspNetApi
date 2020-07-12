# Memcached缓存的使用

### 一、安装Memcached

#### 1、下载Memcached

```
    Windows 下安装 Memcached
    官网上并未提供 Memcached 的 Windows 平台安装包，我们可以使用以下链接来下载，你需要根据自己的系统平台及需要的版本号点击对应的链接下载即可：

    32位系统 1.2.5版本：http://static.runoob.com/download/memcached-1.2.5-win32-bin.zip
    32位系统 1.2.6版本：http://static.runoob.com/download/memcached-1.2.6-win32-bin.zip
    32位系统 1.4.4版本：http://static.runoob.com/download/memcached-win32-1.4.4-14.zip
    64位系统 1.4.4版本：http://static.runoob.com/download/memcached-win64-1.4.4-14.zip
    32位系统 1.4.5版本：http://static.runoob.com/download/memcached-1.4.5-x86.zip
    64位系统 1.4.5版本：http://static.runoob.com/download/memcached-1.4.5-amd64.zip
    在 1.4.5 版本以前 memcached 可以作为一个服务安装，而在 1.4.5 及之后的版本删除了该功能。因此我们以下介绍两个不同版本 1.4.4 及 1.4.5的不同安装方法：
```

2、安装Memcached服务

```c#
memcached <1.4.5 版本安装
1、解压下载的安装包到指定目录。

2、在 1.4.5 版本以前 memcached 可以作为一个服务安装，使用管理员权限运行以下命令：

c:\memcached\memcached.exe -d install
注意：你需要使用真实的路径替代 c:\memcached\memcached.exe。

3、然后我们可以使用以下命令来启动和关闭 memcached 服务：

c:\memcached\memcached.exe -d start
c:\memcached\memcached.exe -d stop
4、如果要修改 memcached 的配置项, 可以在命令行中执行 regedit.exe 命令打开注册表并找到 "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\memcached" 来进行修改。

如果要提供 memcached 使用的缓存配置 可以修改 ImagePath 为:

"c:\memcached\memcached.exe" -d runservice -m 512
-m 512 意思是设置 memcached 最大的缓存配置为512M。

此外我们还可以通过使用 "c:\memcached\memcached.exe -h" 命令查看更多的参数配置。

5、如果我们需要卸载 memcached ，可以使用以下命令：

c:\memcached\memcached.exe -d uninstall
memcached >= 1.4.5 版本安装
1、解压下载的安装包到指定目录。

2、在 memcached1.4.5 版本之后，memcached 不能作为服务来运行，需要使用任务计划中来开启一个普通的进程，在 window 启动时设置 memcached自动执行。

我们使用管理员身份执行以下命令将 memcached 添加来任务计划表中：

schtasks /create /sc onstart /tn memcached /tr "'c:\memcached\memcached.exe' -m 512"
注意：你需要使用真实的路径替代 c:\memcached\memcached.exe。

注意：-m 512 意思是设置 memcached 最大的缓存配置为512M。

注意：我们可以通过使用 "c:\memcached\memcached.exe -h" 命令查看更多的参数配置。

3、如果需要删除 memcached 的任务计划可以执行以下命令：

schtasks /delete /tn memcached
```

[更多信息请点击这里](https://www.runoob.com/Memcached/window-install-memcached.html)

### 二、在项目中导入Memcached.ClientLibrary包

#### ![image-20200712113151805](.\image-20200712113151805.png)

### 三、封装Memcached帮助类

```C#
using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOA.Common
{
    public class MemcachedHelper
    {
        private static readonly MemcachedClient mc = null;
        static MemcachedHelper()
        {
            string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set(string key, object value)
        {
            return mc.Set(key, value);
        }
        public static bool Set(string key, object value, DateTime expiry)
        {
            return mc.Set(key, value, expiry);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
                return mc.Delete(key);
            }
            return false;
        }
    }
}
```

### 四、需要使用的地方调用对应的方法

```C#
	public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Message = MemcachedHelper.Get("UserInfo")
            return View();
        }
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
    }
```

