namespace Webshop.DAL.EF
{
    public class PaymentInfo
    {
        public PaymentInfo()
        {
            Customers = new HashSet<Customer>(); 
        }

        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public int BillingAddressInfoId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public AddressInfo BillingAddressInfo { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
