using System.Data.Entity;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class ProductRepository : IAllProducts
    {
        private readonly AppDBContent appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Product> Products => appDBContent.Products.Include(c => c.Category);
        public IEnumerable<Product> getFavouriteProducts => appDBContent.Products.Where(p => p.IsFavourite).Include(c => c.Category);

        public Product getObjectProdust(int productId) => appDBContent.Products.FirstOrDefault(p => p.Id == productId);
    }
}
