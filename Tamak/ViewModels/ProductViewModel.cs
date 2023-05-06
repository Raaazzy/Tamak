using System.ComponentModel.DataAnnotations;
using Tamak.Data.Models;

namespace Tamak.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }


        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название продукта")]
        [MinLength(1, ErrorMessage = "Минимальная длина должна быть больше одного символа")]
        public string Name { get; set; }


        [Display(Name = "Описание")]
        [MinLength(1, ErrorMessage = "Минимальная длина должна быть больше одного символа")]
        public string Description { get; set; }


        [Display(Name = "Категория продукта")]
        [Required(ErrorMessage = "Выберите категорию")]
        public string Category { set; get; }


        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; }


        public byte[]? Img { get; set; }
        public bool Available { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
