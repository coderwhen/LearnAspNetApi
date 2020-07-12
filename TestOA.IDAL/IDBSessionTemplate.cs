

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.Model;

namespace TestOA.IDAL
{
    public partial interface IDBSession
    {
        ICartDal CartDal { get; set; }
        IGoodsDal GoodsDal { get; set; }
        IUserInfoDal UserInfoDal { get; set; }
    }
}