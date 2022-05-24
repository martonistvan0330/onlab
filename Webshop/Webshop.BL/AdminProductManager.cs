using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class AdminProductManager
    {
        private IAdminProductRepository productRepository;
        private ProductImageRepository productImageRepository;
        private IAdminProductStockRepository productStockRepository;

        public AdminProductManager(
            IAdminProductRepository productRepository,
            ProductImageRepository productImageRepository,
            IAdminProductStockRepository productStockRepository)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.productStockRepository = productStockRepository;
        }

        public async Task<ProductDetailsWithSize> GetProductWithSize(int productId)
        {
            var productDetails = await productRepository.GetProductDetailsOrNull(productId);
            var XS = await productStockRepository.GetStockByProductSizeOrNull(productId, 1);
            var S = await productStockRepository.GetStockByProductSizeOrNull(productId, 2);
            var M = await productStockRepository.GetStockByProductSizeOrNull(productId, 3);
            var L = await productStockRepository.GetStockByProductSizeOrNull(productId, 4);
            var XL = await productStockRepository.GetStockByProductSizeOrNull(productId, 5);
            var XXL = await productStockRepository.GetStockByProductSizeOrNull(productId, 6);
            return new ProductDetailsWithSize(productDetails, XS, S, M, L, XL, XXL);
        }

        public async Task<int> AddProduct(NewProduct newProduct)
        {
            var productId = await productRepository.AddProduct(newProduct);
            await productStockRepository.AddNewStock(productId, 1, newProduct.XS);
            await productStockRepository.AddNewStock(productId, 2, newProduct.S);
            await productStockRepository.AddNewStock(productId, 3, newProduct.M);
            await productStockRepository.AddNewStock(productId, 4, newProduct.L);
            await productStockRepository.AddNewStock(productId, 5, newProduct.XL);
            await productStockRepository.AddNewStock(productId, 6, newProduct.XXL);
            return productId;
        }

        public async Task<int> UpdateProduct(int productId, NewProduct newProduct)
        {
            await productRepository.UpdateProduct(productId, newProduct);
            await productStockRepository.RefreshStock(productId, 1, newProduct.XS);
            await productStockRepository.RefreshStock(productId, 2, newProduct.S);
            await productStockRepository.RefreshStock(productId, 3, newProduct.M);
            await productStockRepository.RefreshStock(productId, 4, newProduct.L);
            await productStockRepository.RefreshStock(productId, 5, newProduct.XL);
            await productStockRepository.RefreshStock(productId, 6, newProduct.XXL);
            return productId;
        }

        public async Task<bool> AddProductImage(byte[] image, string fileName, int productId, bool main = false)
        {
            return await productImageRepository.AddImage(image, fileName, productId, main);
        }
    }
}
