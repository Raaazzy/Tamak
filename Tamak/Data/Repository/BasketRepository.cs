using Tamak.Data;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly AppDBContent _db;

        public BasketRepository(AppDBContent dbContext)
        {
            _db = dbContext;
        }

        public async Task Create(Basket entity)
        {
            await _db.Baskets.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAll()
        {
            return _db.Baskets;
        }

        public async Task Delete(Basket entity)
        {
            _db.Baskets.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Basket> Update(Basket entity)
        {
            _db.Baskets.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}