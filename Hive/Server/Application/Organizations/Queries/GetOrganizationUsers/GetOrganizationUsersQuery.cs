using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Shared.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.GetOrganizationUsers
{
    public record GetOrganizationUsersQuery(Guid OrganizationId) : IRequest<List<UserViewModel>>;

    public class GetOrganizationUsersQueryHandler : IRequestHandler<GetOrganizationUsersQuery, List<UserViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationUsersQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserViewModel>> Handle(GetOrganizationUsersQuery request, CancellationToken cancellationToken)
        {
            List<string> orgUsers = await _context.OrganizationUsers
                .Where(p => p.OrganizationId == request.OrganizationId)
                .Select(o => o.MemberId)
                .ToListAsync(cancellationToken);
            List<UserViewModel> orgFullUsers = await _context
                .Users.Where(u => orgUsers.Any(id => id == u.Id))
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return orgFullUsers;
        }
    }
}
