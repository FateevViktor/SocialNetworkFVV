using SocialNetworkFVV.Models;

namespace SocialNetworkFVV.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel(User user)
        {
            User = user;
        }
    }
}
