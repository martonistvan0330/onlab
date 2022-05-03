using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
	public class ProductsWithPageCount
	{
        public readonly Product[] Products;
        public readonly int PageCount;

        public ProductsWithPageCount(Product[] products, int pageCount)
        {
            Products = products;
            PageCount = pageCount;
        }
    }
}
