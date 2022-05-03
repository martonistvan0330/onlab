using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public bool IsCancelledOrDelivered { get; set; }
    }
}
