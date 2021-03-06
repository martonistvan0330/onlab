namespace Webshop.DAL.Models
{
    public class ProductDetails
    {
        public readonly int Id;
        public readonly string Name;
        public readonly double Price;
        public readonly int CategoryId;
        public readonly byte[] MainImage;
        public readonly List<byte[]> Images;

        public ProductDetails() { }

        public ProductDetails(int id, string name, int categoryId, double price, byte[] mainImage, List<byte[]> images)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
            MainImage = mainImage;
            Images = images;
        }
    }
}
