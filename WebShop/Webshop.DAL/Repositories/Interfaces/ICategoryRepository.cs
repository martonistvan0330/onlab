using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<Category>> ListMainCategories();
        Task<IReadOnlyCollection<Category>> ListSubcategoriesByParentCategory(string parentCategory);
        Task<IReadOnlyCollection<int>> GetCategoryIdsByParentCategory(string categoryName);
    }
}
