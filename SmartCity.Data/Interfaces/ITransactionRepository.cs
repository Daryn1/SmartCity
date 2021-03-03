using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetUserTransactionsAsync(string userLogin);
        Task<Transaction> GetByIdAsync(long id);
        Task<List<Transaction>> GetAllAsync();
        Task SaveAsync(Transaction model);
        Task DeleteAsync(long id);
        Task<bool> Exists(long id);
    }
}