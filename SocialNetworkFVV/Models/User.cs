using Microsoft.AspNetCore.Identity;

namespace SocialNetworkFVV.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string MiddleName { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
    }
}
