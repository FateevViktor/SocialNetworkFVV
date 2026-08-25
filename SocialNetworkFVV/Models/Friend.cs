using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetworkFVV.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        public bool FriendRequestAccepted { get; set; }
        public DateTime FriendRequestDateTime { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string? CurrentFriendId { get; set; }
        public User CurrentFriend { get; set; }
    }
}
