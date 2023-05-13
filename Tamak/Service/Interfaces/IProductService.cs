using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Data.Response;
using Tamak.ViewModels;

namespace Tamak.Service.Interfaces
{
    public interface IProductService
    {
        BaseResponse<Dictionary<long, string>> GetCategories();
        public IBaseResponse<List<Product>> GetProducts();

        public Task<IBaseResponse<ProductViewModel>> GetProduct(long id);

        public Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term);

        public Task<IBaseResponse<Product>> Create(long assortimentId, Assortiment assortiment);

        public Task<IBaseResponse<bool>> DeleteProduct(long id);

        public Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model);

        public Task<BaseResponse<Product>> Save(ProductViewModel model);

        public Task<BaseResponse<Product>> ChangeAvaliable(ProductViewModel model);
    }
}
