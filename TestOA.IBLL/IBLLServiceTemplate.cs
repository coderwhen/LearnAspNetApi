

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.Model;

namespace TestOA.IBLL
{
    public partial interface ICartService : IBaseService<Cart>
    {
    }
    public partial interface IGoodsService : IBaseService<Goods>
    {
    }
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
    }
}