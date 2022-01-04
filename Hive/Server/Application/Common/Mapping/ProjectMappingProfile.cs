using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Projects.Commands.AddUserToProject;
using Hive.Server.Application.Projects.Commands.CreateProject;
using Hive.Server.Application.Projects.Commands.UpdateProject;
using Hive.Shared.Projects.Commands;
using Hive.Shared.Projects.Queries;
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
            CreateMap<UpdateProjectCommand, Project>()
                .ForMember(dto => dto.Id, target => target.MapFrom(t => t.ProjectId))
                .ForMember(dto => dto.Name, target => target.MapFrom(t => t.Name))
                .ForMember(dto => dto.Description, target => target.MapFrom(t => t.Description))
                .ForMember(dto => dto.ProjectOwnerId, target => target.MapFrom(t => t.ProjectOwnerId))
                .ReverseMap()
                .ForAllOtherMembers(members => members.Ignore());

            CreateMap<AddUserToProjectCommand, ProjectUser>()
                .ForMember(dto => dto.Id, target => target.MapFrom(t => Guid.NewGuid()))
                .ReverseMap();

            CreateMap<string, ProjectUser>()
                   .ConstructUsing(str => new ProjectUser { MemberId = str, Id = Guid.NewGuid() });

            CreateMap<Project, ProjectDisplayViewModel>();

        }
    }
}
