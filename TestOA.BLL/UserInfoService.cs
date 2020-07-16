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
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = CurrentDBSession.UserInfoDal;
        }

        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            var temp = CurrentDBSession.Db.Set<UserInfo>().Add(userInfo);
            CurrentDBSession.SaveChanges();
            return temp;
        }

        public IEnumerable<UserInfo> DeleteUserInfo(List<long> ids)
        {
            var temp = CurrentDBSession.Db.Set<UserInfo>().Where(u => ids.Contains(u.Uid));
            var ttt = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(temp);
            var tttemp = ttt.ToArray();
            if (CurrentDBSession.SaveChanges())
                return tttemp;
            else return null;
        }

        public object DeleteAllUserInfo()
        {
            var userInfoList = LoadEntities(c => true);
            var temp = CurrentDBSession.Db.Set<UserInfo>().RemoveRange(userInfoList);
            if (CurrentDBSession.SaveChanges())
            {
                return new { success = true };
            }
            return temp;
        }
    }
}
