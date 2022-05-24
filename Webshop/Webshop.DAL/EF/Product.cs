namespace Webshop.DAL.EF
{
    public class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            CartItems = new HashSet<CartItem>();
            ProductStocks = new HashSet<ProductStock>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int VatId { get; set; }
        public int CategoryId { get; set; }

        public Vat Vat { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<ProductStock> ProductStocks { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
