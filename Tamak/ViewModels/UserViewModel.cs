using System.ComponentModel.DataAnnotations;
using Tamak.Data.Enum;

namespace Tamak.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "Корпус")]
        public string Campus { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }
    }
}
