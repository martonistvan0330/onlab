namespace Webshop.DAL.Models
{
    public class NewCartItem
    {
        public readonly int ProductId;
        public readonly int Amount;
        public readonly int SizeId;

        public NewCartItem(int productId, int amount, int sizeId)
        {
            ProductId = productId;
            Amount = amount;
            SizeId = sizeId;
        }
    }
}
