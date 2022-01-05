using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Application.Projects.Queries.GetProject;
using Hive.Server.Application.Projects.Queries.GetProjectStatisticsOverview;
using Hive.Server.Application.Projects.Queries.GetProjectUsersByProjectId;
using Hive.Server.Infrastructure;
using Hive.Shared.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Queries.GetUserProjects
{
    public record GetUserProjectsQuery(Guid OrganizationId, string UserId) : IRequest<IList<ProjectViewModel>>;

    public class GetUserProjectsQueryHandler : IRequestHandler<GetUserProjectsQuery, IList<ProjectViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public GetUserProjectsQueryHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IList<ProjectViewModel>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Where(p => p.OrganizationId == request.OrganizationId && p.ProjectUsers.Any(pu => pu.MemberId == request.UserId))
                .ToListAsync();

            List<ProjectViewModel> vms = new();
            int index = 0;

            while (vms.Count != projects.Count)
            {
                var vm = await GetFullProjectData(projects[index]);
                vms.Add(vm);
                index += 1;
            }

            return vms;
        }

        public async Task<ProjectViewModel> GetFullProjectData(Project project)
        {
            var organization = await _context.Organizations.FindAsync(project.OrganizationId);
            var members = await _mediator.Send(new GetUsersByProjectIdQuery(project.Id));
            var projectStatistics = await _mediator.Send(new GetProjectStatisticsOverviewQuery(project.Id));

            return new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                NameInitial = project.Name.First(),
                Description = project.Description,
                OrganizationName = organization.Name,
                OrganizationId = organization.Id,
                Members = members,
                ProjectStatistics = projectStatistics,
                CreatedAt = project.CreatedAt
            };
        }
    }
}
