using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public interface IStockAsyncRepository
    {
        Task<Stock> GetStockAsync(int id);

        Task<IEnumerable<Stock>> GetAllStocksAsync();
       
        Task CreateStockAsync(Stock stock);
        
        Task UpdateStockAsync(Stock stock);
    }
}
