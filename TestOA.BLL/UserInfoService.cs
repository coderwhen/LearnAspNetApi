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

        public IEnumerable<UserInfo> DeleteUserInfo(List<long> ids)
        {
            var temp = LoadEntities(c => true).AsQueryable().Where(c => ids.Contains(c.Uid)).AsEnumerable();
            temp = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(temp);
            if (CurrentDBSession.SaveChanges())
                return temp;
            else return null;
        }

        public object DeleteAllUserInfo()
        {
            var userInfoList = LoadEntities(c => true);
            var temp = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(userInfoList);
            CurrentDBSession.Db.SaveChanges();
            return temp;
        }
    }
}
