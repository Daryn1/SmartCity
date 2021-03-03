using System.Collections.Generic;
using System.Linq;
using SmartCity.Data.Entities.UserAccount;

namespace SmartCity.Data.Interfaces
{
    public interface IMessageRepository
    {
        IQueryable<Message> GetTwoUsersMessages(string senderLogin, string recipientLogin);
        bool MessageWithTextExists(string messageText);
        Message Get(long id);
        List<Message> GetAll();
        void Save(Message model);
        void Delete(long id);
    }
}