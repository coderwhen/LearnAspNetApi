using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOA.SpringNet
{
    public class UserInfoService : IUserInfoService
    {
        public Person Person { get; set; }
        public string UserName;
        public string ShowMsg()
        {
            return "Hellow Worldhuy" + UserName + ":Age" + Person.Age;
        }
    }
}
