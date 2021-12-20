using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Tickets.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Queries.GetTicketsByProjectId
{
    public record GetTicketsByProjectIdQuery(Guid ProjectId) : IRequest<List<TicketViewModel>>;

    public class GetByProjectIdQueryHandler : IRequestHandler<GetTicketsByProjectIdQuery, List<TicketViewModel>>
    {
        private readonly ApplicationDbContext context;
        private readonly MapperConfiguration mapperConfiguration;
        private const string TicketDateTimeFormat = "MMM dd, yyyy";

        public GetByProjectIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketViewModel>()
                .ForMember(dto => dto.AssignedUserName, target => target.Ignore())
                .ForMember(dto => dto.LastUpdated, target => target.MapFrom(t => t.LastModfied.ToString(TicketDateTimeFormat)));

                cfg.CreateMap<ApplicationUser, TicketUserViewModel>()
                .ForMember(dto => dto.Name, target => target.MapFrom(t => $"{t.FirstName} {t.LastName.First()}"));
            });
        }

        public async Task<List<TicketViewModel>> Handle(GetTicketsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = await context.Tickets.Where(t => t.ProjectId == request.ProjectId)
                .ProjectTo<TicketViewModel>(mapperConfiguration)
                .ToListAsync();
            return tickets;
        }
    }
}
