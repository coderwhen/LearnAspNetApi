using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.DALFactory;
using TestOA.IDAL;
using TestOA.Model;

namespace TestOA.BLL
{
    public class UserInfoService : BaseService<UserInfo>
    {
        public IDBSession CurrentDbSession
        {
            get
            {
                return new DBSession();
            }
        }
    }
}
