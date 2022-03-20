﻿using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebshopDbContext dbContext;

        public CategoryRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<IReadOnlyCollection<Models.Category>> ListMainCategories()
        {
            return await dbContext.Category
                            .FindMainCategories()
                            .GetCategories();
        }

        public async Task<IReadOnlyCollection<Models.Category>> ListSubcategoriesByParentCategory(string parentCategoryName)
        {
            var parentCategory = await dbContext.Category
                                    .GetByNameOrNull(parentCategoryName);
            if(parentCategory == null)
            {
                return Array.Empty<Models.Category>();
            }
            else
            {
                return await dbContext.Category
                                .FindCategoriesByParentCategory(parentCategory)
                                .GetCategories();
            }
        }

        public async Task<IReadOnlyCollection<int>> GetCategoryIdsByParentCategory(string categoryName)
        {
            var category = await dbContext.Category.GetByNameOrNull(categoryName);
            if (category == null)
            {
                return Array.Empty<int>();
            }
            else
            {
                return await dbContext.Category
                        .FilterByParentCategory(category)
                        .GetIds();
            }
        }
    }
}
