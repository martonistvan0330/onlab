using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class ProductRepository : IProductRepository, IAdminProductRepository
    {
        public const int PAGES_PER_PRODUCT = 6;

        private readonly WebshopDbContext dbContext;

        public ProductRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public int GetProductCount()
        {
            return dbContext.Product.Count();
        }

        public async Task<IReadOnlyCollection<Models.Product>> GetMainPageProducts(List<int> productIds)
        {
            return await dbContext.Product
                            .FindByIdList(productIds)
                            .GetProducts();
        }

        public async Task<ProductsWithPageCount> GetFilteredProducts(List<int> categoryIds, double minPrice, double maxPrice, List<string> sizes, int page)
        {
            var query = dbContext.Product
                            .FilterByCategory(categoryIds)
                            .FilterByPrice(minPrice, maxPrice)
                            .FilterBySize(sizes);
            var pageCount = query.Count();
            var products = await query.GetProducts(page);
            return new ProductsWithPageCount(
                products.ToArray(),
                (pageCount + PAGES_PER_PRODUCT - 1) / PAGES_PER_PRODUCT);
        }

        public async Task<ProductDetails?> GetProductDetailsOrNull(int productID)
        {
            return await dbContext.Product
                            .WithImages()
                            .FindById(productID)
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

        public async Task<int> AddProduct(NewProduct newProduct)
        {
            var product = new EF.Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId,
                VatId = 4,
            };

            dbContext.Product.Add(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> UpdateProduct(int productId, NewProduct newProduct)
        {
            var dbProduct = await dbContext.Product.FindById(productId).SingleOrDefaultAsync();
            if (dbProduct != null)
            {
                dbProduct.Name = newProduct.Name;
                dbProduct.Price = newProduct.Price;
                dbProduct.CategoryId = newProduct.CategoryId;
                dbContext.Product.Update(dbProduct);
                await dbContext.SaveChangesAsync();
            }
            return productId;
        }
    }
}
