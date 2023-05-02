using Tamak.Data.Models;
using Tamak.Data.Response;

namespace Tamak.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetProducts();
    }
}
