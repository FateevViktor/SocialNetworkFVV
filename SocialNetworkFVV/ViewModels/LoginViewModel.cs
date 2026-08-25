using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFVV.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; } = false;

        //[Required]
        //[Display(Name = "Никнейм")]
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
