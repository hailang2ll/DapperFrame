using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Reflection;
using System.Data;
using DapperExtensions;
using DapperFrame.Entity;
using DapperFrame.Helpers;

namespace DapperFrame.DapperAccess
{
    /// <summary>
    /// 基类-单例实体操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DapperOperation
    {

        private string _dbName;
        public DapperOperation(string dbName)
        {
            _dbName = dbName;
        }
        public IDatabase GetDatabase()
        {
#if NETSTANDARD2_0
            var tableConfigList = JsonConfigurationHelper.GetAppSettings<List<TableConfiguration>>("TableConfigCollection");
            var tableConfigEntity = tableConfigList.Where(q => q.Name == _dbName).Select(q => q.ConnectString).FirstOrDefault();
#elif NET45 
            var tableConfigList = JsonConfigurationHelper.GetAppSettings<TableConfiguration>();
            var tableConfigEntity = tableConfigList.Where(q => q.Name == _dbName).Select(q => q.ConnectString).FirstOrDefault();
#endif
            var connection = new SqlConnection(tableConfigEntity);
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            return new Database(connection, sqlGenerator);
        }

        #region Insert
        public bool Insert<T>(T entity) where T : class
        {
            using (var db = GetDatabase())
            {
                db.Insert(entity);
                return true;
            }
        }
        #endregion

        #region Update
        public bool Update<T>(T entity) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity);
            }
        }

        //public bool Update<T>(T entity, object parameters) where T : class
        //{
        //    using (var db = GetDatabase())
        //    {
        //        return db.Update(entity, parameters);
        //    }
        //}
        #endregion

        #region Delete
        public bool Delete<T>(object parameters) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.Delete<T>(parameters);
            }
        }

        public bool Delete<T>(T entity) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.Delete(entity);
            }
        }
        #endregion

        #region GET
        public T Get<T>(object id) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.Get<T>(id);
            }
        }
        public IEnumerable<T> GetList<T>() where T : class
        {
            using (var db = GetDatabase())
            {
                return db.GetList<T>();
            }
        }

        public IEnumerable<T> GetList<T>(object predicate = null, IList<ISort> sorts = null) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.GetList<T>(predicate, sorts);
            }
        }

        public IEnumerable<T> GetPageList<T>(object predicate, IList<ISort> sorts, int page = 1, int size = 10) where T : class
        {
            if (size <= 0)
            {
                return Enumerable.Empty<T>();
            }

            if (sorts == null || sorts.Count <= 0)
            {
                throw new ArgumentNullException("sorts", "排序条件不能为空");
            }

            page = page > 1 ? page : 1;
            using (var db = GetDatabase())
            {
                return db.GetPage<T>(predicate, sorts, page - 1, size);
            }
        }
        public int Count<T>(object predicate = null) where T : class
        {
            using (var db = GetDatabase())
            {
                return db.Count<T>(predicate);
            }
        }
        #endregion
    }
}
