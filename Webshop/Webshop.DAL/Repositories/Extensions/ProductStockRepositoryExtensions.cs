using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class ProductStockRepositoryExtensions
    {
        public static IQueryable<ProductStock> FilterByStock(this IQueryable<ProductStock> productStocks, int minStock = 0)
        {
            return productStocks.Where(ps => ps.Stock > minStock);
        }

        public static IQueryable<ProductStock> FilterBySize(this IQueryable<ProductStock> productStocks, int sizeId)
        {
            return productStocks.Where(ps => ps.SizeId == sizeId);
        }

        public static IQueryable<ProductStock> FindProductStockByProductId(this IQueryable<ProductStock> productStocks, int productId)
        {
            return productStocks.Where(ps => ps.Product.Id == productId);
        }

        public static IQueryable<ProductStock> FilterByProductId(this IQueryable<ProductStock> productStocks, int productId)
        {
            return productStocks.Where(ps => ps.Product.Id == productId);
        }

        public static IQueryable<ProductStock> FilterByProductIdAndSize(this IQueryable<ProductStock> productStocks, int productId, int sizeId)
		{
            return productStocks.FilterByProductId(productId).FilterBySize(sizeId);
		}

        public static IQueryable<ProductStock> WithProduct(this IQueryable<ProductStock> productStocks)
            => productStocks.Include(ps => ps.Product);

        public static IQueryable<ProductStock> WithSize(this IQueryable<ProductStock> productStocks)
            => productStocks.Include(ps => ps.Size);

        public async static Task<IReadOnlyCollection<string>> GetSizes(this IQueryable<ProductStock> productstocks)
        {
            return await productstocks
                            .Select(ps => ps.Size.Name)
                            .ToArrayAsync();
        }

        public async static Task<IReadOnlyCollection<Models.ProductStock>> GetStocks(this IQueryable<ProductStock> productstocks)
        {
            return await productstocks
                            .Select(ps => ps.GetStock())
                            .ToArrayAsync();
        }

        public async static Task<ProductStock?> GetProductStockOrNull(this IQueryable<ProductStock> productstocks)
        {
            return await productstocks
                            .SingleOrDefaultAsync();
        }

        public async static Task<int?> GetStock(this IQueryable<ProductStock> productstocks)
        {
            return await productstocks
                            .Select(ps => ps.Stock)
                            .SingleOrDefaultAsync();
        }

        public static Models.ProductStock GetStock(this ProductStock dbRecord)
            => new Models.ProductStock(dbRecord.Size.Name, dbRecord.Stock);
    }
}
