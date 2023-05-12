using Automarket.Service.Implementations;
using Azure;
using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Extensions;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Assortiment> _assortimentRepository;
        private readonly IBaseRepository<Order> _orderRepository;

        public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository, IBaseRepository<Assortiment> assortimentRepository, IBaseRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _assortimentRepository = assortimentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Basket?.Orders;
                var response = from p in orders
                               join c in _productRepository.GetAll() on p.ProductId equals c.Id
                               join d in _assortimentRepository.GetAll() on c.AssortimentId equals d.Id
                               join u in _userRepository.GetAll() on d.UserId equals u.Id
                               select new OrderViewModel()
                               {
                                   Id = p.Id,
                                   ProductName = c.Name,
                                   OrderDate = p.OrderDate,
                                   Shop = u.Name,
                                   City = u.City.GetDisplayName(),
                                   Campus = u.Campus.GetDisplayName(),
                                   Status = p.Status.GetDisplayName()
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Basket?.Orders.Where(x => x.Id == id).ToList();
                if (orders == null || orders.Count == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Заказов нет",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                var response = (from p in orders
                                join c in _productRepository.GetAll() on p.ProductId equals c.Id
                                join d in _assortimentRepository.GetAll() on c.AssortimentId equals d.Id
                                join u in _userRepository.GetAll() on d.UserId equals u.Id
                                select new OrderViewModel()
                                {
                                    Id = p.Id,
                                    ProductName = c.Name,
                                    OrderDate = p.OrderDate,
                                    Shop = u.Name,
                                    City = u.City.GetDisplayName(),
                                    Campus = u.Campus.GetDisplayName(),
                                    Status = p.Status.GetDisplayName()
                                }).FirstOrDefault();

                return new BaseResponse<OrderViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetShopItems(string userName)
        {
            try
            {

                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Email == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var response = from p in _orderRepository.GetAll() where p.ShopEmail == userName
                               join c in _productRepository.GetAll() on p.ProductId equals c.Id
                               select new OrderViewModel()
                               {
                                   Id = p.Id,
                                   ProductName = c.Name,
                                   OrderDate = p.OrderDate,
                                   Status = p.Status.GetDisplayName()
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
