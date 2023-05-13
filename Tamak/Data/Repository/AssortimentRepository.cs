using Microsoft.EntityFrameworkCore;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class AssortimentRepository : IBaseRepository<Assortiment>
    {
        private readonly AppDBContent _db;

        public AssortimentRepository(AppDBContent dbContext)
        {
            _db = dbContext;
        }

        public async Task Create(Assortiment entity)
        {
            await _db.Assortements.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Assortiment> GetAll()
        {
            return _db.Assortements;
        }

        public async Task Delete(Assortiment entity)
        {
            _db.Assortements.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Assortiment> Update(Assortiment entity)
        {
            _db.Assortements.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }   
}
