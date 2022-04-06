namespace Webshop.DAL.Models
{
    public class Product
    {
        public readonly int Id;
        public readonly string Name;
        public readonly double Price;
        public readonly string ImageSource;

        public Product(int id, string name, double price, string imageSource)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageSource = imageSource;
        }
    }
}
