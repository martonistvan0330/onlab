using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdOrNull(int id);
        Task<IReadOnlyCollection<Category>> ListCategories();
        Task<IReadOnlyCollection<Category>> ListMainCategories();
        Task<IReadOnlyCollection<Category>> ListSubcategoriesByParentCategory(int parentCategoryId);
        Task<IReadOnlyCollection<int>> GetCategoryIdsByParentCategory(int categoryId);
    }
}
