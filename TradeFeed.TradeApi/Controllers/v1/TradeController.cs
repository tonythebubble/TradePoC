using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TradeFeed.DataAccess.Model;
using TradeFeed.TradeApi.Contracts.v1;
using TradeFeed.TradeApi.Repository;

namespace TradeFeed.TradeApi.Controllers.v1
{
    [ApiController]
    public class TradeController : Controller
    {
        public ITradeRepository Repository { get; }

        public TradeController(ITradeRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Trade.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            List<Trade> trades = (List<Trade>)await Repository.GetAllTradesAsync();

            return Ok(trades);
        }

        [HttpGet(ApiRoutes.Trade.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Trade trade = await Repository.GetTradeAsync(id);
            
            return Ok(trade);
        }


        [HttpPost(ApiRoutes.Trade.Create)]
        public async Task<IActionResult> Create([FromBody]Trade obj)
        {
            try
            {
                await Repository.CreateTradeAsync(obj);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(obj);
        }
    }
}