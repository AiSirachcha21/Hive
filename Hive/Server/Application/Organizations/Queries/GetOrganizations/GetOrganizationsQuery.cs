using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.QueryViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.GetOrganizations
{
    public record GetOrganizationsQuery(string UserId) : IRequest<IList<OrganizationViewModel>>;

    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IList<OrganizationViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<OrganizationViewModel>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            List<OrganizationViewModel> orgs = await _context.Organizations
                       .Where(o => o.SystemAdminId == request.UserId)
                       .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);

            var index = 0;

            while (index != orgs.Count)
            {
                Guid orgId = orgs[index].Id;
                int projectCount = _context.Projects.Where(p => p.OrganizationId == orgId).Count();
                List<string> orgUsers = await _context.OrganizationUsers.Where(p => p.OrganizationId == orgId).Select(o => o.MemberId).ToListAsync(cancellationToken);
                List<OrganizationEmployeeViewModel> orgFullUsers = await _context
                    .Users.Where(u => orgUsers.Any(id => id == u.Id))
                    .ProjectTo<OrganizationEmployeeViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                orgs[index].Employees = orgFullUsers;
                orgs[index].ProjectCount = projectCount;
                index++;
            }

            return orgs;
        }
    }
}
