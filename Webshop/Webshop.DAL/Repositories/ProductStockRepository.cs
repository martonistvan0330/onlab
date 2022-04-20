using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly WebshopDbContext dbContext;

        public ProductStockRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<IReadOnlyCollection<string>> GetSizesByProductId(int productId)
        {
            return await dbContext.ProductStock
                .WithProduct()
                .FindProductStockByProductId(productId)
                .FilterByStock()
                .GetSizes();
        }

        public async Task<IReadOnlyCollection<Models.ProductStock>> GetStocksByProductId(int productId)
        {
            return await dbContext.ProductStock
                .WithProduct()
                .WithSize()
                .FindProductStockByProductId(productId)
                .FilterByStock()
                .GetStocks();
        }

        public async Task<int?> GetStockByProductSizeOrNull(int productId, int sizeId)
        {
            return await dbContext.ProductStock
                .WithProduct()
                .FindProductStockByProductId(productId)
                .FilterBySize(sizeId)
                .GetStock();
        }
    }
}
