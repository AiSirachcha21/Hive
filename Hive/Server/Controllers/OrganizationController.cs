using Hive.Server.Application.Organizations.Commands.CreateOrganization;
using Hive.Server.Application.Organizations.Queries.GetOrganizations;
using Hive.Shared.Organizations.CommandViewModels;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ApiController
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<IList<OrganzationViewModel>>> Get(string userId)
        {
            var result = await Mediator.Send(new GetOrganizationsQuery(userId));
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrganizationViewModel>> Create([FromBody] CreateOrganizationCommand data)
        {
            return Ok(await Mediator.Send(data));
        }
    }
}
