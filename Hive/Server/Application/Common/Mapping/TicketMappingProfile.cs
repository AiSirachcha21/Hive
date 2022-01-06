using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Tickets.Commands.CreateTicket;
using Hive.Server.Application.Tickets.Commands.UpdateTicket;
using Hive.Shared.Tickets.Queries;
using System;
using System.Linq;

namespace Hive.Server.Application.Common.Mapping
{
    public class TicketMappingProfile : Profile
    {
        private const string _ticketDateTimeFormat = "MMM dd, yyyy";

        public TicketMappingProfile()
        {
            CreateMap<TicketViewModel, Ticket>()
                .ForMember(dto => dto.LastModfied, target => target.MapFrom(t => DateTime.Now));

            CreateMap<Ticket, TicketViewModel>()
                .ForMember(dto => dto.AssignedUserName, target => target.Ignore())
                .ForMember(dto => dto.LastUpdated, target => target.MapFrom(t => t.LastModfied.ToString(_ticketDateTimeFormat)));

            CreateMap<ApplicationUser, TicketUserViewModel>()
                .ForMember(dto => dto.Name, target => target.MapFrom(t => $"{t.FirstName} {t.LastName.First()}"));

            CreateMap<CreateTicketCommand, Ticket>()
                .ForMember(dto => dto.Id, target => target.MapFrom(t => Guid.NewGuid()))
                .ForMember(dto => dto.TicketStatus, target => target.MapFrom(t => TicketStatus.NotStarted));

            CreateMap<UpdateTicketCommand, Ticket>()
                .ForMember(c => c.TicketStatus, target => target.MapFrom(t => t.Status));
        }
    }
}
