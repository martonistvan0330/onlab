using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IProductStockRepository
    {
        Task<IReadOnlyCollection<string>> GetSizesByProductId(int productId);
        Task<IReadOnlyCollection<ProductStock>> GetStocksByProductId(int productId);
        Task<int> GetStockByProductSizeOrNull(int productId, int sizeId);
        Task<bool> UpdateStock(int productId, int sizeId, int amount);
        Task<bool> AddStock(IReadOnlyCollection<ProductStockWithId> productStocks);
    }
}