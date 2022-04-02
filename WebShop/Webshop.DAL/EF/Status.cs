namespace Webshop.DAL.EF
{
    public class Status
    {
        public Status()
        {
            OrderItems = new HashSet<OrderItem>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
