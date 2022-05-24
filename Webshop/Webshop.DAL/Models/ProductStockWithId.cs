using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
	public class ProductStockWithId
	{
        public readonly int ProductId;
        public readonly int SizeId;
        public readonly int Stock;

        public ProductStockWithId(int productId, int sizeId, int stock)
        {
            ProductId = productId;
            SizeId = sizeId;
            Stock = stock;
        }
    }
}
