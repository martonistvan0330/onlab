using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Controllers
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
        public async Task<ActionResult> AddCartItem([FromQuery] string sessionId, [FromQuery] int customerId)
        {
            if (await orderManager.ValidateSessionId(sessionId))
            {
                if (await orderManager.TryCreateOrder(customerId, sessionId))
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return NotFound("invalid sessionID");
            }
        }

    }
}
