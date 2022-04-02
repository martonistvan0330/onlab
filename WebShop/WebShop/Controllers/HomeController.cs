using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webshop.BL;
using WebShop.Web.Models;

namespace WebShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ProductManager productManager;

        public HomeController(ILogger<HomeController> logger, ProductManager productManager)
        {
            this.logger = logger;
            this.productManager = productManager;
        }

        public async Task<IActionResult> Index() => View(await productManager.GetMainPageProducts());

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}