using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;

namespace Tamak.Service.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _allProducts;

        public ProductService(IProductRepository allProducts)
        {
            _allProducts = allProducts;
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
                    Description = $"[GetProducts] : {ex.Message}"
                };
            }
        }
    }
}
