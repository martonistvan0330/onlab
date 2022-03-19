namespace Webshop.DAL.EF
{
    public class Size
    {
        public Size(string name)
        {
            Name = name;
            ProductStocks = new HashSet<ProductStock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductStock> ProductStocks { get; set; }
    }
}
