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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<Order>> GetOrder([FromQuery] string userId)
            => await orderManager.GetOrders(userId);

        [HttpGet("{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderDetails>> GetOrder([FromRoute] int orderId, [FromQuery] string userId)
        {
            var (success, order) = await orderManager.GetOrder(orderId, userId);
            if (success)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<int>> CreateOrder([FromQuery] string userId, [FromQuery] int customerId)
        {
            var (success, orderId) = await orderManager.TryCreateOrder(userId, customerId);
            if (success)
            {
                return Ok(orderId);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPatch("{orderId}/cancel")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> CancelOrder([FromRoute] int orderId,[FromQuery] string userId)
        {
            try
			{
                var success = await orderManager.TryCancelOrder(orderId, userId);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception e)
			{
                return NotFound(e.Message);
			}
        }
    }
}
