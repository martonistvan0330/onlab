using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        int GetProductCount();

        Task<IReadOnlyCollection<Product>> GetMainPageProducts(List<int> productIds);
        Task<ProductsWithPageCount> GetFilteredProducts(List<int> categoryIds, double minPrice, double maxPrice, List<string> sizes, int page);
        Task<ProductDetails?> GetProductDetailsOrNull(int productId);
        Task<bool> ExistsByName(string productName);
        Task<int?> GetProductIdByName(string productName);
    }
}
