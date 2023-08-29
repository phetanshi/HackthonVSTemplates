using System.Linq.Expressions;

namespace DotnetApiTemplate.Logging.LogRepo
{
    public interface ILogRepository
    {
        #region GetList

        /// <summary>
        /// Fetches list of items from database
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> GetList<T>() where T : class;

        /// <summary>
        /// Fetches list of items from database (Async Method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <returns>Task<IQueryable<T>></returns>
        Task<IQueryable<T>> GetListAsync<T>() where T : class;

        /// <summary>
        /// Fetches list of items from database which are matched with give prediction
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="predicate"></param>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Fetches list of items from database which are matched with give prediction (Async Method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="predicate"></param>
        /// <returns>Task<IQueryable<T>></returns>
        Task<IQueryable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        #endregion

        #region GetSingle
        /// <summary>
        /// Fetches single object which is matched with given id (primary key)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="id">Primary key of a table</param>
        /// <returns>single object of given type</returns>
        T GetSingle<T>(int id) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given keys (composit key)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="compositKey"> combination of  composit key values</param>
        /// <returns>single object of given type</returns>
        T GetSingle<T>(params object[] compositKey) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given key value which is of string type (primary key)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="primaryKeyValue">String type primary key value</param>
        /// <returns>single object of given type</returns>
        T GetSingle<T>(string primaryKeyValue) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given prediction
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="predicate"></param>
        /// <returns>single object of given type</returns>
        T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given id (primary key) (Async method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="id">Primary key of a table</param>
        /// <returns>single object of given type</returns>
        Task<T> GetSingleAsync<T>(int id) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given keys (composit key) (Async Method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="compositKey"> combination of  composit key values</param>
        /// <returns>single object of given type</returns>
        Task<T> GetSingleAsync<T>(params object[] strCompositKey) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given key value which is of string type (primary key) (Async method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="primaryKeyValue">String type primary key value</param>
        /// <returns>single object of given type</returns>
        Task<T> GetSingleAsync<T>(string primaryKeyValue) where T : class;

        /// <summary>
        /// Fetches single object which is matched with given prediction (Async method)
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="predicate"></param>
        /// <returns>single object of given type</returns>
        Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion
    }
}
