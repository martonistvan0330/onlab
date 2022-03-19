namespace Webshop.DAL
{
    public class PaymentInfo
    {
        public PaymentInfo()
        {
            Customers = new HashSet<Customer>(); 
        }

        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public int BillingAddressId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Address BillingAddress { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
