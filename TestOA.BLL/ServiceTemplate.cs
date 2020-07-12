
 
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
		public partial class CartService : BaseService<Cart>, ICartService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.CartDal;
        }
	}		
		public partial class GoodsService : BaseService<Goods>, IGoodsService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.GoodsDal;
        }
	}		
		public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
		public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserInfoDal;
        }
	}		
	}