using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _profileRepository;

        public ProfileService(IBaseRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IBaseResponse<Profile>> Get(string userName)
        {
            try
            {
                var car = await _profileRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.User.Name == userName);
                if (car == null)
                {
                    return new BaseResponse<Profile>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.Success,
                    Data = car
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    Description = $"[Get] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public Task<IBaseResponse<Profile>> Create(ProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<Product>> Edit(long id, ProfileViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
