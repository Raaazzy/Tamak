using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.ViewModels;

namespace Tamak.Service.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<Profile>> Get(string userName);

        Task<IBaseResponse<Profile>> Create(ProfileViewModel model);

        Task<IBaseResponse<Product>> Edit(long id, ProfileViewModel model);
    }
}
