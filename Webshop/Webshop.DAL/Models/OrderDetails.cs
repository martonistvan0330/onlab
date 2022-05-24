using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class OrderDetails
    {
        public readonly string Email;
        public readonly string ShippingMethod;
        public readonly AddressInfo ShippingAddressInfo;
        public readonly string PaymentMethod;
        public readonly AddressInfo BillingAddressInfo;
        public readonly OrderItem[] OrderItems;
        public readonly double Total;

        public OrderDetails(
            string email,
            string shippingMethod,
            AddressInfo shippingAddress,
            string paymentMethod,
            AddressInfo billingAddress, 
            OrderItem[] orderItems,
            double total)
        {
            Email = email;
            ShippingMethod = shippingMethod;
            ShippingAddressInfo = shippingAddress;
            PaymentMethod = paymentMethod;
            BillingAddressInfo = billingAddress;
            OrderItems = orderItems;
            Total = total;
        }
    }
}
