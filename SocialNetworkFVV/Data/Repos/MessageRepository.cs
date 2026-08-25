using Microsoft.EntityFrameworkCore;
using SocialNetworkFVV.Models;
using System.Collections.Generic;

namespace SocialNetworkFVV.Data.Repos
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext _db;
        public MessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Message> GetMessages(User sender, User recipient)
        {
            List<Message> messagesFrom = new List<Message>();
            List<Message> messagesTo = new List<Message>();
            List<Message> itog = new List<Message>();

            messagesFrom = _db.Messages.Include(x => x.Sender).Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
            messagesTo = _db.Messages.Include(x => x.Recipient).Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();
            itog.AddRange(messagesFrom);
            itog.AddRange(messagesTo);
            itog.OrderBy(x => x.MessageId);

            return itog;
        }
        //Получить сообщение по Id его
        public Message? GetMessages(int id)
        {
            Message? message = _db.Messages.FirstOrDefault(x => x.MessageId == id);

            return message;
        }

        public int AddMessage(Message item) //Добавить сообщение
        {
            _db.Messages.Add(item);
            _db.SaveChanges();
            return item.MessageId;
        }

        //Удалим сообщение
        public void DeleteMessage(Message message)
        {
            if (message != null)
            {
                _db.Messages.Remove(message);
                _db.SaveChanges();
            }
        }
    }
}
