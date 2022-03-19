using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<Category>> ListMainCategories();
    }
}
