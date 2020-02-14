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
    public class StockController : Controller
    {
        public IRepository Repository { get; }

        public StockController(IRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Stock.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Stock> stocks = await Repository.StockGetAllAsync();

            return Ok(stocks);
        }

        [HttpGet(ApiRoutes.Stock.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Stock stock = await Repository.StockGetAsync(id);
            return Ok(stock);
        }
    }
}