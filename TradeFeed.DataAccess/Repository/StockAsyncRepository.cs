using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public class StockApiRepository : AsyncRepository, IStockAsyncRepository
    {
        public StockApiRepository(string baseUrl) : base(baseUrl)
        {

        }

         public async Task<Stock> GetStockAsync(int id)
        {
            return await GetAsync<Stock>("/api/v1/stock/byId/" + id.ToString());
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return await GetAsync<IEnumerable<Stock>>("/api/v1/stock/all");
        }

        public async Task CreateStockAsync(Stock stock)
        {
            await PutAsync<Stock>("/api/v1/stock/create", stock);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            await PutAsync<Stock>("/api/v1/client/update/" + stock.Id.ToString(), stock);
        }
    }
}
