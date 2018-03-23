using Dapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Tests.Service
{
    /// <summary>
    /// Dapper原生SQL
    /// </summary>
    public class Sys_DapperService : BaseService
    {
        #region Get
        public void GetList()
        {
            var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest ORDER BY ID DESC").ToList();
            foreach (var item in list)
            {

            }
        }

        public void GetListByIN()
        {
            var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE ID IN @Ids", new { Ids = new int[] { 1, 2, 3, 4 } }).ToList();
            foreach (var item in list)
            {

            }
        }

        public void GetListByLike()
        {
            var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE Name LIKE @Name", new { Name = "%hai%" }).ToList();
            foreach (var item in list)
            {

            }
        }

        public void GetEntity()
        {
            var list = connection.Query<Sys_DapperTest>("SELECT * FROM dbo.Sys_DapperTest WHERE ID=@ID", new { ID = 13 }).First();

        }
        #endregion

        #region Insert
        public void Insert()
        {
            for (int i = 0; i <= 2; i++)
            {
                var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( N'hailang" + i + "',1)");
            }

        }
        public void InsertParam()
        {
            var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( @Name,@Age)", new { Name = "jiayou", Age = 2 });
            var state = connection.State;

        }

        public void InsertEntity()
        {
            Sys_DapperTest entity = new Sys_DapperTest()
            {
                Name = "aaaa",
                Age = 3,
            };
            var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( @Name,@Age)", entity);
            var state = connection.State;

        }
        #endregion

        #region Delete
        public void DeleteParam()
        {
            var flag = connection.Execute("DELETE FROM dbo.Sys_DapperTest WHERE ID=@ID", new { ID = 14 });

        }
        #endregion

        #region Update
        public void UpdateParam()
        {
            var flag = connection.Execute("UPDATE dbo.Sys_DapperTest SET Name=@Name WHERE ID=@ID", new { Name = "bbbb", ID = 15 });

        }

        public void UpdateEntity()
        {
            Sys_DapperTest entity = new Sys_DapperTest()
            {
                ID = 15,
                Name = "cccf",
            };
            var flag = connection.Execute("UPDATE dbo.Sys_DapperTest SET Name=@Name WHERE ID=@ID", entity);

        }
        #endregion

        #region Transaction
        public void TransactionCommit()
        {
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( N'hailangCCC',1)", transaction: transaction);
                    connection.Execute("insert into Sys_DapperProduct ([ProductName], [ProductCount]) values (N'ABC'+" + 1 + ", '1');", transaction: transaction);

                    transaction.Commit();
                }


            }
            finally
            {
            }
        }

        public void TransactionRollback()
        {

            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var flag = connection.Execute("INSERT dbo.Sys_DapperTest( Name, Age )VALUES  ( N'hailangddd',1)", transaction: transaction);

                    transaction.Rollback();
                }

            }
            finally
            {
            }
        }
        #endregion
    }
}
