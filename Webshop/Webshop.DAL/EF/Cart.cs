namespace Webshop.DAL.EF
{
    public class Cart
    {
        public Cart()
        {
            Items = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
