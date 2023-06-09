﻿using Automarket.Service.Implementations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Repository;
using Tamak.Service.Implementations;
using Tamak.Service.Interfaces;

namespace Tamak
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Product>, ProductRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Assortiment>, AssortimentRepository>();
            services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
            services.AddScoped<IBaseRepository<Time>, TimeRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssortimentService, AssortimentService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
