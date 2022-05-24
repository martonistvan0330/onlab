using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.BL;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/order/json")]
    [ApiController]
    public class OrderJsonController : Controller
    {
        private readonly OrderManager orderManager;
        public OrderJsonController(OrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> CreateOrder([FromQuery] string userId, [FromQuery] int customerId)
        {
            var (success, orderId) = await orderManager.TryCreateOrder(userId, customerId);
            if (success)
            {
                return Json( new { orderId = orderId });
            }
            else
            {
                return Conflict();
            }
        }
    }
}
