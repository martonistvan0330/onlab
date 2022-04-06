using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> FilterByCategory(this IQueryable<Product> products, List<int> categoryIds)
        {
            return products
                    .Where(p => categoryIds.Contains(p.Category.Id));
        }

        public static IQueryable<Product> FilterByPrice(this IQueryable<Product> products, double minPrice, double maxPrice)
        {
            var filteredProducts = products
                                    .Where(p => p.Price > minPrice);
            if (maxPrice > 0)
            {
                filteredProducts = filteredProducts
                                    .Where(p => p.Price < maxPrice);
            }
            return filteredProducts;
        }

        public static IQueryable<Product> FilterBySize(this IQueryable<Product> products, List<string> sizes)
        {
            if (sizes.Count <= 0)
            {
                return products;
            }
            else
            {
                return products.Where(p => p.ProductStocks.Any(ps => sizes.Contains(ps.Size.Name) && ps.Stock > 0));
            }
        }

        public static IQueryable<Product> FindByName(this IQueryable<Product> products, string productName)
        {
            return products.Where(p => p.Name.Equals(productName));
        }

        public static IQueryable<Product> FindByIdList(this IQueryable<Product> products, List<int> productIds)
        {
            return products.Where(p => productIds.Contains(p.Id));
        }

        public static async Task<IReadOnlyCollection<Models.Product>> GetProducts(this IQueryable<Product> products)
        {
            return await products
                            .WithImages()           
                            .Select(dbRec => dbRec.GetProduct())
                            .ToArrayAsync();
        }

        public static async Task<IReadOnlyCollection<Models.Product>> GetProducts(this IQueryable<Product> products, int pageNum, int productsPerPage = 6)
        {
            return await products
                            .Skip((pageNum - 1) * productsPerPage)
                            .Take(productsPerPage)
                            .GetProducts();
        }

        public async static Task<Models.ProductDetails?> GetProductDetailsOrNull(this IQueryable<Product> products)
        {
            return await products
                            .WithVat()
                            .Select(p => p.GetProductDetails())
                            .SingleOrDefaultAsync();
        }

        public async static Task<int?> GetProductIdOrNull(this IQueryable<Product> products)
        {
            return await products
                            .Select(p => p.Id)
                            .SingleOrDefaultAsync();
        }

        public async static Task<bool> ExistsByName(this IQueryable<Product> products, string productName)
        {
            var dbProduct = await products
                            .GetProductByNameOrNull(productName);
            if (dbProduct == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async static Task<Product?> GetProductByNameOrNull(this IQueryable<Product> products, string productName)
        {
            return await products
                            .FindByName(productName)
                            .GetProductOrNull();
        }

        public async static Task<Product?> GetProductOrNull(this IQueryable<Product> products)
        {
            return await products
                            .SingleOrDefaultAsync();
        }

        public static IQueryable<Product> WithVat(this IQueryable<Product> products)
            => products.Include(p => p.Vat);

        public static IQueryable<Product> WithImages(this IQueryable<Product> products)
            => products.Include(p => p.ProductImages);

        public static Models.Product GetProduct(this Product dbRecord)
            => new Models.Product(dbRecord.Id, dbRecord.Name, dbRecord.Price, dbRecord.ProductImages.First(pi => pi.MainImage).ImageSource);

        public static Models.ProductDetails GetProductDetails(this Product dbRecord)
            => new Models.ProductDetails(dbRecord.Name, dbRecord.Price, dbRecord.Vat.Percentage);
    }
}
