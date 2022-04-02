using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebshopDbContext dbContext;

        public ProductRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public int GetProductCount()
        {
            return dbContext.Product.Count();
        }

        public async Task<IReadOnlyCollection<Models.Product>> GetProductsByIdList(List<int> productIds)
        {
            return await dbContext.Product
                            .FindByIdList(productIds)
                            .GetProducts();
        }

        public async Task<IReadOnlyCollection<Models.Product>> GetFilteredProducts(List<int> categoryIds, double minPrice, double maxPrice, List<string> sizes, int page)
        {
            return await dbContext.Product
                            .FilterByCategory(categoryIds)
                            .FilterByPrice(minPrice, maxPrice)
                            .FilterBySize(sizes)
                            .GetProducts(page, 3);
        }

        public async Task<ProductDetails?> GetProductDetailsOrNull(string productName)
        {
            return await dbContext.Product
                            .FindByName(productName)
                            .GetProductDetailsOrNull();
        }

        public async Task<bool> ExistsByName(string productName)
        {
            return await dbContext.Product
                            .ExistsByName(productName);
        }

        public async Task<int?> GetProductIdByName(string productName)
        {
            return await dbContext.Product
                            .FindByName(productName)
                            .GetProductIdOrNull();
        }
    }
}
