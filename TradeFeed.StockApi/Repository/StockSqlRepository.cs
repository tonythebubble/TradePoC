using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;
using TradeFeed.DataAccess.DbContext;
using System.Data.SqlClient;

namespace TradeFeed.StockApi.Repository
{
    public class StockSqlRepository : IStockRepository
    {
        protected readonly IDbContext _Context;

        public StockSqlRepository(IDbContext context)
        {
            _Context = context;
        }

        public async Task<Stock> GetStockAsync(int id)
        {
            Stock stock = new Stock();
            await Task.Run(() => { stock = GetStock(id); });

            return stock;
        }

        public Stock GetStock(int id)
        {
            Stock stock = new Stock();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", id);

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Stock_Get", parameters))
            {
                while (reader.Read())
                {
                    stock = PopulateStock(reader);
                }
                reader.Close();
            }

            return stock;
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            IEnumerable<Stock> stocks = new List<Stock>();
            await Task.Run(() => { stocks = GetAllStocks(); });

            return stocks;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            List<Stock> stocks = new List<Stock>();

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Stock_Get"))
            {
                while (reader.Read())
                {
                    stocks.Add(PopulateStock(reader));
                }
                reader.Close();
            }

            return stocks;
        }

        private Stock PopulateStock(IDataRecord record)
        {
            return new Stock()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                Name = record["Name"].ToString(),
                Type = record["Type"].ToString()
            };
        }

        private SqlParameter[] GetParameters(Stock stock)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Id", stock.Id);
            parameters[1] = new SqlParameter("@Name", stock.Name);
            parameters[2] = new SqlParameter("@Type", stock.Type);

            return parameters;
        }

        public async Task CreateStockAsync(Stock stock)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_Stock_Create", GetParameters(stock)); });
            
        }


        public async Task UpdateStockAsync (Stock stock)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_Stock_Update", GetParameters(stock)); });
            
        }
    }
}
