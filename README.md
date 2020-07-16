# Redis的使用方法

### 一、导入nuget包

![image-20200716124150516](./image-20200716124150516.png)

### 二、创建RedisHelper类

```C#
 public static class RedisHelper
    {
     	//主机配置
        static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379", "127.0.0.1:9999" });
     	//获取redis实例
        public static IRedisClient redisclient = clientManager.GetClient();
    }
```

### 三、使用Redis进行数据操作

```C#
  var redis = RedisHelper.redisclient;
			//该数据模拟动态数据
            var user = new UserInfo
            {
                Uid = 0,
                UName = "2250573213",
                UPwd = "zychhazl99"
            };
			//调用RedisHelper类Set<类型>(【键】key,【值】value,【过期时间】expired)
            RedisHelper.redisclient.Set<UserInfo>("user", user, DateTime.Now.AddSeconds(10));
            Console.ReadKey();
```



