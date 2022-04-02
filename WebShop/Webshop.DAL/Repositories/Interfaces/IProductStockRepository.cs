using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IProductStockRepository
    {
        Task<IReadOnlyCollection<string>> GetSizesByProductId(int productId);
        Task<IReadOnlyCollection<ProductStock>> GetStocksByProductId(int productId);
        Task<int?> GetStockByProductIdSize(int productId, string size);
    }
}