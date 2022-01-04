using AutoMapper;
using Hive.Domain;
using Hive.Shared.Organizations.QueryViewModels;

namespace Hive.Server.Application.Common.Mapping
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, OrganizationViewModel>();
            CreateMap<ApplicationUser, OrganizationEmployeeViewModel>();
            CreateMap<Organization, OrganizationSettingsOverviewViewModel>()
                .ForMember(o => o.OrganizationId, target => target.MapFrom(t => t.Id));

            CreateMap<UpdateOrganizationRequestViewModel, Organization>();
        }
    }
}
