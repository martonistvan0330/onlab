using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly CustomerManager customerManager;
        public CustomerController(CustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

        [HttpPost]
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
                return BadRequest("invalid sessionID");
            }
        }
    }
}
