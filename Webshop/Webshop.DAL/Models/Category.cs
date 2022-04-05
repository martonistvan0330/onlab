namespace Webshop.DAL.Models
{
    public class Category
    {
        public readonly int Id;
        public readonly string Name;

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
