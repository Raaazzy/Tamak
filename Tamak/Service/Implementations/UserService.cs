using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Extensions;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Repository;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll()
                    .Select(x => new User()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Name = x.Name,
                        Role = x.Role,
                        City = x.City,
                        Campus = x.Campus
                    });

                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = users,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IBaseResponse<UserViewModel>> GetUser(string email)
        {
            try
            {
                var product = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == email);
                if (product == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new UserViewModel()
                {
                    Email = product.Email,
                    Name = product.Name,
                    City = product.City.GetDisplayName(),
                    Campus = product.Campus.GetDisplayName(),

                };

                return new BaseResponse<UserViewModel>()
                {
                    StatusCode = StatusCode.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<User>> Save(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Email == model.Email);

                user.Name = model.Name;
                user.City = (City)Enum.Parse(typeof(City), model.City);
                if (model.Campus != null)
                {
                    user.Campus = (Campus)Enum.Parse(typeof(Campus), model.Campus);
                }

                await _userRepository.Update(user);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
