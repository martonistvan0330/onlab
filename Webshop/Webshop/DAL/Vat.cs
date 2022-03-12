namespace Webshop.DAL
{
    public class Vat
    {
        public Vat()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int Percentage { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
