using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using System.Configuration;

namespace TestOA.RedisDemo
{
    public static class RedisHelper
    {
        static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379", "127.0.0.1:9999" });
        public static IRedisClient redisclient = clientManager.GetClient();
    }
}
