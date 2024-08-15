using Application.User.Commands;
using Application.Users.IServices;
using Application.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSignUpService userSignUpService;

        public UserController(IUserSignUpService userSignUpService)
        {
            this.userSignUpService = userSignUpService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await userSignUpService.RegisterUserAsync(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
