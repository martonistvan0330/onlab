using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories
{
    public class ProductImageRepository
    {
        private WebshopDbContext dbContext;

        public ProductImageRepository(WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddImage(byte[] image, string fileName, int productId, bool main = false)
        {
            var productImage = new ProductImage()
            {
                ProductId = productId,
                FileName = fileName,
                Image = image,
                MainImage = main
            };

            dbContext.ProductImage.Add(productImage);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
