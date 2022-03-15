using Microsoft.AspNetCore.Mvc;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly DAL.WebshopDbContext dbContext;
        public CustomerController(DAL.WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{customerId}")]
        public ActionResult<Models.Customer> GetCustomer([FromRoute] int customerId)
        {
            var dbCustomer = dbContext.Customer.SingleOrDefault(c => c.Id == customerId);

            if (dbCustomer != null)
            {
                return new Models.Customer()
                {
                    Id = dbCustomer.Id,
                    Email = dbCustomer.User.Email,
                    Username = dbCustomer.User.Username,
                    Password = dbCustomer.User.Password,
                    Name = dbCustomer.Name,
                    PhoneNumber = dbCustomer.PhoneNumber,
                };
            }
            else
            {
                return NotFound();
            }
        }
    }
}
