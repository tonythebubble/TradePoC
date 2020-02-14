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
    public class TradeSqlRepository : ITradeRepository
    {
        protected readonly IDbContext _Context;

        public TradeSqlRepository(IDbContext context)
        {
            _Context = context;
        }

        public async Task<Trade> GetTradeAsync(int id)
        {
            Trade trade = new Trade();
            await Task.Run(() => { trade = GetTrade(id); });

            return trade;
        }

        public Trade GetTrade(int id)
        {
            Trade trade = new Trade();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", id);

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Trade_Get", parameters))
            {
                while (reader.Read())
                {
                    trade = PopulateTrade(reader);
                }
                reader.Close();
            }

            return trade;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            IEnumerable<Trade> trades = new List<Trade>();
            await Task.Run(() => { trades = GetAllTrades(); });

            return trades;
        }

        public IEnumerable<Trade> GetAllTrades()
        {
            List<Trade> trades = new List<Trade>();

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Trade_Get"))
            {
                while (reader.Read())
                {
                    trades.Add(PopulateTrade(reader));
                }
                reader.Close();
            }

            return trades;
        }

        private Trade PopulateTrade(IDataRecord record)
        {
            return new Trade()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                StockId = Int32.Parse(record["StockId"].ToString()),
                ClientId = Int32.Parse(record["ClientId"].ToString()),
                Venue = record["Venue"].ToString(),
                Quantity = Int32.Parse(record["Quantity"].ToString()),
                Price = decimal.Parse(record["Price"].ToString()),
                BuySell = record["BuySell"].ToString()
            };
        }

        private SqlParameter[] GetParameters(Trade trade)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@StockId", trade.StockId);
            parameters[1] = new SqlParameter("@ClientId", trade.ClientId);
            parameters[2] = new SqlParameter("@Venue", trade.Venue);
            parameters[3] = new SqlParameter("@Quantity", trade.Quantity);
            parameters[4] = new SqlParameter("@Price", trade.Price);
            parameters[5] = new SqlParameter("@BuySell", trade.BuySell.ToUpper());

            return parameters;
        }

        public async Task CreateTradeAsync(Trade trade)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_Trade_Create", GetParameters(trade)); });
        }
    }
}
