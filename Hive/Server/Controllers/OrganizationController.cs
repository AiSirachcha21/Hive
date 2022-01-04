using Hive.Server.Application.Organizations.Commands.CreateOrganization;
using Hive.Server.Application.Organizations.Commands.DeleteOrganization;
using Hive.Server.Application.Organizations.Queries.GetOrganizations;
using Hive.Server.Application.Organizations.Queries.GetOrganizationSettingsData;
using Hive.Shared;
using Hive.Shared.Organizations.CommandViewModels;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public class OrganizationController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IList<OrganizationViewModel>>> GetByUser()
        {
            var result = await Mediator.Send(new GetOrganizationsQuery(UserId));
            if (result == null || result.Count == 0) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrganizationViewModel>> Create([FromBody] string organizationName)
        {
            var command = new CreateOrganizationCommand(OrgName: organizationName, UserId: UserId);
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOrganizationCommand(id);
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<OrganizationSettingsOverviewViewModel>> GetOrganizationSettingsModel(Guid organizationId)
        {
            var result = await Mediator.Send(new GetOrganizationSettingsOverviewQuery(organizationId));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
