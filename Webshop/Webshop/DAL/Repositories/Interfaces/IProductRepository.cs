﻿using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        int GetProductCount();

        Task<IReadOnlyCollection<Product>> GetProductsByIdList(List<int> productIds);
        Task<IReadOnlyCollection<Product>> GetFilteredProducts(List<int> categoryIds, double minPrice, double maxPrice, List<string> sizes, int page);
        Task<ProductDetails?> GetProductDetailsOrNull(string productName);
        Task<int?> GetProductIdByName(string productName);
    }
}