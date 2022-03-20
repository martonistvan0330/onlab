namespace Webshop.DAL.Models
{
    public class Product
    {
        public readonly string Name;
        public readonly double Price;

        public Product() { }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
