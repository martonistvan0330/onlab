using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryManager categoryManager;
        public CategoryController(CategoryManager categoryManager) => this.categoryManager = categoryManager;

        [HttpGet("main")]
        public async Task<IEnumerable<Category>> GetMainCategories()
            => await categoryManager.ListMainCategories();

        [HttpGet()]
        public async Task<IEnumerable<Category>> GetSubcategoriesByParentCategory([FromQuery] string parentCategoryName)
            => await categoryManager.ListSubcategoriesByParentCategory(parentCategoryName);
        /*{
            var categories = dbContext.Category.Where(c => c.ParentCategoryId == parentCategoryId);
            
            return categories
                    .Select(c => new Models.Category(c.Id, c.Name))
                    .ToArray();
        }*/
    }
}
