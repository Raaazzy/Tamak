using Tamak.Data.Models;

namespace Tamak.Data.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> getFavouriteProducts { get; }
        Product getObjectProdust(int productId);
    }
}
