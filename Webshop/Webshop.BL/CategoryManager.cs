using Webshop.DAL.Repositories.Interfaces;
using Webshop.DAL.Models;

namespace Webshop.BL
{
    public class CategoryManager
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category?> GetCategory(int id)
            => await categoryRepository.GetByIdOrNull(id);

        public async Task<IReadOnlyCollection<Category>> ListMainCategories()
            => await categoryRepository.ListMainCategories();

        public async Task<IReadOnlyCollection<Category>> ListSubcategoriesByParentCategory(int parentCategoryId)
            => await categoryRepository.ListSubcategoriesByParentCategory(parentCategoryId);
    }
}
