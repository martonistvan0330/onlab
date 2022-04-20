using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderManager orderManager;
        public OrderController(OrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<int>> AddCartItem([FromQuery] string userId, [FromQuery] int customerId)
        {
            var (success, orderId) = await orderManager.TryCreateOrder(userId, customerId);
            if (success)
            {
                return Ok(orderId);
            }
            else
            {
                return Conflict(-1);
            }
        }
    }
}
