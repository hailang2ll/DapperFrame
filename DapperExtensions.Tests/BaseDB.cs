using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using DapperExtensions.Tests;
using DapperExtensions;
using System.Data.SqlClient;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Reflection;
using System.Data;

namespace DapperExtensions.Tests
{
    /// <summary>
    /// 基类-单例实体操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDB<T> where T : class
    {
        public IDatabase GetDatabase()
        {
            var connection = new SqlConnection("Integrated Security=False;server=192.168.0.101;database=WALIUJR_SYS;User ID=sa;Password=Cst-88888;Connect Timeout=30");
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            var sqlGenerator = new SqlGeneratorImpl(config);
            return new Database(connection, sqlGenerator);
        }

        #region GET
        public IEnumerable<T> GetList()
        {
            using (var db = GetDatabase())
            {
                return db.GetList<T>();
            }
        }

        public IEnumerable<T> GetList(object predicate = null, IList<ISort> sorts = null)
        {
            using (var db = GetDatabase())
            {
                return db.GetList<T>(predicate, sorts);
            }
        }

        public IEnumerable<T> GetPageList(object predicate, IList<ISort> sorts, int page = 1, int size = 10)
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

        public T Get(object id)
        {
            using (var db = GetDatabase())
            {
                return db.Get<T>(id);
            }
        }

        public int Count(object predicate = null)
        {
            using (var db = GetDatabase())
            {
                return db.Count<T>(predicate);
            }
        }

        #endregion

        //public bool Execute(string sql, object param = null, CommandType? commandType = null)
        //{
        //    using (var db = GetDatabase())
        //    {
        //        return db.Connection.Execute(sql, param, commandType: commandType) > 0;
        //    }
        //}


        public bool Insert(T entity)
        {
            using (var db = GetDatabase())
            {
                db.Insert(entity);
                return true;
            }
        }

        public bool Update(T entity)
        {
            using (var db = GetDatabase())
            {
                return db.Update(entity);
            }
        }

        //public bool Update(T entity, object parameters) 
        //{
        //    using (var db = GetDatabase())
        //    {
        //        return db.Update(entity,parameters);
        //    }
        //}

        public bool Delete(object parameters)
        {
            using (var db = GetDatabase())
            {
                return db.Delete<T>(parameters);
            }
        }

        public bool Delete(T entity)
        {
            using (var db = GetDatabase())
            {
                return db.Delete(entity);
            }
        }
    }
}
