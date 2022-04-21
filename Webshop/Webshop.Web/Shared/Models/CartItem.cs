using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
