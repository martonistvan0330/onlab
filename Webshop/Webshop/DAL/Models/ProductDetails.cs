namespace Webshop.DAL.Models
{
    public class ProductDetails
    {
        public readonly string Name;
        public readonly double Price;
        public readonly int VatPercentage;
        public readonly string Description;

        public ProductDetails() { }

        public ProductDetails(string name, double price, int vatPercentage)
        {
            Name = name;
            Price = price;
            VatPercentage = vatPercentage;
        }
    }
}
