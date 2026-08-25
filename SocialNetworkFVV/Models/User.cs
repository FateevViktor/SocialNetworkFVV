using Microsoft.AspNetCore.Identity;

namespace SocialNetworkFVV.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string MiddleName { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }

        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }
        public List<Friend> Friends { get; set; } = new List<Friend>();
        public string GetFullName()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }

        public User()
        {
            Image = "https://via.placeholder.com/500";
            Status = "Ура! Я в соцсети!";
            About = "Информация обо мне.";
        }
    }
}
