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

        public async Task<IReadOnlyCollection<Models.Category>> ListSubcategoriesByParentCategory(int parentCategoryId)
        {
            var parentCategory = await dbContext.Category
                                    .GetByIdOrNull(parentCategoryId);
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

        public async Task<IReadOnlyCollection<int>> GetCategoryIdsByParentCategory(int categoryId)
        {
            var category = await dbContext.Category.GetByIdOrNull(categoryId);
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
