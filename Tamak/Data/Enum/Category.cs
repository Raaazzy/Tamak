using System.ComponentModel.DataAnnotations;

namespace Tamak.Data.Enum
{
    public enum Category
    {
        [Display(Name = "Кофе")]
        Coffee = 0,
        [Display(Name = "Какао")]
        Kakao = 1,
    }
}
