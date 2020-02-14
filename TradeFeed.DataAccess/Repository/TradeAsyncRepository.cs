using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public class TradeApiRepository : AsyncRepository, ITradeAsyncRepository
    {
        public TradeApiRepository(string baseUrl) : base(baseUrl)
        {

        }

         public async Task<Trade> GetTradeAsync(int id)
        {
            return await GetAsync<Trade>("/api/v1/trade/byId/" + id.ToString());
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await GetAsync<IEnumerable<Trade>>("/api/v1/trade/all");
        }

        public async Task CreateTradeAsync(Trade trade)
        {
            await PostAsync<Trade>("/api/v1/trade/create", trade);
        }
    }
}
