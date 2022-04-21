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

        public async Task<bool> UpdateStock(int productId, int sizeId, int amount)
        {
            var dbProductStock = await dbContext.ProductStock
                                        .FindProductStockByProductId(productId)
                                        .FilterBySize(sizeId)
                                        .FilterByStock()
                                        .GetProductStockOrNull();
            if (dbProductStock != null)
            {
                dbProductStock.Stock -= amount;
                dbContext.ProductStock.Update(dbProductStock);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<(bool, int)> UpdateCartItem(Models.UpdateCartItem cartItem, int cartId)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByIdCart(cartItem.Id, cartId);
            if (dbCartItem != null)
            {
                dbCartItem.Amount = cartItem.Amount;
                dbCartItem.SizeId = cartItem.SizeId;
                dbContext.CartItem.Update(dbCartItem);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbCartItem.ProductId);
                }
                catch
                {
                    return (false, -1);
                }
            }
            else
            {
                return (false, -1);
            }
        }
    }
}
