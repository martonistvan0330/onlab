namespace Webshop.DAL.Models
{
    public class CartItemWithId
    {
        public int Id { get; set; }
        public readonly Product Product;
        public readonly string Size;
        public readonly int Amount;
        public readonly double Price;

        public CartItemWithId(int id, Product product, string size, int amount, double price)
        {
            Id = id;
            Product = product;
            Size = size;
            Amount = amount;
            Price = price;
        }
    }
}
