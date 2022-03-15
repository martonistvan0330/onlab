namespace Webshop.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductDetails() { }

        public ProductDetails(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
