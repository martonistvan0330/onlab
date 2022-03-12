namespace Webshop.DAL
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
            InverseParentCategory = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection<Category> InverseParentCategory { get; set; }
    }
}
