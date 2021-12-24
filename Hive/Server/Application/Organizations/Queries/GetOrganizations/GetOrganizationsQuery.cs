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
    public record GetOrganizationsQuery(string UserId) : IRequest<IList<OrganzationViewModel>>;

    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IList<OrganzationViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<OrganzationViewModel>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Organizations
                       .Where(o => o.SystemAdminId == request.UserId)
                       .ProjectTo<OrganzationViewModel>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
        }
    }
}
