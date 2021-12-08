using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.QueryViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.GetOrganizations
{
    public class GetOrganizationsQuery : IRequest<IList<OrganzationViewModel>>
    {
        public GetOrganizationsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IList<OrganzationViewModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly MapperConfiguration _mapperConfiguration;

            public GetOrganizationsQueryHandler(ApplicationDbContext context)
            {
                _context = context;

                _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Organization, OrganzationViewModel>()
                    .ForMember(dto => dto.Id, x => x.MapFrom(p => p.Id))
                    .ForMember(dto => dto.Name, x => x.MapFrom(p => p.Name)));
            }

            public async Task<IList<OrganzationViewModel>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
            {
                var orgs = await _context.Organizations
                    .Where(o => o.SystemAdminId == request.UserId)
                    .ProjectTo<OrganzationViewModel>(_mapperConfiguration)
                    .ToListAsync(cancellationToken);

                return orgs;
            }
        }
    }


}
