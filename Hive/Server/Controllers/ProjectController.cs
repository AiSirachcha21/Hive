using Hive.Domain;
using Hive.Server.Application.Projects.Commands.AddUserToProject;
using Hive.Server.Application.Projects.Commands.CreateProject;
using Hive.Server.Application.Projects.Commands.DeleteProject;
using Hive.Server.Application.Projects.Commands.UpdateProject;
using Hive.Server.Application.Projects.Queries.GetProject;
using Hive.Server.Application.Projects.Queries.GetProjectUsersByProjectId;
using Hive.Server.Application.Projects.Queries.GetUserProjects;
using Hive.Shared.Projects.Commands;
using Hive.Shared.Projects.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ApiController
    {
        [HttpGet]
        [Route("{projectId:Guid}")]
        public async Task<ActionResult<ProjectViewModel>> Get(Guid projectId)
        {
            var result = await Mediator.Send(new GetProjectQuery(projectId));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IList<ProjectViewModel>>> GetUserProjects(Guid organizationId)
        {
            var result = await Mediator.Send(new GetUserProjectsQuery(organizationId, UserId));

            if (result.Count == 0 || result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<ActionResult<CreateProjectViewModel>> CreateProject(CreateProjectRequestModel request)
        {
            var (organizationId, name, description, projectOwnerId) = request;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CreateProjectCommand command = new(organizationId, name, description, projectOwnerId, userId);

            var result = await Mediator.Send(command);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet]
        [Route("{projectId:Guid}/users")]
        public async Task<ActionResult<List<ApplicationUser>>> GetUsers(Guid projectId)
        {
            var result = await Mediator.Send(new GetUsersByProjectIdQuery(projectId));
            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid projectId)
        {
            var result = await Mediator.Send(new DeleteProjectCommand(projectId));
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddUsers(AddUserToProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
