using Tamak.Data.Response;
using Tamak.ViewModels;

namespace Tamak.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
    }
}
