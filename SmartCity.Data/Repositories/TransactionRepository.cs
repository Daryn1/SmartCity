using System.Linq;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class TransactionRepository : AsyncBaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SmartCityDbContext context) : base(context)
        {
        }

        public IQueryable<Transaction> GetUserTransactionsAsync(string userLogin)
        {
            return DbSet.Where(transaction =>
               transaction.Sender.Login == userLogin || transaction.Recipient.Login == userLogin);
        }
    }
}
