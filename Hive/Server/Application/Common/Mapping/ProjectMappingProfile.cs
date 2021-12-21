using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Projects.Commands.CreateProject;
using Hive.Shared.Projects.Commands;
using System;

namespace Hive.Server.Application.Common.Mapping
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectCommand, Project>()
                .ForMember(dto => dto.Id, target => target.MapFrom(t => Guid.NewGuid()))
                .ForMember(dto => dto.ProjectUsers, target => target.Ignore());

            CreateMap<Project, CreateProjectViewModel>();
        }
    }
}
