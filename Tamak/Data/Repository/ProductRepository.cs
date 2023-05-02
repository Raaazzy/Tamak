using Microsoft.EntityFrameworkCore;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContent appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public async Task<bool> Create(Product entity)
        {
            await appDBContent.Products.AddAsync(entity);
            await appDBContent.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Task<Product> entity)
        {
            appDBContent.Remove(entity);
            await appDBContent.SaveChangesAsync();

            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await appDBContent.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await appDBContent.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<Product>> Select()
        {
            return await appDBContent.Products.ToListAsync();
        }
    }
}
