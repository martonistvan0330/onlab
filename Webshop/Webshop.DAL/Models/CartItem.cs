namespace Webshop.DAL.Models
{
    public class CartItem
    {
        public readonly int Id;
        public readonly int ProductId;
        public readonly string ProductName;
        public readonly int SizeId;
        public readonly string SizeName;
        public readonly int Amount;
        public readonly double Price;

        public CartItem(int id, int productId, string productName, int sizeId, string sizeName, int amount, double price)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            SizeId = sizeId;
            SizeName = sizeName;
            Amount = amount;
            Price = price;
        }
    }
}
