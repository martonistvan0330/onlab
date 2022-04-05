using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
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
            return new Models.Category(dbRecord.Id , dbRecord.Name);
        }

        public static async Task<Category?> GetByIdOrNull(this IQueryable<Category> categories, int categoryId)
        {
            return await categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        public static async Task<Category?> GetByNameOrNull(this IQueryable<Category> categories, string categoryName)
        {
            return await categories.SingleOrDefaultAsync(c => c.Name.Equals(categoryName));
        }

        public static IQueryable<Category> FilterByParentCategory(this IQueryable<Category> categories, Category parentCategory)
        {
            return categories.Where(c => c == parentCategory || c.ParentCategory == parentCategory || c.ParentCategory.ParentCategory == parentCategory);
        }

        public static async Task<IReadOnlyCollection<int>> GetIds(this IQueryable<Category> categories)
        {
            return await categories
                .Select(dbRec => dbRec.Id)
                .ToArrayAsync();
        }
    }
}