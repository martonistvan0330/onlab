namespace Webshop.DAL
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ShippingInfoId { get; set; }
        public int PaymentInfoId { get; set; }
        public bool MainCustomer { get; set; }

        public User User { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
    }
}
