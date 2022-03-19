using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories
{
    internal static class CategoryRepositoryExtensions
    {
        public static IQueryable<Category> FindMainCategories(this IQueryable<Category> categories)
        {
            return categories.FindCategoriesByParentCategory(null);
        }

        public static IQueryable<Category> FindCategoriesByParentCategory(this IQueryable<Category> categories, Category? parentCategory = null)
        {
            return categories.Where(c => c.ParentCategory == parentCategory);
        }

        public static async Task<IReadOnlyCollection<Models.Category>> GetCategories(this IQueryable<Category> categories)
        {
            return await categories.Select(dbRec => dbRec.GetCategory())
                                  .ToArrayAsync();
        }

        public static Models.Category GetCategory(this Category dbRecord)
        {
            return new Models.Category(dbRecord.Name);
        }
    }
}