using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class ProductDetailsWithSize
    {
        public ProductDetails ProductDetails { get; set; }
        public int XS { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
        public int XXL { get; set; }
    }
}
