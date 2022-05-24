namespace Webshop.DAL.EF
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
