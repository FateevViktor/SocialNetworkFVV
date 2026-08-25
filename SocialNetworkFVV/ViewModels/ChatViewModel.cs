using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.ViewModels
{
    public class ChatViewModel
    {
        public User You { get; set; } = new User();

        public User ToWhom { get; set; } = new User();

        public List<Message> History { get; set; } = new List<Message>();

        public MessageViewModel NewMessage { get; set; } = new MessageViewModel();

        public ChatViewModel()
        {
            NewMessage = new MessageViewModel();
        }
    }
}
