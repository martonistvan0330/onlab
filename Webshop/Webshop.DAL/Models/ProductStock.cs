namespace Webshop.DAL.Models
{
    public class ProductStock
    {
        public readonly string Size;
        public readonly int Stock;

        public ProductStock(string size, int stock)
        {
            Size = size;
            Stock = stock;
        }
    }
}
