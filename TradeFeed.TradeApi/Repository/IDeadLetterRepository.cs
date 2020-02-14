using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeFeed.DataAccess.Model;

namespace TradeFeed.TradeApi.Repository
{
    public interface IDeadLetterRepository
    {
        Task<DeadLetter> GetDeadLetterAsync(int id);

        DeadLetter GetDeadLetter(int Id);

        Task<IEnumerable<DeadLetter>> GetAllDeadLettersAsync();

        IEnumerable<DeadLetter> GetAllDeadLetters();
        
        Task CreateDeadLetterAsync(DeadLetter deadLetter);
       
    }
}
