namespace Webshop.DAL.EF
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int ShippingInfoId { get; set; }
        public int PaymentInfoId { get; set; }
        public bool MainCustomer { get; set; }

        public ShippingInfo ShippingInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
