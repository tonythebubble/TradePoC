using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TradeFeed.DataAccess;
using TradeFeed.DataAccess.Model;
using TradeFeed.RestApi.Contracts.v1;

namespace TradeFeed.RestApi.Controllers.v1
{
    [ApiController]
    public class TradeController : Controller
    {
        public IRepository Repository { get; }

        public TradeController(IRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Trade.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Trade> trades = await Repository.TradeGetAllAsync();

            return Ok(trades);
        }

        [HttpGet(ApiRoutes.Trade.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Trade trade = await Repository.TradeGetAsync(id);
            return Ok(trade);
        }

        [HttpGet(ApiRoutes.Trade.GetByClientId)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByClientIdAsync([FromRoute]Int32 id)
        {
            IEnumerable<Trade> trades = await Repository.TradeGetAllAsync();
            return Ok(trades.Where(t => t.ClientId == id));
        }

        [HttpGet(ApiRoutes.Trade.GetByStockId)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByStockIdAsync([FromRoute]Int32 id)
        {
            IEnumerable<Trade> trades = await Repository.TradeGetAllAsync();
            return Ok(trades.Where(t => t.StockId == id));
        }
    }
}