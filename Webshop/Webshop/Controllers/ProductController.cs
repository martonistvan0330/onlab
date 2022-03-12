using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<Models.Product[]> GetMainPageProducts()
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
                    .Select(p => new Models.Product()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                    })
                    .ToArray();
        }

        [HttpGet]
        public ActionResult<Models.Product[]> GetProducts([FromQuery] int categoryId, [FromQuery] double minPrice, [FromQuery] double maxPrice, [FromQuery] string? sizes)
        {
            
            var categoryIds = dbContext.Category
                .Where(c => c.Id == categoryId || c.ParentCategory.Id == categoryId || c.ParentCategory.ParentCategory.Id == categoryId)
                .Select(c => c.Id)
                .ToArray();

            string[] sizeArray = sizes.Split(',');

            var filteredProducts = dbContext.Product
                .Where(p => categoryIds.Contains(p.Category.Id))
                .Where(p => p.Price >= minPrice)
                .Where(p => p.ProductStocks.Any(ps => sizeArray.Contains(ps.Size.Name) && ps.Stock > 0));

            if (maxPrice > 0)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice);
            }

            return filteredProducts
                    .Select(p => new Models.Product()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                    })
                    .ToArray();
        }

        [HttpGet("{productId}/sizes")]
        public ActionResult<string[]> GetSizes([FromRoute] int productId)
        {
            var sizes = dbContext.ProductStock
                .Where(ps => ps.Product.Id == productId)
                .Where(ps => ps.Stock > 0)
                .Select(p => p.Size.Name);
            if (sizes.Count() > 0)
            {
                return sizes.ToArray();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{productId}/stock")]
        public ActionResult<int> GetStock([FromRoute] int productId, [FromQuery] string size)
        {
            var product = dbContext.ProductStock
                .Where(ps => ps.Product.Id == productId)
                .FirstOrDefault();
            if (product != null)
            {
                return dbContext.ProductStock
                .Where(ps => ps.Product.Id == productId)
                .Where(ps => ps.Size.Name.Equals(size))
                .Select(p => p.Stock)
                .SingleOrDefault(); ;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
