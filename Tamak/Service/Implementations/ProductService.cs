using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Service.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _allProducts;

        public ProductService(IProductRepository allProducts)
        {
            _allProducts = allProducts;
        }

        public async Task<IBaseRepository<ProductViewModel>> CreateProduct(ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var product = new Product()
                {
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Img = productViewModel.Img,
                    Price = productViewModel.Price,
                    Available = productViewModel.Available,
                    //Category = productViewModel.Category
                };
                await _allProducts.Create(product);
                return (IBaseRepository<ProductViewModel>)baseResponse;
            }
            catch (Exception ex)
            {
                return (IBaseRepository<ProductViewModel>)new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseRepository<Product>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _allProducts.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Data.Enum.StatusCode.UserNotFound;
                    return (IBaseRepository<Product>)baseResponse;
                }
                baseResponse.Data= product;
                return (IBaseRepository<Product>)baseResponse;
            }
            catch (Exception ex) 
            {
                return (IBaseRepository<Product>)new BaseResponse<Product>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseRepository<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = _allProducts.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Data.Enum.StatusCode.UserNotFound;
                    return (IBaseRepository<bool>)baseResponse;
                }
                await _allProducts.Delete(product);
                return (IBaseRepository<bool>)baseResponse;
            }
            catch (Exception ex)
            {
                return (IBaseRepository<bool>)new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseRepository<Product>> GetProductByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _allProducts.GetByName(name);
                if (product == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = Data.Enum.StatusCode.UserNotFound;
                    return (IBaseRepository<Product>)baseResponse;
                }
                baseResponse.Data = product;
                return (IBaseRepository<Product>)baseResponse;
            }
            catch (Exception ex)
            {
                return (IBaseRepository<Product>)new BaseResponse<Product>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = await _allProducts.Select();
                if (products.Count == 0)
                {
                    baseResponse.Description = "Упс, такого продукта не существует...";
                    baseResponse.StatusCode = Data.Enum.StatusCode.NotFound;
                    return baseResponse;
                }
                baseResponse.Data = products;
                baseResponse.StatusCode = Data.Enum.StatusCode.Success;
                return baseResponse;
            } 
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = Data.Enum.StatusCode.InternalServerError,
                };
            }
        }
    }
}
