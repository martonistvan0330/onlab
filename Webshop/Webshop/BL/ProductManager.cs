using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class ProductManager
    {
        public readonly IProductRepository productRepository;
        public readonly ICategoryRepository categoryRepository;
        public readonly IProductStockRepository productStockRepository;

        public ProductManager(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductStockRepository productStockRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.productStockRepository = productStockRepository;
        }

        public async Task<IReadOnlyCollection<Product>> GetMainPageProducts()
        {
            var productCount = productRepository.GetProductCount();
            var random = new Random();
            var productIds = new List<int>();
            while (productIds.Count < 6)
            {
                int id = random.Next(0, productCount);
                if (!productIds.Contains(id))
                {
                    productIds.Add(id);
                }
            }

            return await productRepository.GetProductsByIdList(productIds);
        }

        public async Task<IReadOnlyCollection<Product>> GetFilteredProducts(string categoryName, double minPrice, double maxPrice, string? sizes, int page)
        {
            var categoryIds = await categoryRepository.GetCategoryIdsByParentCategory(categoryName);

            if (categoryIds.Count == 0)
            {
                return Array.Empty<Product>();
            }
            else
            {
                List<string> sizeList;
                if (!string.IsNullOrEmpty(sizes))
                {
                    sizeList = sizes.Split(',').ToList();
                }
                else
                {
                    sizeList = Array.Empty<string>().ToList();
                }
                return await productRepository.GetFilteredProducts(categoryIds.ToList(), minPrice, maxPrice, sizeList, page);
            }
        }

        public async Task<ProductDetails?> GetProductDetailsOrNull(string productName)
        {
            return await productRepository.GetProductDetailsOrNull(productName);
        }

        public async Task<IReadOnlyCollection<string>> GetSizesByName(string productName)
        {
            var productId = await productRepository.GetProductIdByName(productName);

            if (productId == null)
            {
                return Array.Empty<string>();
            }
            else
            {
                return await productStockRepository.GetSizesByProductId(productId.Value);
            }
        }

        public async Task<IReadOnlyCollection<ProductStock>> GetStocksByName(string productName)
        {
            var productId = await productRepository.GetProductIdByName(productName);

            if (productId == null)
            {
                return Array.Empty<ProductStock>();
            }
            else
            {
                return await productStockRepository.GetStocksByProductId(productId.Value);
            }
        }

        public async Task<int?> GetStockByNameSize(string productName, string size)
        {
            var productId = await productRepository.GetProductIdByName(productName);

            if (productId == null)
            {
                return null;
            }
            else
            {
                return await productStockRepository.GetStockByProductIdSize(productId.Value, size);
            }
        }
    }
}
