using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public string RecipientId { get; set; }
        public User Recipient { get; set; }
    }
}
