using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Webshop.DAL.EF;
using Webshop.Web.Server.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/admin/products")]
    [ApiController]
    public class AdminProductsController : Controller
    {
        private readonly WebshopDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminProductsController(
            WebshopDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost("{productId}/addimage")]
        public async Task<IActionResult> AddProductImage([FromForm] IEnumerable<IFormFile> files, [FromRoute] int productId, [FromQuery] string userId) 
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        var productImage = new ProductImage();
                        productImage.FileName = file.FileName;
                        productImage.ProductId = productId;
                        productImage.Image = fileBytes;
                        productImage.MainImage = false;
                        dbContext.ProductImage.Add(productImage);
                        dbContext.SaveChanges();
                    }
                }
            }
            return Ok();
        }
    }
}
