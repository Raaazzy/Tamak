using System.ComponentModel.DataAnnotations;

namespace Tamak.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
