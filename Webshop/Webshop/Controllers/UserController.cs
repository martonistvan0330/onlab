using Microsoft.AspNetCore.Mvc;
using Webshop.BL;

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager userManager;
        public UserController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        /*[HttpGet("{userId}")]
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
        }*/

        /*[HttpGet]
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
        }*/

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> AddUser([FromBody] Models.NewUser newUser)
        {
            if ((await userManager.ExistsByUsername(newUser.Username)))
            {
                return BadRequest("username not available");
            } else
            {
                if (await userManager.TryAddUser(newUser))
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
}
