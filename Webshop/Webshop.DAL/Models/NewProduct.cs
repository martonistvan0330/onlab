using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class NewProduct
    {
        public readonly string Name;
        public readonly double Price;
        public readonly int CategoryId;
        public readonly int XS;
        public readonly int S;
        public readonly int M;
        public readonly int L;
        public readonly int XL;
        public readonly int XXL;

        public NewProduct(string name, double price, int categoryId, int xs, int s, int m, int l, int xl, int xxl)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;
            XS = xs;
            S = s;
            M = m;
            L = l;
            XL = xl;
            XXL = xxl;
        }
    }
}
