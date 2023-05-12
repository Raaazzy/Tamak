using System.ComponentModel.DataAnnotations;

namespace Tamak.Data.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "Принят в работу")]
        Working = 0,
        [Display(Name = "Готов")]
        Done = 1,

        [Display(Name = "В обработке")]
        Process = 2
    }
}
