using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TradeFeed.DataAccess.DbContext
{
    public class SqlDbContext : IDbContext
    {
        private bool disposed = false; // to detect redundant calls

        private string _connectionString;

        private SqlConnection DBConnection = new SqlConnection();

        public SqlDbContext(string connectionString)
        {
            _connectionString = connectionString;

            DBConnection.ConnectionString = _connectionString;
            DBConnection.Open();
        }

        public SqlDataReader ReturnDataReader(string storedProc, SqlParameter[] parameters)
        {
            List<IDataRecord> records = new List<IDataRecord>();

            using (SqlCommand command = CreateCommand(storedProc))
            {
                foreach (SqlParameter parameter in parameters)
                    command.Parameters.Add(parameter);

                return command.ExecuteReader();

            }
        }

        public SqlDataReader ReturnDataReader(string storedProc)
        {
            List<IDataRecord> records = new List<IDataRecord>();

            using (SqlCommand command = CreateCommand(storedProc))
            {
                return command.ExecuteReader();
            }
        }

        public void ExecuteProc(string storedProc, SqlParameter[] parameters)
        {
            using (SqlCommand command = CreateCommand(storedProc))
            {
                foreach (SqlParameter parameter in parameters)
                    command.Parameters.Add(parameter);

                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        private SqlCommand CreateCommand(string storedProc)
        {
            SqlCommand command = DBConnection.CreateCommand();
            command.CommandText = storedProc;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            return command;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
                DBConnection.Close();
        }
    }
}
