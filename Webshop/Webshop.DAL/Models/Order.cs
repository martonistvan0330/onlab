using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class Order
    {
        public readonly int Id;
        public readonly string Status;
        public readonly double Total;
        public readonly bool IsCancelledOrDelivered;

        public Order(
            int id,
            string status,
            double total,
            bool cancelledOrDelivered)
        {
            Id = id;
            Status = status;
            Total = total;
            IsCancelledOrDelivered = cancelledOrDelivered;
        }
    }
}
