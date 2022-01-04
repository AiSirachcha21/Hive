using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Shared.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.GetOrganizationSettingsData
{
    public record GetOrganizationSettingsOverviewQuery(Guid organizationId) : IRequest<OrganizationSettingsOverviewViewModel>;

    public class GetOrganizationSettingsOverviewQueryHandler : IRequestHandler<GetOrganizationSettingsOverviewQuery, OrganizationSettingsOverviewViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationSettingsOverviewQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrganizationSettingsOverviewViewModel> Handle(GetOrganizationSettingsOverviewQuery request, CancellationToken cancellationToken)
        {
            Organization org = await _context.Organizations.FindAsync(request.organizationId);
            if (org == null) return null;

            ApplicationUser systemAdmin = await _context.Users.FindAsync(org.SystemAdminId);

            List<ProjectDisplayViewModel> projects = await _context.Projects.Where(p => p.OrganizationId == org.Id).ProjectTo<ProjectDisplayViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            OrganizationSettingsOverviewViewModel mappedOrg = _mapper.Map<Organization, OrganizationSettingsOverviewViewModel>(org);
            mappedOrg.Projects = projects;
            mappedOrg.OwnerName = $"{systemAdmin.FirstName} {systemAdmin.LastName}";
            mappedOrg.OwnerEmail = systemAdmin.Email;

            return mappedOrg;
        }
    }
}
