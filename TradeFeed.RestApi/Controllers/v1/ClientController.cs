using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TradeFeed.DataAccess;
using TradeFeed.DataAccess.Model;
using TradeFeed.RestApi.Contracts.v1;

namespace TradeFeed.RestApi.Controllers.v1
{
    [ApiController]
    public class ClientController : Controller
    {
        public IRepository Repository { get; }

        public ClientController(IRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Client.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Client> clients = await Repository.ClientGetAllAsync();

            return Ok(clients);
        }

        [HttpGet(ApiRoutes.Client.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Client client = await Repository.ClientGetAsync(id);
            return Ok(client);
        }
    }
}