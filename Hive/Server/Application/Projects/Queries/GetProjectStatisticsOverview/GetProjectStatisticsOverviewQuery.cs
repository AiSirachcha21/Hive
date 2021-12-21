using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Queries.GetProjectStatisticsOverview
{
    public record GetProjectStatisticsOverviewQuery(Guid ProjectId)
        : IRequest<ProjectStatisticsOverviewViewModel>;

    public class GetProjectStatisticsOverviewQueryHandler
        : IRequestHandler<GetProjectStatisticsOverviewQuery, ProjectStatisticsOverviewViewModel>
    {
        private readonly ApplicationDbContext _context;

        public GetProjectStatisticsOverviewQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectStatisticsOverviewViewModel> Handle(GetProjectStatisticsOverviewQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            var tickets = await _context.Tickets.Where(t => t.ProjectId == project.Id).ToListAsync(cancellationToken: cancellationToken);

            var ticketTimes = tickets.Select(t =>
            {
                var createdDate = t.CreatedAt.Date;
                var lastModifiedDate = t.LastModfied.Date;
                return (createdDate - lastModifiedDate).TotalHours;
            });

            decimal averageTicketCompletionTime = (ticketTimes.Any() ? (decimal)ticketTimes.Average() : 0);

            var completedTicketCount = tickets.Where(t => t.TicketStatus == TicketStatus.Completed).Count();
            var createdTicketCount = tickets.Count;


            return new ProjectStatisticsOverviewViewModel()
            {
                AverageHoursPerTicket = averageTicketCompletionTime,
                WorkItemsCreated = createdTicketCount,
                WorkItemsCompleted = completedTicketCount
            };

        }
    }
}
