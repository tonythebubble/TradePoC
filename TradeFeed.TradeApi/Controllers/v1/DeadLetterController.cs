using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TradeFeed.DataAccess.Model;
using TradeFeed.TradeApi.Contracts.v1;
using TradeFeed.TradeApi.Repository;

namespace TradeFeed.DeadLetterApi.Controllers.v1
{
    [ApiController]
    public class DeadLetterController : Controller
    {
        public IDeadLetterRepository Repository { get; }

        public DeadLetterController(IDeadLetterRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.DeadLetter.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            List<DeadLetter> deadLetters = (List<DeadLetter>)await Repository.GetAllDeadLettersAsync();

            return Ok(deadLetters);
        }

        [HttpGet(ApiRoutes.DeadLetter.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            DeadLetter deadLetter = await Repository.GetDeadLetterAsync(id);
            
            return Ok(deadLetter);
        }


        [HttpPost(ApiRoutes.DeadLetter.Create)]
        public async Task<IActionResult> Create([FromBody]DeadLetter obj)
        {
            await Repository.CreateDeadLetterAsync(obj);

            return Ok(obj);
        }
    }
}