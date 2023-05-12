using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity.Core.Mapping;
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
        private readonly IBaseRepository<Assortiment> _assortimentRepository;
        private readonly IBaseRepository<Time> _timeRepository;

        public AssortimentService(IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository, IBaseRepository<Assortiment> assortimentRepository, IBaseRepository<Time> timeRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _assortimentRepository = assortimentRepository;
            _timeRepository = timeRepository;
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

        public IBaseResponse<List<Assortiment>> GetAssortiments()
        {
            try
            {
                var assortiments = _assortimentRepository.GetAll().ToList();
                if (!assortiments.Any())
                {
                    return new BaseResponse<List<Assortiment>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.Success
                    };
                }

                return new BaseResponse<List<Assortiment>>()
                {
                    Data = assortiments,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Assortiment>>()
                {
                    Description = $"[GetAssortiments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Time>> GetTimes()
        {
            try
            {
                var times = _timeRepository.GetAll().ToList();
                if (!times.Any())
                {
                    return new BaseResponse<List<Time>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.Success
                    };
                }

                return new BaseResponse<List<Time>>()
                {
                    Data = times,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Time>>()
                {
                    Description = $"[GetTimes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> Add(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                if (user.Assortiment == null)
                {
                    var assort = new Assortiment
                    {
                        User = user,
                        UserId = user.Id,
                        Products = new List<Product>(),
                        Times = new List<Time>()
                    };
                    
                    await _assortimentRepository.Create(assort);
                    user.Assortiment = assort;

                    for (int i = 9; i < 18; i++)
                    {
                        for (int j = 0; j < 60; j += 15)
                        {
                            string k = j % 60 == 0 ? "00" : $"{j % 60}";
                            var time = new Time()
                            {
                                StringData = $"{i}:{k}",
                                NumData = i * 100 + (j % 60),
                                Avaliable = true,
                                AssortimentId = user.Id,
                                Assortiment = assort
                            };
                            await _timeRepository.Create(time);
                            assort.Times.Add(time);
                        }
                    }
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
