using Dapper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Contracts
{
    public interface ISys_DapperTest
    {
        /// <summary>
        /// 我的列表
        /// </summary>
        /// <param name="MemberName"></param>
        /// <returns></returns>
        List<Sys_DapperTest> GetDapperList();
    }
}
