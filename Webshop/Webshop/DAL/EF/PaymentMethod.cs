namespace Webshop.DAL
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            PaymentInfos = new HashSet<PaymentInfo>();
        }

        public int Id { get; set; }
        public string Method { get; set; }
        public int Deadline { get; set; }

        public ICollection<PaymentInfo> PaymentInfos { get; set; }
    }
}
