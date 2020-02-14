using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TradeFeed.DataAccess.DbContext
{
    public interface IDbContext : IDisposable
    {
        SqlDataReader ReturnDataReader(string storedProc);

        SqlDataReader ReturnDataReader(string storedProc, SqlParameter[] parameters);

        void ExecuteProc(string storedProc, SqlParameter[] parameters);
    }
}
