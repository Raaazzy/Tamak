using System.ComponentModel.DataAnnotations;

namespace Tamak.Data.Enum
{
    public enum City
    {
        [Display(Name = "Москва")]
        Moscow = 0,
        [Display(Name = "Санкт-Петербург")]
        Spb = 1,
        [Display(Name = "Нижний Новгород")]
        Nn = 2,
        [Display(Name = "Пермь")]
        Perm = 3,
    }
}
