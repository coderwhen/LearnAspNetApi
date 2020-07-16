using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOA.RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = RedisHelper.redisclient;
            var user = new UserInfo
            {
                Uid = 0,
                UName = "2250573213",
                UPwd = "zychhazl99"
            };
            RedisHelper.redisclient.Set<UserInfo>("user", user, DateTime.Now.AddSeconds(10));
            Console.ReadKey();
        }
    }

    public class UserInfo
    {
        public int Uid { get; set; }
        public string UName { get; set; }

        public string UPwd { get; set; }
    }
}
