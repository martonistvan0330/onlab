using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.EF
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }
        public bool MainImage { get; set; }

        public Product Product { get; set; }
    }
}
