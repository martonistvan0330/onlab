namespace Webshop.DAL.EF
{
    public class Size
    {
        public Size(string name)
        {
            Name = name;
            ProductStocks = new HashSet<ProductStock>();
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductStock> ProductStocks { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
