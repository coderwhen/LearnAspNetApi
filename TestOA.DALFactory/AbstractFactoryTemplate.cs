
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TestOA.IDAL;
using System.Reflection;

namespace TestOA.DALFactory
{
    public partial class AbstractFactory
    {
			public static ICartDal CreateCartDal()
		{
			string fullClassName = NameSpace + ".CartDal";
			return CreateInstance(fullClassName) as ICartDal;
        }
			public static IGoodsDal CreateGoodsDal()
		{
			string fullClassName = NameSpace + ".GoodsDal";
			return CreateInstance(fullClassName) as IGoodsDal;
        }
			public static IUserInfoDal CreateUserInfoDal()
		{
			string fullClassName = NameSpace + ".UserInfoDal";
			return CreateInstance(fullClassName) as IUserInfoDal;
        }
		}
}
