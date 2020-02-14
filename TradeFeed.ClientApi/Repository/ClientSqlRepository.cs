using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;
using TradeFeed.DataAccess.DbContext;
using System.Data.SqlClient;

namespace TradeFeed.ClientApi.Repository
{
    public class ClientSqlRepository : IClientRepository
    {
        protected readonly IDbContext _Context;

        public ClientSqlRepository(IDbContext context)
        {
            _Context = context;
        }

        public async Task<Client> GetClientAsync(int id)
        {
            Client client = new Client();
            await Task.Run(() => { client = GetClient(id); });

            return client;
        }

        public Client GetClient(int id)
        {
            Client client = new Client();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", id);

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Client_Get", parameters))
            {
                while (reader.Read())
                {
                    client = PopulateClient(reader);
                }
                reader.Close();
            }

            return client;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            IEnumerable<Client> clients = new List<Client>();
            await Task.Run(() => { clients = GetAllClients(); });

            return clients;
        }

        public IEnumerable<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            using (SqlDataReader reader = _Context.ReturnDataReader("proc_Client_Get"))
            {
                while (reader.Read())
                {
                    clients.Add(PopulateClient(reader));
                }
                reader.Close();
            }

            return clients;
        }

        private Client PopulateClient(IDataRecord record)
        {
            return new Client()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                Name = record["Name"].ToString()
            };
        }

        private SqlParameter[] GetParameters(Client client)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Id", client.Id);
            parameters[1] = new SqlParameter("@Name", client.Name);

            return parameters;
        }

        public async Task CreateClientAsync(Client client)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_Client_Create", GetParameters(client)); });
            
        }


        public async Task UpdateClientAsync (Client client)
        {
            await Task.Run(() => { _Context.ExecuteProc("proc_Client_Update", GetParameters(client)); });
            
        }
    }
}
