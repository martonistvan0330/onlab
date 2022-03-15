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

        [HttpPost]
        public ActionResult AddCustomer([FromBody] Models.Customer customer)
        {
            bool isNewMainCustomer = !dbContext.Customer.Any(c => c.MainCustomer == true && c.UserId == customer.UserId);

            var shippingAddress = customer.ShippingInfo.ShippingAddress;

            var dbShippingAddress = new DAL.Address()
            {
                FirstName = shippingAddress.FirstName,
                LastName = shippingAddress.LastName,
                Country = shippingAddress.Country,
                Region = shippingAddress.Region,
                City = shippingAddress.City,
                ZipCode = shippingAddress.ZipCode,
                Street = shippingAddress.Street,
                PhoneNumber = shippingAddress.PhoneNumber,
            };
            
            dbContext.Address.Add(dbShippingAddress);
            dbContext.SaveChanges();

            var dbShippingInfo = new DAL.ShippingInfo()
            {
                ShippingMethodId = customer.ShippingInfo.Method.Id,
                ShippingAddressId = dbShippingAddress.Id,
            };

            dbContext.ShippingInfo.Add(dbShippingInfo);
            dbContext.SaveChanges();

            var billingAddress = customer.PaymentInfo.BillingAddress;

            var dbBillingAddress = new DAL.Address()
            {
                FirstName = billingAddress.FirstName,
                LastName = billingAddress.LastName,
                Country = billingAddress.Country,
                Region = billingAddress.Region,
                City = billingAddress.City,
                ZipCode = billingAddress.ZipCode,
                Street = billingAddress.Street,
                PhoneNumber = billingAddress.PhoneNumber,
            };

            dbContext.Address.Add(dbBillingAddress);
            dbContext.SaveChanges();

            var dbPaymentInfo = new DAL.PaymentInfo()
            {
                PaymentMethodId = customer.PaymentInfo.Method.Id,
                BillingAddressId = dbBillingAddress.Id,
            };

            dbContext.PaymentInfo.Add(dbPaymentInfo);
            dbContext.SaveChanges();

            var dbCustomer = new DAL.Customer()
            {
                UserId = customer.UserId,
                Name = customer.Name,
                ShippingInfoId = dbShippingInfo.Id,
                PaymentInfoId = dbPaymentInfo.Id,
                MainCustomer = isNewMainCustomer,
            };

            dbContext.Customer.Add(dbCustomer);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
