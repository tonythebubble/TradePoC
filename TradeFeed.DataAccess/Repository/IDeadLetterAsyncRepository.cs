using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.DataAccess.Repository
{
    public interface IDeadLetterAsyncRepository
    {
        Task<DeadLetter> GetDeadLetterAsync(int id);

        Task<IEnumerable<DeadLetter>> GetAllDeadLettersAsync();
       
        Task CreateDeadLetterAsync(DeadLetter deadLetter);
    }
}
