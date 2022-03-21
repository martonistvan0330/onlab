namespace Webshop.DAL.Models
{
    public class CartItem
    {
        public readonly Product Product;
        public readonly string Size;
        public readonly int Amount;

        public CartItem(Product product, string size, int amount)
        {
            Product = product;
            Size = size;
            Amount = amount;
        }
    }
}
