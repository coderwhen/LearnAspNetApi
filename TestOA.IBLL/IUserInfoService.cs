using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.Model;

namespace TestOA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        UserInfo AddUserInfo(UserInfo userInfo);

        IEnumerable<UserInfo> DeleteUserInfo(List<long> ids);

        object DeleteAllUserInfo();
    }
}
