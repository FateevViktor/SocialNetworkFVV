using SocialNetworkFVV.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFVV.ViewModels
{
    public class MyPageViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Вам необходимо ввести дату рождения")]
        [DataType(DataType.Date)] //Указываем, что нам нужна только дата
        public DateTime BirthdayDate { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Картинка")]
        public string Image { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Информация обо мне")]
        public string About { get; set; } = string.Empty;
        public List<User> Friends { get; set; } = new List<User>();
    }
}
