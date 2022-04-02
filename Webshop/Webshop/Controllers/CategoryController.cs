using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Controllers
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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategoriesByParentCategory([FromQuery] string parentCategoryName)
        {
            var categories = await categoryManager.ListSubcategoriesByParentCategory(parentCategoryName);
            if (categories.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(categories);
            }
        }
    }
}
