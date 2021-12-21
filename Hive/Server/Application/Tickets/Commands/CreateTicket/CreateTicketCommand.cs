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
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Ticket>(request);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
