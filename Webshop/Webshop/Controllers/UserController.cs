using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.Models;

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

        [HttpGet]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            if (await userManager.CheckLogin(login.Username, login.Password))
            {
                return Ok();
            }
            else
            {
                return BadRequest("invalid username or password");
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> AddUser([FromBody] NewUser newUser)
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
