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

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName(),
                        City = x.City.GetDisplayName(),
                        Campus = x.Campus.GetDisplayName()
                    });

                return new BaseResponse<IEnumerable<UserViewModel>>()
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

        public async Task<IBaseResponse<ProductViewModel>> GetUser(long id)
        {
            try
            {
                var product = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ProductViewModel()
                {
                    Description = product.Name,
                    Category = product.City.GetDisplayName()
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
