using Hive.Domain;
using Hive.Server.Application.Authentication.Commands.Login;
using Hive.Server.Application.Authentication.Commands.Register;
using Hive.Server.Application.Authentication.Queries.GetCurrentUser;
using Hive.Shared.Login;
using Hive.Shared.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse res = await Mediator.Send(new LoginCommand(request, _userManager, _signInManager));
            int errorCode = res.StatusCode;
            IActionResult response = errorCode switch
            {
                400 => BadRequest(res.Message),
                _ => Ok(res.Message),
            };
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var response = await Mediator.Send(new RegisterCommand(request, _userManager, _signInManager));
            return response.Errors != null ? BadRequest(response.Errors) : Ok(response.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<CurrentUser> GetCurrentUserInfo() 
            => await Mediator.Send(new GetCurrentUserQuery(User));
    }
}
