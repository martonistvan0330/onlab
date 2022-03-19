using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly WebshopDbContext dbContext;
        public ProductController(WebshopDbContext dbContext)
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
        public ActionResult<Models.Product[]> GetProducts([FromQuery] int categoryId = 0, [FromQuery] double minPrice = 0, [FromQuery] double maxPrice = 0, [FromQuery] string? sizes = null, [FromQuery] int page = 1)
        {
            
            var categoryIds = dbContext.Category
                .Where(c => c.Id == categoryId || c.ParentCategory.Id == categoryId || c.ParentCategory.ParentCategory.Id == categoryId)
                .Select(c => c.Id)
                .ToArray();

            var filteredProducts = dbContext.Product
                .Where(p => categoryIds.Contains(p.Category.Id))
                .Where(p => p.Price >= minPrice);

            if (sizes != null)
            {
                string[] sizeArray = sizes.Split(',');
                filteredProducts = filteredProducts.Where(p => p.ProductStocks.Any(ps => sizeArray.Contains(ps.Size.Name) && ps.Stock > 0));
            }

            if (maxPrice > 0)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice);
            }

            var products = filteredProducts
                    .Skip((page - 1) * 6)
                    .Take(6)
                    .Select(p => new Models.Product()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                    })
                    .ToArray();
            
            if (products.Length > 0)
            {
                return products;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{productId}")]
        public ActionResult<Models.ProductDetails> GetProduct([FromRoute] int productId)
        {
            var dbProduct = dbContext.Product.SingleOrDefault(p => p.Id == productId);

            if (dbProduct != null)
            {
                return new Models.ProductDetails()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                };
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{productId}/sizes")]
        public ActionResult<string[]> GetProductSizes([FromRoute] int productId)
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
        public ActionResult<int> GetProductStock([FromRoute] int productId, [FromQuery] string size)
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
