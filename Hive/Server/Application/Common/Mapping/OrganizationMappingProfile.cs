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
        }
    }
}
