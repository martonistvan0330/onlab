namespace Webshop.DAL.EF
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }

        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
