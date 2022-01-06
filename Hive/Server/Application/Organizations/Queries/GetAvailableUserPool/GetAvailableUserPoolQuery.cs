using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Server.Infrastructure;
using Hive.Shared.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.GetAvailableUserPool
{
    public record GetAvailableUserPoolQuery(Guid OrganizationId) : IRequest<List<UserViewModel>>;

    public class GetAvailableUserPoolQueryHandler : IRequestHandler<GetAvailableUserPoolQuery, List<UserViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAvailableUserPoolQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserViewModel>> Handle(GetAvailableUserPoolQuery request, CancellationToken cancellationToken)
        {
            var organizationUsers = await _context.OrganizationUsers.Where(ou => ou.OrganizationId == request.OrganizationId).Select(ou => ou.MemberId).ToListAsync();
            var users = await _context.Users
                .Where(p => organizationUsers.All(p2 => p2 != p.Id))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }
    }
}
