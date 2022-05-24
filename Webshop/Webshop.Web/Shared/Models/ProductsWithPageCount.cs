using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
	public class ProductsWithPageCount
	{
		public Product[] Products { get; set; }
		public int PageCount { get; set; }
	}
}
