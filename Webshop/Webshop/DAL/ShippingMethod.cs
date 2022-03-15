namespace Webshop.DAL
{
    public class ShippingMethod
    {
        public ShippingMethod()
        {
            ShippingInfos = new HashSet<ShippingInfo>();
        }

        public int Id { get; set; }
        public string Method { get; set; }

        public ICollection<ShippingInfo> ShippingInfos { get; set; }
    }
}
