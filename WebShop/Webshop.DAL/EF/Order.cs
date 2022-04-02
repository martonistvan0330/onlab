namespace Webshop.DAL.EF
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public int StatusId { get; set; }
        public int CustomerId { get; set; }

        public Status Status { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
