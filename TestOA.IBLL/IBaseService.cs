using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOA.IDAL;

namespace TestOA.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        IDBSession CurrentDBSession { get; }

        IBaseDal<T> CurrentDal { get; set; }

        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc);

        T AddEntity(T entity);
        bool EditEntity(T entity);
        bool DeleteEntity(T entity);
    }
}
