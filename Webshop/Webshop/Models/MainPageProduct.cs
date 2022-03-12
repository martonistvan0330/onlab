namespace Webshop.Models
{
    public class MainPageProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public MainPageProduct(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
