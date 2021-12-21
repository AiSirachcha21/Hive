using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Tickets.Commands.CreateTicket;
using Hive.Shared.Tickets.Queries;
using System;

namespace Hive.Server.Application.Common.Mapping
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<TicketViewModel, Ticket>();

            CreateMap<CreateTicketCommand, Ticket>()
                .ForMember(dto => dto.Id, target => target.MapFrom(t => Guid.NewGuid()))
                .ForMember(dto => dto.TicketStatus, target => target.MapFrom(t => TicketStatus.NotStarted));
        }
    }
}
