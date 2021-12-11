using Hive.Server.Application.Organizations.Commands.CreateOrganization;
using Hive.Server.Application.Organizations.Queries.GetOrganizations;
using Hive.Shared.Organizations.CommandViewModels;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IList<OrganzationViewModel>>> GetByUser()
        {
            var result = await Mediator.Send(new GetOrganizationsQuery(UserId));
            if (result == null || result.Count == 0) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrganizationViewModel>> Create([FromBody] string organizationName)
        {
            var command = new CreateOrganizationCommand(orgName: organizationName, userId: UserId);
            return Ok(await Mediator.Send(command));
        }
    }
}
