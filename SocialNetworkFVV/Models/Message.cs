namespace SocialNetworkFVV.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime MessageDateTime { get; set; }

        public string? SenderId { get; set; }
        public User Sender { get; set; }

        public string? RecipientId { get; set; }
        public User Recipient { get; set; }
    }
}
