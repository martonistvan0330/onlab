using System.Transactions;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
	public class ProductManager
	{
		private readonly IProductRepository productRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IProductStockRepository productStockRepository;
		private readonly ISizeRepository sizeRepository;

		public ProductManager(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductStockRepository productStockRepository, ISizeRepository sizeRepository)
		{
			this.productRepository = productRepository;
			this.categoryRepository = categoryRepository;
			this.productStockRepository = productStockRepository;
			this.sizeRepository = sizeRepository;
		}

		public async Task<IReadOnlyCollection<Product>> GetMainPageProducts()
		{
			var productCount = productRepository.GetProductCount();
			var random = new Random();
			var productIds = new List<int>();
			while (productIds.Count < 6)
			{
				int id = random.Next(1, productCount+1);
				if (!productIds.Contains(id))
				{
					productIds.Add(id);
				}
			}

			return await productRepository.GetMainPageProducts(productIds);
		}

		public async Task<IReadOnlyCollection<Product>> GetFilteredProducts(int categoryId, double minPrice, double maxPrice, string? sizes, int page)
		{
			var categoryIds = await categoryRepository.GetCategoryIdsByParentCategory(categoryId);

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

		public async Task<ProductDetails?> GetProductDetailsOrNull(int productId)
		{
			return await productRepository.GetProductDetailsOrNull(productId);
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

		public async Task<(bool, int)> GetProductIdByName(string productName)
		{
			if (await productRepository.ExistsByName(productName))
			{
				var productId = await productRepository.GetProductIdByName(productName);
				return (true, productId.Value);
			}
			else
			{
				return (false, -1);
			}
		}

		public async Task<int?> GetStockByProductSize(int productId, int sizeId)
		{
			return await productStockRepository.GetStockByProductSizeOrNull(productId, sizeId);
		}

		public async Task<(bool, int)> GetSizeIdByName(string sizeName)
		{
			if (await sizeRepository.ExistsByName(sizeName))
			{
				var sizeId = await sizeRepository.GetIdByName(sizeName);
				return (true, sizeId);
			}
			else
			{
				return (false, -1);
			}
		}

		public async Task<bool> UpdateStock(int productId, int sizeId, int amount)
		{
			using (var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
						TransactionScopeAsyncFlowOption.Enabled))
			{
				if (await productStockRepository.UpdateStock(productId, sizeId, amount))
				{
					transaction.Complete();
					return true;
				}
				return false;
			}
		}

		public async Task<bool> AddStock(IReadOnlyCollection<ProductStockWithId> productStocks)
		{
			if (await productStockRepository.AddStock(productStocks))
			{
				return true;
			}
			return false;
		}
	}
}
