using Hive.Server.Infrastructure;
using Hive.Shared.Tickets.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Queries.GetTicketsByProjectIdForUser
{
    public record GetTicketsByProjectIdForUserQuery(Guid ProjectId, string UserId) : IRequest<List<TicketViewModel>>;

    public class TicketsByProjectIdForUserQueryHandler : IRequestHandler<GetTicketsByProjectIdForUserQuery, List<TicketViewModel>>
    {
        private readonly ApplicationDbContext _context;

        public TicketsByProjectIdForUserQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<TicketViewModel>> Handle(GetTicketsByProjectIdForUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
