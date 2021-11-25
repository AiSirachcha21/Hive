using AutoMapper;

namespace Hive.Server.Application.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
