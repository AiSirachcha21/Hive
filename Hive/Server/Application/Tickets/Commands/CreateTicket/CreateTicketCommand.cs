using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(string Title, string Description, string AssignedUserId, Guid ProjectId) : IRequest<Unit>;

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Unit>
    {
        private readonly ApplicationDbContext context;

        public CreateTicketCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                AssignedUserId = request.AssignedUserId,
                TicketStatus = TicketStatus.NotStarted,
            };

            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
