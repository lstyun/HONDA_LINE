using iWareCommon.Entity;
using iWareDao.Entity;
using System.Linq.Expressions;

namespace iWareBiz.IService
{
    public interface IBaseService
    {
        Task<List<T>> QueryAsync<T,S>(Expression<Func<T, bool>> funcWhere, 
            Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class;
       
        Task<PageResult<T>> QueryPageAsync<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex,
            Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class;

        Task<long> SaveAsync<T>(T obj) where T : BaseEntity;

        Task<long> DeleteAsync<T>(T obj) where T : BaseEntity;

        Task<T?> QueryByIdAsync<T>(long id) where T : BaseEntity;
    }

}
