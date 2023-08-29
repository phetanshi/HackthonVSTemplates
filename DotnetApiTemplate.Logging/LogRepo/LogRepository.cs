using DotnetApiTemplate.Logging.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DotnetApiTemplate.Logging.LogRepo
{
    public class LogRepository : ILogRepository
    {
        public LogRepository(LogDbContext database)
        {
            Database = database;
        }

        public LogDbContext Database { get; }

        #region GetList
        public IQueryable<T> GetList<T>() where T : class
        {
            return Database.Set<T>();
        }

        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Database.Set<T>().Where(predicate);
        }

        public async Task<IQueryable<T>> GetListAsync<T>() where T : class
        {
            var result = GetList<T>();
            return await Task.FromResult(result);
        }

        public async Task<IQueryable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var result = GetList<T>(predicate);
            return await Task.FromResult(result);
        }
        #endregion

        #region GetSingle
        public T GetSingle<T>(int id) where T : class
        {
            return Database.Set<T>().Find(id);
        }
        public T GetSingle<T>(params object[] compositKey) where T : class
        {
            return Database.Set<T>().Find(compositKey);
        }
        public T GetSingle<T>(string primaryKeyValue) where T : class
        {
            return Database.Set<T>().Find(primaryKeyValue);
        }
        public T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Database.Set<T>().FirstOrDefault(predicate);
        }


        public async Task<T> GetSingleAsync<T>(int id) where T : class
        {
            return await Database.Set<T>().FindAsync(id);
        }
        public async Task<T> GetSingleAsync<T>(params object[] strCompositKey) where T : class
        {
            return await Database.Set<T>().FindAsync(strCompositKey);
        }
        public async Task<T> GetSingleAsync<T>(string primaryKeyValue) where T : class
        {
            return await Database.Set<T>().FindAsync(primaryKeyValue);
        }
        public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Database.Set<T>().FirstOrDefaultAsync(predicate);
        }
        #endregion
    }
}
