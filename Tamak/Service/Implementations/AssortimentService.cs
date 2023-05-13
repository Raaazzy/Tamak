using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class AssortimentService : IAssortimentService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Product> _productRepository;

        public AssortimentService(IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetItems(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Assortiment)
                    .ThenInclude(x => x.Products)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<Product>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var products = user.Assortiment?.Products;
                
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetItem(string userName, long id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Assortiment)
                    .ThenInclude(x => x.Products)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Assortiment.Products.Where(x => x.Id == id).ToList();
                if (orders == null || orders.Count == 0)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Заказов нет",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                var response = (from p in orders
                                join c in _productRepository.GetAll() on p.Id equals c.Id
                                select new Product()
                                {
                                    Id = p.Id,
                                    Name = c.Name,
                                    Price = c.Price,
                                    Description = c.Description,
                                    Available = c.Available,
                                }).FirstOrDefault();

                return new BaseResponse<Product>()
                {
                    Data = response,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> Add(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Assortiment)
                    .ThenInclude(x => x.Products)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var product = new Product()
                {
                    Name = "кофе",
                    Description = "кофе",
                    Price = 0,
                    Available = true,
                    AssortimentId = user.Assortiment.Id,
                    Assortiment = user.Assortiment,
                    Category = 0,
                    CategoryId = 0,
                };
                var productRep = _productRepository.Create(product);
                user.Assortiment.Products.Add(product);

                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
