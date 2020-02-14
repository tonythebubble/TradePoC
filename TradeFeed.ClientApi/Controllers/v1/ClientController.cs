using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TradeFeed.DataAccess.Model;
using TradeFeed.ClientApi.Contracts.v1;
using TradeFeed.ClientApi.Repository;

namespace TradeFeed.ClientApi.Controllers.v1
{
    [ApiController]
    public class ClientController : Controller
    {
        public IClientRepository Repository { get; }

        public ClientController(IClientRepository repository)
        {
            Repository = repository;
        }


        [HttpGet(ApiRoutes.Client.Get)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetAll()
        {
            List<Client> clients = (List<Client>)await Repository.GetAllClientsAsync();

            return Ok(clients);
        }

        [HttpGet(ApiRoutes.Client.GetById)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> GetByIdAsync([FromRoute]Int32 id)
        {
            Client client = await Repository.GetClientAsync(id);
            
            return Ok(client);
        }

        [HttpPut(ApiRoutes.Client.Update)]
        public async Task<IActionResult> Update([FromRoute]Int32 id, [FromBody]Client obj)
        {
            Client client = await Repository.GetClientAsync(id);
            if (client.Id == 0)
                return NotFound();

            await Repository.UpdateClientAsync(obj);

            return Ok(obj);
        }

        [HttpPut(ApiRoutes.Client.Create)]
        public async Task<IActionResult> Create([FromBody]Client obj)
        {
            Client client = await Repository.GetClientAsync(obj.Id);

            if (client.Id > 0)
                return StatusCode(302);

            await Repository.CreateClientAsync(obj);

            return Ok(obj);
        }
    }
}