using Tamak.Data.Enum;
using Tamak.Data.Models;

namespace Tamak.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {
            if (!content.Products.Any())
            {
                content.AddRange(
                    new Product
                    {
                        Name = "Латте",
                        Description = "Свежий кофе с молочным оттенком",
                        //Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 200,
                        Available = true,
                        Category = Category.Coffee
                    },
                    new Product
                    {
                        Name = "Капучино",
                        Description = "Свежий кофе с сильным молочным оттенком",
                        //Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 190,
                        Available = true,
                        Category = Category.Coffee
                    },
                    new Product
                    {
                        Name = "Американо",
                        Description = "Свежий кофе без молока",
                        //Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 150,
                        Available = true,
                        Category = Category.Coffee
                    },
                    new Product
                    {
                        Name = "Какао",
                        Description = "какао",
                        //Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 100,
                        Available = true,
                        Category = Category.Kakao
                    }
                );
            }

            content.SaveChanges();
        }
    }
}
