using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOA.IDAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDBSession
    {
        DbContext Db { get; }
        IUserInfoDal UserInfoDal { get; set; }

        bool SaveChanges();

    }
}
