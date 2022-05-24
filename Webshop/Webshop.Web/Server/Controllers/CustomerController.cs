using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly CustomerManager customerManager;
        public CustomerController(CustomerManager customerManager, SessionManager sessionManager)
        {
            this.customerManager = customerManager;
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Customer>> GetCustomer([FromRoute] int  customerId, [FromQuery] string userId)
        {
            var customer = await customerManager.GetById(customerId, userId);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        /*[HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> UpdateCustomer([FromBody] Customer customer, [FromQuery] string? oldName = null)
        {
            if (await customerManager.ValidateSessionId(customer.SessionId))
            {
                if (string.IsNullOrEmpty(oldName))
                {
                    oldName = customer.Name;
                }
                if (await customerManager.ExistsByName(oldName, customer.SessionId))
                {
                    if (await customerManager.TryUpdateCustomerWithAll(customer, oldName))
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
                    return NotFound();
                }
            }
            else
            {
                return NotFound("invalid sessionID");
            }
        }*/

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<int>> AddCustomer([FromBody] Customer customer, [FromQuery] string userId)
        {
            var (success, customerId) = await customerManager.TryAddCustomerWithAll(customer, userId);
            if (success)
            {
                return Ok(customerId);
            }
            else
            {
                return Conflict();
            }
        }
    }
}
