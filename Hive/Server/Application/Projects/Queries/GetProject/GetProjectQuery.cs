using Hive.Server.Application.Projects.Queries.GetProjectStatisticsOverview;
using Hive.Server.Application.Projects.Queries.GetProjectUsersByProjectId;
using Hive.Server.Infrastructure;
using Hive.Shared.Projects.Queries;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Queries.GetProject
{
    public record GetProjectQuery(Guid ProjectId) : IRequest<ProjectViewModel>;

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public GetProjectQueryHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProjectViewModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            var organization = await _context.Organizations.FindAsync(project.OrganizationId);
            var members = await _mediator.Send(new GetUsersByProjectIdQuery(request.ProjectId), cancellationToken);
            var projectStatistics = await _mediator.Send(new GetProjectStatisticsOverviewQuery(request.ProjectId), cancellationToken);

            return new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                NameInitial = project.Name.First(),
                Description = project.Description,
                OrganizationName = organization.Name,
                Members = members,
                ProjectStatistics = projectStatistics
            };

        }
    }
}
