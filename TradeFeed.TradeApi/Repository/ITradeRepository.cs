using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.TradeApi.Repository
{
    public interface ITradeRepository
    {
        Task<Trade> GetTradeAsync(int id);

        Trade GetTrade(int Id);

        Task<IEnumerable<Trade>> GetAllTradesAsync();

        IEnumerable<Trade> GetAllTrades();
        
        Task CreateTradeAsync(Trade trade);
    }
}
