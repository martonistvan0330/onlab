using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartManager cartManager;
        public CartController(CartManager cartManager)
        {
            this.cartManager = cartManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CartItemWithId>>> GetCartItems([FromQuery] string sessionId)
        {
            if (await cartManager.ValidateSessionId(sessionId))
            {
                var cartItems = await cartManager.ListCartItems(sessionId);
                return Ok(cartItems);
            }
            else
            {
                return NotFound("invalid sessionID");
            }
        }

        [HttpPut("{cartItemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> UpdateCartItem([FromRoute]int cartItemId, [FromQuery] string sessionId, [FromQuery] int amount)
        {
            if (await cartManager.ValidateSessionId(sessionId))
            {
                if (await cartManager.TryUpdateCartItem(cartItemId, sessionId, amount))
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

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> AddCartItem([FromBody] CartItem cartItem, [FromQuery] string sessionId)
        {
            if (await cartManager.ValidateSessionId(sessionId))
            {
                if (await cartManager.TryAddCartItem(cartItem, sessionId))
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

        [HttpDelete("{cartItemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> RemoveCartItem([FromRoute] int cartItemId, [FromQuery] string sessionId)
        {
            if (await cartManager.ValidateSessionId(sessionId))
            {
                if (await cartManager.TryRemoveCartItem(cartItemId, sessionId))
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
