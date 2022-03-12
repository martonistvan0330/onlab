using Microsoft.AspNetCore.Mvc;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly DAL.WebshopDbContext dbContext;
        public CategoryController(DAL.WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("main")]
        public ActionResult<Models.Category[]> GetMainCategories()
        {
            var mainCategories = dbContext.Category.Where(c => c.ParentCategory == null);

            return mainCategories
                    .Select(c => new Models.Category(c.Id, c.Name))
                    .ToArray();
        }

        [HttpGet("{parentCategoryId}")]
        public ActionResult<Models.Category[]> GetSubCategories([FromRoute] int parentCategoryId)
        {
            var categories = dbContext.Category.Where(c => c.ParentCategoryId == parentCategoryId);
            
            return categories
                    .Select(c => new Models.Category(c.Id, c.Name))
                    .ToArray();
        }
    }
}
