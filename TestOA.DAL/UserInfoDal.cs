using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestOA.IDAL;
using TestOA.Model;

namespace TestOA.DAL
{
    public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal
    {
        public bool IsExist(UserInfo userInfo)
        {
            return LoadEntities(c => userInfo.Uid == c.Uid).Count() > 0;
        }
    }
}
