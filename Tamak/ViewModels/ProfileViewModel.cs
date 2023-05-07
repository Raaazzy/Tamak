using System.ComponentModel.DataAnnotations;

namespace Tamak.ViewModels
{
    public class ProfileViewModel
    {
        public long Id { get; set; }


        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите корпус")]
        public string Campus { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        public string Email { get; set; }
    }
}
