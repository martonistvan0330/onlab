using Microsoft.AspNetCore.Mvc;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly DAL.WebshopDbContext dbContext;
        public AddressController(DAL.WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<Models.Address[]> List([FromQuery] string? search = null, [FromQuery] int from = 0)
        {
            IQueryable<DAL.Address> filteredList;

            if (string.IsNullOrEmpty(search))
                filteredList = dbContext.Address;
            else
                filteredList = dbContext.Address.Where(a => a.Name.Contains(search));

            return filteredList
                    .Skip(from) // lapozashoz: hanyadik termektol kezdve
                    .Take(5) // egy lapon max 5 termek
                    .Select(a => new Models.Address(a.Id, a.Name, a.Country, a.Region, a.ZipCode, a.City, a.Street)) // adatbazis entitas -> DTO
                    .ToArray(); // a fenti IQueryable kiertekelesesen kieroltetese, kulonben hibara futnank
        }
    }
}
