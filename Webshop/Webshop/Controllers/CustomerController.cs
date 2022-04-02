using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Controllers
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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers([FromQuery] string sessionId)
        {
            if (await customerManager.ValidateSessionId(sessionId))
            {
                var customers = await customerManager.ListCustomers(sessionId);
                return Ok(customers);
            }
            else
            {
                return NotFound("invalid sessionID");
            }
        }

        [HttpPut]
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
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (await customerManager.ValidateSessionId(customer.SessionId))
            {
                if (await customerManager.ExistsByName(customer.Name, customer.SessionId))
                {
                    return BadRequest("name not available");
                }
                else
                {
                    if (await customerManager.TryAddCustomerWithAll(customer))
                    {
                        return Ok();
                    }
                    else
                    {
                        return Conflict();
                    }
                }
            }
            else
            {
                return NotFound("invalid sessionID");
            }
        }
    }
}
