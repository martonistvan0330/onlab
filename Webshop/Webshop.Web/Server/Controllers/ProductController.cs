using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductManager productManager;
        public ProductController(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        [HttpGet("main")]
        public async Task<IEnumerable<Product>> GetMainPageProducts()
            => await productManager.GetMainPageProducts();

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int categoryId, [FromQuery] double minPrice = 0, [FromQuery] double maxPrice = 0, [FromQuery] string? sizes = null, [FromQuery] int page = 1)
        {
            var products = await productManager.GetFilteredProducts(categoryId, minPrice, maxPrice, sizes, page);
            
            if (products.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDetails>> GetProduct([FromQuery] string productName)
        {
            var product = await productManager.GetProductDetailsOrNull(productName);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpGet("sizes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IReadOnlyCollection<string>>> GetProductSizes([FromQuery] string productName)
        {
            var sizes = await productManager.GetSizesByName(productName);
            if (sizes.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(sizes);
            }
        }

        [HttpGet("sizes/stock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IReadOnlyCollection<ProductStock>>> GetProductStocks([FromQuery] string productName)
        {
            var sizes = await productManager.GetStocksByName(productName);
            if (sizes.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(sizes);
            }
        }
    }
}
