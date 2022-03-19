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

        public async Task<IReadOnlyCollection<Category>> ListMainCategories()
            => await categoryRepository.ListMainCategories();
    }
}
