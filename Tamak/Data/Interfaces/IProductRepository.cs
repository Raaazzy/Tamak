using Tamak.Data.Models;

namespace Tamak.Data.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        Task<Product> GetByName(string name);
    }
}
