using Microsoft.EntityFrameworkCore;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly AppDBContent appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public async Task Create(Product entity)
        {
            await appDBContent.Products.AddAsync(entity);
            await appDBContent.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
            appDBContent.Products.Remove(entity);
            await appDBContent.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return appDBContent.Products;
        }

        public async Task<Product> Update(Product entity)
        {
            appDBContent.Products.Update(entity);
            await appDBContent.SaveChangesAsync();
            return entity;
        }
    }
}
