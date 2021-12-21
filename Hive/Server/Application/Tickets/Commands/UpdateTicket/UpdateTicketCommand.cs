using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Tickets.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.UpdateTicket
{
    public record UpdateTicketCommand(
        Guid Id,
        string Title,
        string Description,
        string AssignedUserId,
        TicketStatus Status) : IRequest<Unit>;

    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTicketCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket originalTicket = await _context.Tickets.FindAsync(request.Id);
            Ticket newTicket = _mapper.Map(request, originalTicket);

            newTicket.LastModfied = DateTime.Now;

            _context.Tickets.Update(newTicket);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
