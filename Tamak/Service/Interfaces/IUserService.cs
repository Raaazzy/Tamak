using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.ViewModels;

namespace Tamak.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        public Task<IBaseResponse<UserViewModel>> GetUser(string id);

        public Task<BaseResponse<User>> Save(UserViewModel model);
    }
}
