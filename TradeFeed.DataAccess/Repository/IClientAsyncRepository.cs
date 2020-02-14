using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public interface IClientAsyncRepository
    {
        Task<Client> GetClientAsync(int id);

        Task<IEnumerable<Client>> GetAllClientsAsync();
       
        Task CreateClientAsync(Client client);
        
        Task UpdateClientAsync(Client client);
    }
}
