using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Tickets.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Queries.GetTicketsByProjectId
{
    public record GetTicketsByProjectIdQuery(Guid ProjectId) : IRequest<List<TicketViewModel>>;

    public class GetTicketsByProjectIdQueryHandler : IRequestHandler<GetTicketsByProjectIdQuery, List<TicketViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTicketsByProjectIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TicketViewModel>> Handle(GetTicketsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _context.Tickets.Where(t => t.ProjectId == request.ProjectId)
                .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return tickets;
        }
    }
}
