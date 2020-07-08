using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.DALFactory;
using TestOA.IBLL;
using TestOA.IDAL;
using TestOA.Model;

namespace TestOA.BLL
{
    public class UserInfoService : BaseService<UserInfo>, IUserService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserInfoDal;
        }

        public IEnumerable<UserInfo> AddHHH()
        {
            List<UserInfo> list = new List<UserInfo>();
            list.Add(new UserInfo
            {
                UName = "zychhazl99",
                UPwd = "zychhazl99"
            });

            var temp = CurrentDBSession.Db.Set<UserInfo>().AddRange(list);
            CurrentDBSession.SaveChanges();
            return temp;
        }
    }

}
