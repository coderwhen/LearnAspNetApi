using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.DAL;
using TestOA.IDAL;
using TestOA.Model;

namespace TestOA.DALFactory
{
    public partial class DBSession : IDBSession
    {
        private ICartDal _CartDal;

        public ICartDal CartDal
        {
            get
            {
                if (_CartDal == null)
                {
                    _CartDal = AbstractFactory.CreateCartDal();//通过抽象工厂封装了类的实例的创建
                }
                return _CartDal;
            }

            set
            {
                _CartDal = value;
            }
        }
        private IGoodsDal _GoodsDal;

        public IGoodsDal GoodsDal
        {
            get
            {
                if (_GoodsDal == null)
                {
                    _GoodsDal = AbstractFactory.CreateGoodsDal();//通过抽象工厂封装了类的实例的创建
                }
                return _GoodsDal;
            }

            set
            {
                _GoodsDal = value;
            }
        }
        private IUserInfoDal _UserInfoDal;

        public IUserInfoDal UserInfoDal
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.CreateUserInfoDal();//通过抽象工厂封装了类的实例的创建
                }
                return _UserInfoDal;
            }

            set
            {
                _UserInfoDal = value;
            }
        }
    }
}