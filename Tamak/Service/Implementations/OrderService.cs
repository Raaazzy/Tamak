using Automarket.Service.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Repository;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly IBaseRepository<Time> _timeRepository;
        private readonly IBaseRepository<Assortiment> _assortimentRepository;

        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Order> orderRepository, IBaseRepository<Basket> basketRepository, IBaseRepository<Time> timeRepository, IBaseRepository<Assortiment> assortimentRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _timeRepository = timeRepository;
            _assortimentRepository= assortimentRepository;
        }

        public async Task<IBaseResponse<Order>> Create(CreateOrderViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .FirstOrDefaultAsync(x => x.Email == model.Login);
                if (user == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                if (user.Basket == null)
                {
                    var basket = new Basket
                    {
                        User = user,
                        UserId = user.Id,
                        Orders = new List<Order>()
                    };

                    await _basketRepository.Create(basket);
                    user.Basket = basket;
                }

                var time = await _timeRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.OrderDate);
                time.Avaliable = false;
                await _timeRepository.Update(time);

                var order = new Order()
                {
                    OrderDate = time.StringData,
                    BasketId = user.Basket.Id,
                    ProductId = model.ProductId,
                    ShopEmail = model.Shop,
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), model.Status)
            };

                await _orderRepository.Create(order);
                user.Basket.Orders.Add(order);

                return new BaseResponse<Order>()
                {
                    Description = "Заказ создан",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var order = _orderRepository.GetAll()
                    .Select(x => x.Basket.Orders.FirstOrDefault(y => y.Id == id))
                    .FirstOrDefault();

                if (order == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.OrderNotFound,
                        Description = "Заказ не найден"
                    };
                }

                await _orderRepository.Delete(order);
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.Success,
                    Description = "Заказ удален"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Order>> ChangeStatus(long id)
        {
            try
            {
                var order = await _orderRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (order.Status == OrderStatus.Process)
                {
                    order.Status = OrderStatus.Working;
                } 
                else if (order.Status == OrderStatus.Working)
                {
                    order.Status = OrderStatus.Done;
                } 
                else
                {
                    var users = _userRepository.GetAll();
                    var assortiments = _assortimentRepository.GetAll();

                    foreach (var u in users)
                    {
                        if (u.Email == order.ShopEmail)
                        {
                            foreach (var a in assortiments)
                            {
                                if (a.UserId == u.Id)
                                {
                                    var time = await _timeRepository.GetAll().FirstOrDefaultAsync(x => x.AssortimentId == a.Id && x.StringData == order.OrderDate);
                                    time.Avaliable = true;
                                    await _timeRepository.Update(time);
                                    break;
                                }
                            }
                            break;
                        }
                    }


                    Delete(id);
                    return new BaseResponse<Order>()
                    {
                        Data = order,
                        Description = "Данные обновлены",
                        StatusCode = StatusCode.Success
                    };
                }

                await _orderRepository.Update(order);

                return new BaseResponse<Order>()
                {
                    Data = order,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
