using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class TimeRepository : IBaseRepository<Time>
    {
        private readonly AppDBContent _db;

        public TimeRepository(AppDBContent db)
        {
            _db = db;
        }

        public async Task Create(Time entity)
         {
            await _db.Times.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Time> GetAll()
        {
            return _db.Times;
        }

        public async Task Delete(Time entity)
        {
            _db.Times.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Time> Update(Time entity)
        {
            _db.Times.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
