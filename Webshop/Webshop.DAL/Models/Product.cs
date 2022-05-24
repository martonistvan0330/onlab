namespace Webshop.DAL.Models
{
    public class Product
    {
        public readonly int Id;
        public readonly string Name;
        public readonly double Price;
        public readonly byte[] Image;

        public Product(int id, string name, double price, byte[] image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }
    }
}
