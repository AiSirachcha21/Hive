using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Tickets.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(string Title, string Description, string AssignedUserId, Guid ProjectId) : IRequest<TicketViewModel>;

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TicketViewModel> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                AssignedUserId = request.AssignedUserId,
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                ProjectId = request.ProjectId,
                TicketStatus = TicketStatus.NotStarted,
                Title = request.Title,
                LastModfied = DateTime.UtcNow,
            };

            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var ticketOwner = ticket.AssignedUserId == null ? null : await _context.Users.FindAsync(ticket.AssignedUserId);

            return new TicketViewModel
            {
                AssignedUserId = ticket.AssignedUserId,
                AssignedUserName = ticketOwner != null ? $"{ticketOwner.FirstName} {ticketOwner.LastName}" : null,
                Description = ticket.Description,
                Id = ticket.Id,
                LastUpdated = ticket.LastModfied.ToShortDateString(),
                Status = ticket.TicketStatus,
                Title = ticket.Title
            };
        }
    }
}
