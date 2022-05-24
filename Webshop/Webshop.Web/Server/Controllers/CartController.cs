using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;
using Webshop.Web.Server.Models;

namespace Webshop.Web.Server.Controllers
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
        public async Task<IEnumerable<CartItem>> GetCartItems([FromQuery] string userId)
        {
            return await cartManager.ListCartItems(userId);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> UpdateCartItem([FromBody] UpdateCartItem cartItem, [FromQuery] string userId)
        {
            if (await cartManager.TryUpdateCartItem(cartItem, userId))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> AddCartItem([FromBody] NewCartItem cartItem, [FromQuery] string userId)
        {
            if (cartItem.Amount <= 0)
			{
                return BadRequest();
			}
            try {
                if (await cartManager.TryAddCartItem(cartItem, userId))
                {
                    return Ok();
                }
                else
                {
                    return Conflict("something went wrong");
                }
            }
            catch (Exception e)
            { 
                return Conflict(e.Message);
            }
        }

        [HttpDelete("{cartItemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> RemoveCartItem([FromRoute] int cartItemId, [FromQuery] string userId)
        {
            if (await cartManager.TryRemoveCartItem(cartItemId, userId))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
    }
}
