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
    public class DeadLetterController : Controller
    {
        public IRepository Repository { get; }

        public DeadLetterController(IRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.DeadLetter.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<DeadLetter> deadLetters = await Repository.DeadLetterGetAllAsync();

            return Ok(deadLetters);
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [HttpGet(ApiRoutes.DeadLetter.GetById)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            DeadLetter deadLetter = await Repository.DeadLetterGetAsync(id);
            return Ok(deadLetter);
        }
    }
}