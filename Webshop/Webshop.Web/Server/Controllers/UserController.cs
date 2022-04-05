using Microsoft.AspNetCore.Mvc;
using Webshop.BL;
using Webshop.DAL.Models;

namespace Webshop.Web.Server.Controllers
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<string>> Login([FromBody] Login login)
        {
            var sessionId = await userManager.Login(login.Username, login.Password);
            if (sessionId == null)
            {
                return NotFound("invalid username or password");
            }
            else
            {
                return Ok(sessionId.ToString());
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
