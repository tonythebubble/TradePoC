using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public interface ITradeAsyncRepository
    {
        Task<Trade> GetTradeAsync(int id);

        Task<IEnumerable<Trade>> GetAllTradesAsync();
       
        Task CreateTradeAsync(Trade trade);
    }
}
