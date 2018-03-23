using Dapper.Tests.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Tests
{
    class Program 
    {
        static void Main(string[] args)
        {
            Sys_DapperService db = new Sys_DapperService();
            db.TransactionCommit();
        }
    }
}
