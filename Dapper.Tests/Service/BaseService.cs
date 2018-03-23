using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Dapper;
#if !NETCOREAPP1_0
using System.Threading;
#endif

namespace Dapper.Tests.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class BaseService : IDisposable
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=(local)\SQL2016;Database=tempdb;User ID=sa;Password=Password12!"
                : "Integrated Security=False;server=192.168.0.101;database=WALIUJR_SYS;User ID=sa;Password=Cst-88888;Connect Timeout=30";

        protected SqlConnection _connection;
        protected SqlConnection connection => _connection ?? (_connection = GetOpenConnection());

        public static SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = ConnectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }

}
