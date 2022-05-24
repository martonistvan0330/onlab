using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class ProductDetailsWithSize
    {
        public readonly ProductDetails ProductDetails;
        public readonly int XS;
        public readonly int S;
        public readonly int M;
        public readonly int L;
        public readonly int XL;
        public readonly int XXL;

        public ProductDetailsWithSize(ProductDetails productDetails, int xs, int s, int m, int l, int xl, int xxl)
        {
            ProductDetails = productDetails;
            XS = xs;
            S = s;
            M = m;
            L = l;
            XL = xl;
            XXL = xxl;
        }
    }
}
