using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Projects.Queries.GetProjectStatisticsOverview;
using Hive.Server.Application.Projects.Queries.GetUsersByProjectId;
using Hive.Server.Infrastructure;
using Hive.Shared.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Queries.GetProject
{
    public record GetProjectQuery(Guid projectId) : IRequest<ProjectViewModel>;

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IMediator mediator;

        public GetProjectQueryHandler(ApplicationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<ProjectViewModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(request.projectId);
            var organization = await context.Organizations.FindAsync(project.OrganizationId);
            var members = await mediator.Send(new GetUsersByProjectIdQuery(request.projectId));
            var projectStatistics = await mediator.Send(new GetProjectStatisticsOverviewQuery(request.projectId));

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
