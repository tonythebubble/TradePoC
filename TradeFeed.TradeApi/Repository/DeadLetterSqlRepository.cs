using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;
using TradeFeed.DataAccess.DbContext;
using System.Data.SqlClient;

namespace TradeFeed.TradeApi.Repository
{
    public class DeadLetterSqlRepository : IDeadLetterRepository
    {
        protected readonly IDbContext _Context;

        public DeadLetterSqlRepository(IDbContext context)
        {
            _Context = context;
        }

        public async Task<DeadLetter> GetDeadLetterAsync(int id)
        {
            DeadLetter deadLetter = new DeadLetter();
            await Task.Run(() => { deadLetter = GetDeadLetter(id); });

            return deadLetter;
        }

        public DeadLetter GetDeadLetter(int id)
        {
            DeadLetter deadLetter = new DeadLetter();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", id);

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_DeadLetter_Get", parameters))
            {
                while (reader.Read())
                {
                    deadLetter = PopulateDeadLetter(reader);
                }
                reader.Close();
            }

            return deadLetter;
        }

        public async Task<IEnumerable<DeadLetter>> GetAllDeadLettersAsync()
        {
            IEnumerable<DeadLetter> deadLetters = new List<DeadLetter>();
            await Task.Run(() => { deadLetters = GetAllDeadLetters(); });

            return deadLetters;
        }

        public IEnumerable<DeadLetter> GetAllDeadLetters()
        {
            List<DeadLetter> deadLetters = new List<DeadLetter>();

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_DeadLetter_Get"))
            {
                while (reader.Read())
                {
                    deadLetters.Add(PopulateDeadLetter(reader));
                }
                reader.Close();
            }

            return deadLetters;
        }

        private DeadLetter PopulateDeadLetter(IDataRecord record)
        {
            return new DeadLetter()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                Body = record["Body"].ToString(),
                Message = record["Message"].ToString()
            };
        }

        private SqlParameter[] GetParameters(DeadLetter deadLetter)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Body", deadLetter.Body.Replace("'","").Replace("\"", ""));
            parameters[1] = new SqlParameter("@Message", deadLetter.Message.Replace("'", "").Replace("\"", ""));

            return parameters;
        }

        public async Task CreateDeadLetterAsync(DeadLetter deadLetter)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_DeadLetter_Create", GetParameters(deadLetter)); });
        }

    }
}
