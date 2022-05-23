using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string MainImageSource { get; set; }
        public List<string> ImageSources { get; set; }
    }
}
