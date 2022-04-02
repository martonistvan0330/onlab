namespace Webshop.DAL.EF
{
    public class Cart
    {
        public Cart()
        {
            Items = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionId { get; set; }

        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
