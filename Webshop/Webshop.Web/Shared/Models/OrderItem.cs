using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class OrderItem
    {
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string ImageSource { get; set; }
    }
}
