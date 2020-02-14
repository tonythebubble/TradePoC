using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.StockApi.Repository
{
    public interface IStockRepository
    {
        Task<Stock> GetStockAsync(int id);

        Stock GetStock(int Id);

        Task<IEnumerable<Stock>> GetAllStocksAsync();

        IEnumerable<Stock> GetAllStocks();
        
        Task CreateStockAsync(Stock stock);
        
        Task UpdateStockAsync(Stock stock);
    }
}
