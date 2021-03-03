using System.Collections.Generic;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Services.Infrastructure;

namespace SmartCity.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(long transactionId);
        Task<List<Transaction>> GetUserTransactionsAsync(string userLogin);
        Task<OperationResult> CreateTransaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);
        Task<bool> TransactionExistsAsync(int id);
        Task DeleteTransactionAsync(int id);
    }
}