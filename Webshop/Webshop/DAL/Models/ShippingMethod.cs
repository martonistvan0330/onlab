namespace Webshop.Models
{
    public class ShippingMethod
    {
        public int Id { get; set; }
        public string Method { get; set; }

        public ShippingMethod(int id, string method)
        {
            Id = id;
            Method = method;
        }
    }
}