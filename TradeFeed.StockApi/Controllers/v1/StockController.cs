using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TradeFeed.DataAccess.Model;
using TradeFeed.StockApi.Contracts.v1;
using TradeFeed.StockApi.Repository;

namespace TradeFeed.StockApi.Controllers.v1
{
    [ApiController]
    public class StockController : Controller
    {
        public IStockRepository Repository { get; }

        public StockController(IStockRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Stock.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            List<Stock> stocks = (List<Stock>)await Repository.GetAllStocksAsync();

            return Ok(stocks);
        }

        [HttpGet(ApiRoutes.Stock.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Stock stock = await Repository.GetStockAsync(id);
            
            return Ok(stock);
        }

        [HttpPut(ApiRoutes.Stock.Update)]
        public async Task<IActionResult> Update([FromRoute]Int32 id, [FromBody]Stock obj)
        {
            Stock stock = await Repository.GetStockAsync(id);
            if (stock.Id == 0)
                return NotFound();

            await Repository.UpdateStockAsync(obj);

            return Ok(stock);
        }

        [HttpPut(ApiRoutes.Stock.Create)]
        public async Task<IActionResult> Create([FromBody]Stock obj)
        {
            Stock stock = await Repository.GetStockAsync(obj.Id);

            if (stock.Id > 0)
                return StatusCode(302);

            await Repository.CreateStockAsync(obj);

            return Ok(obj);
        }
    }
}