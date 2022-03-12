using Microsoft.AspNetCore.Mvc;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly DAL.WebshopDbContext dbContext;
        public ProductController(DAL.WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("main")]
        public ActionResult<Models.MainPageProduct[]> GetMainPageProducts()
        {
            var productCount = dbContext.Product.Count();
            var random = new Random();
            var ids = new List<int>();
            while (ids.Count < 6)
            {
                int id = random.Next(0, productCount);
                if (!ids.Contains(id))
                {
                    ids.Add(id);
                }
            }

            var products = dbContext.Product.Where(p => ids.Contains(p.Id));

            return products
                    .Select(p => new Models.MainPageProduct(p.Id, p.Name, p.Price))
                    .ToArray();
        }
    }
}
