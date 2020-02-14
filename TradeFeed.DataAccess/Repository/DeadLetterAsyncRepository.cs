using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public class DeadLetterApiRepository : AsyncRepository, IDeadLetterAsyncRepository
    {
        public DeadLetterApiRepository(string baseUrl) : base(baseUrl)
        {

        }

         public async Task<DeadLetter> GetDeadLetterAsync(int id)
        {
            return await GetAsync<DeadLetter>("/api/v1/deadLetter/byId/" + id.ToString());
        }

        public async Task<IEnumerable<DeadLetter>> GetAllDeadLettersAsync()
        {
            return await GetAsync<IEnumerable<DeadLetter>>("/api/v1/deadLetter/all");
        }

        public async Task CreateDeadLetterAsync(DeadLetter deadLetter)
        {
            await PostAsync<DeadLetter>("/api/v1/deadLetter/create", deadLetter);
        }
    }
}
