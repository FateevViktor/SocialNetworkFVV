using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.Data.Repos
{
    public interface IMessageRepository
    {
        List<Message> GetMessages(User sender, User recipient); //Получить переписку
        Message? GetMessages(int id); //Получить сообщение
        int AddMessage(Message item); //Добавить сообщение
        void DeleteMessage(Message message); //Удалить сообщение
    }
}
