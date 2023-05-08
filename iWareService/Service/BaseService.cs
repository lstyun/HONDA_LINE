using iWareBiz.IService;
using iWareCommon.Entity;
using iWareDao.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iWareService.Service
{
    public class BaseService : IBaseService
    {
        protected readonly DbContext _dbContext;

        public BaseService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<long> DeleteAsync<T>(T obj) where T : BaseEntity {

            _dbContext.Set<T>().Remove(obj);

            await _dbContext.SaveChangesAsync();
            return obj.Id;
 
        }

        public virtual async Task<List<T>> QueryAsync<T, S>(Expression<Func<T, bool>> funcWhere, Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T:class
        {

            var list = isAsc ? _dbContext.Set<T>().Where(funcWhere).OrderBy(funcOrderBy) : _dbContext.Set<T>().Where(funcWhere).OrderByDescending(funcOrderBy);
           
            return await list.ToListAsync();
           
        }

        public virtual async Task<PageResult<T>> QueryPageAsync<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class
        {
            if (pageSize <= 0) 
            {
                pageSize = 1;
            }

            if (pageIndex <= 0) 
            { 
                pageIndex = 1;
            }

            var total = await _dbContext.Set<T>().Where(funcWhere).CountAsync();

            var pageCount = total == 0 ? 1 : (int)Math.Ceiling((decimal)total / pageSize);

            if (pageIndex > pageCount) 
            {
                pageIndex = pageCount;
            }


            var list = isAsc ? _dbContext.Set<T>().Where(funcWhere).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(funcOrderBy) : 
                _dbContext.Set<T>().Where(funcWhere).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(funcOrderBy);

            var c = await list.CountAsync();

            string rangeInfo;

            if (total == 0)
            {
                rangeInfo = "0";
            }
            else if (c <= 1)
            {
                rangeInfo = ((pageIndex - 1) * pageSize + 1) + "";
            }
            else 
            {
                int b = (pageIndex - 1) * pageSize + 1;
                int e = b + c - 1;
                rangeInfo = b + "~" + e;
            }


            return new PageResult<T> {
                RangeInfo = rangeInfo,
                Total = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageCount = pageCount,
                Data = await list.ToListAsync() 
            };
        }

        public virtual async Task<long> SaveAsync<T>(T obj) where T : BaseEntity
        {
            await _dbContext.Set<T>().AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id;
        }


        public virtual async Task<T?> QueryByIdAsync<T>(long id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

    }
}
