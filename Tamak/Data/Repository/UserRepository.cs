using Tamak.Data.Interfaces;
using Tamak.Data.Models;

namespace Tamak.Data.Repository
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly AppDBContent _database;

        public UserRepository(AppDBContent db)
        {
            _database = db;
        }

        public IQueryable<User> GetAll()
        {
            return _database.Users;
        }

        public async Task Delete(User entity)
        {
            _database.Users.Remove(entity);
            await _database.SaveChangesAsync();
        }

        public async Task Create(User entity)
        {
            await _database.Users.AddAsync(entity);
            await _database.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _database.Users.Update(entity);
            await _database.SaveChangesAsync();

            return entity;
        }
    }
}
