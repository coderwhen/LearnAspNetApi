
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.Model;

namespace TestOA.IDAL
{
		/// <summary>
		/// Cart接口
		/// </summary>
		public partial interface ICartDal : IBaseDal<Cart>
		{
		
		}

		/// <summary>
		/// Goods接口
		/// </summary>
		public partial interface IGoodsDal : IBaseDal<Goods>
		{
		
		}

		/// <summary>
		/// UserInfo接口
		/// </summary>
		public partial interface IUserInfoDal : IBaseDal<UserInfo>
		{
		
		}

	
}