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
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            var temp = CurrentDBSession.Db.Set<UserInfo>().Add(userInfo);
            CurrentDBSession.SaveChanges();
            return temp;
        }

        public IEnumerable<UserInfo> DeleteUserInfo(UserInfo userInfo)
        {
            var db1 = CurrentDal.LoadEntities(u => true);
            var db2 = CurrentDal.GetDbContext();
            var res = db2.Set<UserInfo>().RemoveRange(db1);
            CurrentDBSession.SaveChanges();
            return res;
        }
    }
}
