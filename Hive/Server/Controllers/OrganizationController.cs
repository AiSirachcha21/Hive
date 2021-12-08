using Hive.Server.Application.Organizations.Queries.GetOrganizations;
using Hive.Shared.Organizations.QueryDtos;
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
        public async Task<ActionResult<IList<OrganzationDto>>> Get(string userId)
        {
            var result = await Mediator.Send(new GetOrganizationsQuery(userId));
            if (result == null) return NoContent();

            return Ok(result);
        }
    }
}
