using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamak.Data.Enum;
using Tamak.Data.Extensions;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Repository;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Automarket.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public BaseResponse<Dictionary<long, string>> GetCategories()
        {
            try
            {
                var types = ((Category[])Enum.GetValues(typeof(Category)))
                    .ToDictionary(k => (long)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<long, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var products = await _productRepository.GetAll()
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Available= x.Available,
                        Category = x.Category.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = products;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductViewModel>> GetProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
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
                    Description = product.Description,
                    Category = product.Category.GetDisplayName(),
                    Img = product.Avatar,
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

        public async Task<IBaseResponse<Product>> Create(long assortimentId, Assortiment assortiment)
        {
            try
            {
                var product = new Product()
                {
                    Name = "кофе",
                    Description = "кофе",
                    Price = 0,
                    Available = true,
                    AssortimentId = assortimentId,
                    Assortiment = assortiment,
                    Category = 0,
                    CategoryId = 0,
                };
                await _productRepository.Create(product);

                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.Success,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _productRepository.Delete(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Description = model.Description;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Available = model.Available;

                await _productRepository.Update(product);


                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.Success,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Product>> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll().ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.Success
                    };
                }

                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Product>> Save(ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                product.Name = model.Name;
                /*product.Category = (Category)Enum.Parse(typeof(Category), model.Category);*/
                product.Description = model.Description;
                product.Price = model.Price;
                product.Available = model.Available;

                await _productRepository.Update(product);

                return new BaseResponse<Product>()
                {
                    Data = product,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Product>> ChangeAvaliable(ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                product.Available = !model.Available;

                await _productRepository.Update(product);

                return new BaseResponse<Product>()
                {
                    Data = product,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}