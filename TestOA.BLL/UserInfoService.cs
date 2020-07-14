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
            //var temp = CurrentDBSession.Db.Set<UserInfo>().Where(u => u.UName == userInfo.UName && u.UPwd == userInfo.UPwd);
            //var count = temp.Count();
            //temp = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(temp).AsQueryable();
            //CurrentDBSession.SaveChanges();
            //var db1 = CurrentDBSession.Db.Set<UserInfo>();
            var db1 = CurrentDal.LoadEntities(u => true);
            db1 = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(db1).AsQueryable();
            //var db2 = CurrentDBSession.Db.Set<UserInfo>();
            return db1;
        }
    }
}
