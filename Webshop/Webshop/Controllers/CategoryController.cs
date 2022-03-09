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

        [HttpGet]
        public ActionResult<string[]> List()
        {
            IQueryable<DAL.Category> filteredList;
            filteredList = dbContext.Category.Where(c => c.ParentCategoryId == null);

            return filteredList
                    .Select(c => c.Name)
                    .ToArray();
        }
    }
}
