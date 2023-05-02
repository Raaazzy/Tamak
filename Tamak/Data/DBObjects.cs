using Tamak.Data.Models;

namespace Tamak.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {
            if (!content.Categories.Any())
            {
                content.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!content.Products.Any())
            {
                content.AddRange(
                    new Product
                    {
                        Name = "Латте",
                        Description = "Свежий кофе с молочным оттенком",
                        Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 200,
                        IsFavourite = true,
                        Available = true,
                        Category = Categories["Кофе"]
                    },
                    new Product
                    {
                        Name = "Капучино",
                        Description = "Свежий кофе с сильным молочным оттенком",
                        Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 190,
                        IsFavourite = false,
                        Available = true,
                        Category = Categories["Кофе"]
                    },
                    new Product
                    {
                        Name = "Американо",
                        Description = "Свежий кофе без молока",
                        Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 150,
                        IsFavourite = false,
                        Available = true,
                        Category = Categories["Кофе"]
                    },
                    new Product
                    {
                        Name = "Какао",
                        Description = "какао",
                        Img = "https://dodopizza-a.akamaihd.net/static/Img/Products/d45790074f574ccfa9c75884dfe55f09_584x584.webp",
                        Price = 100,
                        IsFavourite = false,
                        Available = true,
                        Category = Categories["Какао"]
                    }
                );
            }

            content.SaveChanges();
        }

        private static Dictionary<string, Category> сategoryList;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (сategoryList == null) {
                    var list = new Category[] {
                        new Category { Name = "Кофе" },
                        new Category { Name = "Какао" }
                    };

                    сategoryList = new Dictionary<string, Category>();
                    foreach (Category element in list)
                    {
                        сategoryList.Add(element.Name, element);
                    }
                }

                return сategoryList;
            }
        }
    }
}
