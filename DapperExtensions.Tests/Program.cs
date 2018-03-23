using Dapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtensions.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取所有集合
            UpdateEntity();



        }



        public static void GetList()
        {
            BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
            var list = db.GetList();
            foreach (var item in list)
            {

            }
        }

        public static void GetPageList()
        {
            BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();

            //条件
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Sys_DapperTest>(x => x.ID, Operator.Eq, new List<int>() { 1, 2, 3, 4 }));

            //排序
            List<ISort> sort = new List<ISort>();
            sort.Add(Predicates.Sort<Sys_DapperTest>(p => p.ID, true));

            var list = db.GetPageList(pg, sort, 1, 10).ToList();
            int total = db.Count();
            foreach (var item in list)
            {

            }
        }

        public static void GetEntity()
        {
            BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
            Sys_DapperTest entity = db.Get(15);
        }



        public static void InsertEntity()
        {
            Sys_DapperTest entity = new Sys_DapperTest()
            {
                Name = "lang",
                Age = 3,
            };
            BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
            bool flag = db.Insert(entity);

        }


        public static void UpdateEntity()
        {
            Sys_DapperTest entity = new Sys_DapperTest()
            {
                ID = 21,
                Name = "123langds",
            };
            BaseDB<Sys_DapperTest> db = new BaseDB<Sys_DapperTest>();
            bool flag = db.Update(entity);

        }

    }
}
