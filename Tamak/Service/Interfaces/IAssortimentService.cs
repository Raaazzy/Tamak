using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.ViewModels;

namespace Tamak.Service.Interfaces
{
    public interface IAssortimentService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetItems(string userName);

        Task<IBaseResponse<Product>> GetItem(string userName, long id);

        public Task<IBaseResponse<Product>> Add(string userName);
    }
}
