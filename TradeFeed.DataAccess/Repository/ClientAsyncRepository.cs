using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public class ClientApiRepository : AsyncRepository, IClientAsyncRepository
    {
        public ClientApiRepository(string baseUrl) : base(baseUrl)
        {

        }

         public async Task<Client> GetClientAsync(int id)
        {
            return await GetAsync<Client>("/api/v1/client/byId/" + id.ToString());
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await GetAsync<IEnumerable<Client>>("/api/v1/client/all");
        }

        public async Task CreateClientAsync(Client client)
        {
            await PutAsync<Client>("/api/v1/client/create", client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await PutAsync<Client>("/api/v1/client/update/" + client.Id.ToString(), client);
        }
    }
}
