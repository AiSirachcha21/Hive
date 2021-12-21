using Hive.Server.Application.Tickets.Commands.CreateTicket;
using Hive.Server.Application.Tickets.Commands.DeleteTicket;
using Hive.Server.Application.Tickets.Commands.UpdateTicket;
using Hive.Server.Application.Tickets.Queries.GetTicketsByProjectId;
using Hive.Shared.Tickets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ApiController
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Unit>> Create([FromBody] CreateTicketCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult<Unit>> Update([FromBody] UpdateTicketCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("{projectId:Guid}")]
        public async Task<ActionResult<List<TicketViewModel>>> GetTicketsByProjectId(Guid projectId)
        {
            var result = await Mediator.Send(new GetTicketsByProjectIdQuery(projectId));

            if (result.Count == 0 || result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTicketCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
