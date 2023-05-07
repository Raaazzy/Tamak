using System.ComponentModel.DataAnnotations;

namespace Tamak.Data.Enum
{
    public enum Campus
    {
        [Display(Name = "Покровский бульвар")]
        Pokrovka = 0,
        [Display(Name = "Шаболовка")]
        Shabolovka = 1,
        [Display(Name = "Строгино")]
        Strogino = 2,

        [Display(Name = "Ничего")]
        None = 3
    }
}
