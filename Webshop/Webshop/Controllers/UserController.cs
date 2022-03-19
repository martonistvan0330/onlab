using Microsoft.AspNetCore.Mvc;
using Webshop.DAL.EF;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly WebshopDbContext dbContext;
        public UserController(WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{userId}")]
        public ActionResult<Models.User> GetUser([FromRoute] int userId)
        {
            var dbUser = dbContext.User.SingleOrDefault(u => u.Id == userId);

            if (dbUser != null)
            {
                return new Models.User()
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    Username = dbUser.Username,
                    Password = dbUser.Password,
                };
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Login([FromBody] Models.Login login)
        {
            var dbUser = dbContext.User.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (dbUser != null)
            {
                return Ok(new {dbUser.Id});
            }
            else
            {
                return BadRequest("wrong username or password");
            }
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] Models.NewUser newUser)
        {
            if (dbContext.User.All(u => u.Username != newUser.Username))
            {
                var dbUser = new DAL.User()
                {
                    Email = newUser.Email,
                    Username = newUser.Username,
                    Password = newUser.Password,
                };

                dbContext.User.Add(dbUser);
                dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetUser), new { userId = dbUser.Id }, new Models.User()
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    Username = dbUser.Username,
                    Password = dbUser.Password,
                });
            }
            else
            {
                return BadRequest("user already exists");
            }
            
        }
    }
}
