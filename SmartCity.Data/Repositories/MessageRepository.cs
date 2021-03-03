using System.Linq;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;

namespace SmartCity.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(SmartCityDbContext context) : base(context)
        {
        }

        public IQueryable<Message> GetTwoUsersMessages(string senderLogin, string recipientLogin)
        {
            return dbSet.Where(message =>
                message.Sender.Login == senderLogin && message.Recipient.Login == recipientLogin ||
                message.Sender.Login == recipientLogin && message.Recipient.Login == senderLogin).AsQueryable();
        }

        public bool MessageWithTextExists(string messageText)
        {
            return dbSet.Any(message => message.Text == messageText);
        }
    }
}
