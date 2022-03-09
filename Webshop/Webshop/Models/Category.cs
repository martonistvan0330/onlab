namespace Webshop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? ParentCategory { get; set; }

        public Category(int id, string name, Category? parentCategory = null)
        {
            Id = id;
            Name = name;
            ParentCategory = parentCategory;
        }
    }
}
