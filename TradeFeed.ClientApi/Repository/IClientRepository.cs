using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.ClientApi.Repository
{
    public interface IClientRepository
    {
        Task<Client> GetClientAsync(int id);

        Client GetClient(int Id);

        Task<IEnumerable<Client>> GetAllClientsAsync();

        IEnumerable<Client> GetAllClients();
        
        Task CreateClientAsync(Client client);
        
        Task UpdateClientAsync(Client client);
    }
}
