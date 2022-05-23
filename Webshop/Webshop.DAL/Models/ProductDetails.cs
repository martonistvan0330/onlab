namespace Webshop.DAL.Models
{
    public class ProductDetails
    {
        public readonly int Id;
        public readonly string Name;
        public readonly double Price;
        public readonly string MainImageSource;
        public readonly List<string> ImageSources;

        public ProductDetails() { }

        public ProductDetails(int id, string name, double price, string mainImageSource, List<string> imageSources)
        {
            Id = id;
            Name = name;
            Price = price;
            MainImageSource = mainImageSource;
            ImageSources = imageSources;
        }
    }
}
