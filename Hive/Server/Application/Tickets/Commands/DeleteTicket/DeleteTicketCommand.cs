using Hive.Domain;
using Hive.Server.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.DeleteTicket
{
    public record DeleteTicketCommand(Guid Id) : IRequest<Unit>;

    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTicketCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket ticket = await _context.Tickets.FindAsync(request.Id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
