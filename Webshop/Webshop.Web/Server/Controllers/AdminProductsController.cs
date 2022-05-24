using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Webshop.BL;
using Webshop.DAL.EF;
using Webshop.DAL.Models;
using Webshop.Web.Server.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/admin/products")]
    [ApiController]
    public class AdminProductsController : Controller
    {
        private readonly AdminProductManager productManager;
        private readonly UserController userController;
        public AdminProductsController(AdminProductManager productManager, UserController userController)
        {
            this.productManager = productManager;
            this.userController = userController;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDetailsWithSize>> GetProduct([FromRoute] int productId, [FromQuery] string userId)
        {
            var result = await userController.IsAdmin(userId);
            var okResult = (OkObjectResult)result.Result;
            if ((bool)okResult.Value)
            {
                return await productManager.GetProductWithSize(productId);
            }
            else
            {
                return Unauthorized("You are not an admin!");
            }
        }

        [HttpPut("{productId}/update")]
        public async Task<ActionResult<int>> UpdateProduct([FromBody] NewProduct newProduct, [FromRoute] int productId, [FromQuery] string userId)
        {
            var result = await userController.IsAdmin(userId);
            var okResult = (OkObjectResult)result.Result;
            if ((bool)okResult.Value)
            {
                return await productManager.UpdateProduct(productId, newProduct);
            }
            else
            {
                return Unauthorized("You are not an admin!");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> AddProduct([FromBody] NewProduct newProduct, [FromQuery] string userId)
        {
            var result = await userController.IsAdmin(userId);
            var okResult = (OkObjectResult)result.Result;
            if ((bool)okResult.Value)
            {
                return await productManager.AddProduct(newProduct);
            }
            else
            {
                return Unauthorized("You are not an admin!");
            }
        }

        [HttpPost("{productId}/addimage/{main}")]
        public async Task<ActionResult> AddProductImages([FromForm] IEnumerable<IFormFile> files, [FromRoute] int productId, [FromRoute] bool main,[FromQuery] string userId) 
        {
            var result = await userController.IsAdmin(userId);
            var okResult = (OkObjectResult)result.Result;
            if ((bool)okResult.Value)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            await productManager.AddProductImage(fileBytes, file.FileName, productId, main);
                        }
                    }
                }
                return Ok();
            }
            else
            {
                return Unauthorized("You are not an admin!");
            }
        }
    }
}
