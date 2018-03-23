using Dapper.Contracts;
using Dapper.Model;
using DapperFrame.DapperAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Service
{
    public class Sys_DapperTestService : DapperOperation, ISys_DapperTest
    {
        #region 构造函数
        public Sys_DapperTestService()
            : base("WALIUJR_SYS")
        {

        }
        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<Sys_DapperTest> GetDapperList()
        {
            var result = this.GetList<Sys_DapperTest>().AsList();
            return result;
        }
    }
}
