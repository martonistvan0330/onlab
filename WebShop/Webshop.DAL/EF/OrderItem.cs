namespace Webshop.DAL.EF
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }
        public int SizeId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
        public Status Status { get; set; }
        public Size Size { get; set; }
    }
}
