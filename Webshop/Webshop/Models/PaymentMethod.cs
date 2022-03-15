namespace Webshop.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public int Deadline { get; set; }

        public PaymentMethod(int id, string method, int deadline)
        {
            Id = id;
            Method = method;
            Deadline = deadline;
        }
    }
}