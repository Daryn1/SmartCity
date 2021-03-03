using System.Collections.Generic;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Services.Infrastructure;

namespace SmartCity.Services.Interfaces
{
    public interface IMessengerService
    {
        OperationResult SendMessage(string senderLogin, string recipientLogin, string textMessage);
        void Save(Message message);
        Message GetLastUsersMessage(string senderLogin, string recipientLogin);
        List<Message> GetLastUsersMessages(string senderLogin, string recipientLogin);
    }
}