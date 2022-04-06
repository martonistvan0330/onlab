namespace Webshop.DAL.Models
{
    public class PaymentMethod
    {
        public readonly string Method;
        public readonly int Deadline;

        public PaymentMethod(string method, int deadline)
        {
            Method = method;
            Deadline = deadline;
        }
    }
}