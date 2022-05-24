using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryManager categoryManager;
        public CategoryController(CategoryManager categoryManager) => this.categoryManager = categoryManager;

        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetCategories()
        {
            return Ok(await categoryManager.GetCategories());
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Category>> GetCategory([FromRoute] int categoryId)
		{
            var category = await categoryManager.GetCategory(categoryId);
            if (category != null)
			{
                return Ok(category);
			}
            else
			{
                return NotFound();
            }
		}

        [HttpGet("main")]
        public async Task<IEnumerable<Category>> GetMainCategories()
            => await categoryManager.ListMainCategories();

        [HttpGet("{parentCategoryId}/subcategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategoriesByParentCategory([FromRoute] int parentCategoryId)
        {
            try
			{
                var categories = await categoryManager.ListSubcategoriesByParentCategory(parentCategoryId);
                return Ok(categories);
            }
            catch
			{
                return NotFound();
			}
        }
    }
}
