using AutoMapper;
using Hive.Domain;
using Hive.Shared.Users;

namespace Hive.Server.Application.Common.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
