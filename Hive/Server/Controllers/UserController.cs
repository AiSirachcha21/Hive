using Hive.Server.Application.Users.Queries;
using Hive.Shared;
using Hive.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public class UserController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetUsers()
        {
            List<UserViewModel> result = await Mediator.Send(new GetUsersQuery());
            if (result.Count == 0 || result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
