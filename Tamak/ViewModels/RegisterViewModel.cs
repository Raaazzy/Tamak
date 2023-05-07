using System.ComponentModel.DataAnnotations;

namespace Tamak.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Укажите корпус, в котором располагается ваша организация")]
        public string Campus { get; set; }

        public string Role { get; set; }
    }
}
