using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Projects.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(
        Guid OrganizationId,
        string Name,
        string Description,
        string ProjectOwnerId,
        string UserId)
        : IRequest<CreateProjectViewModel>;


    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly MapperConfiguration _mapperConfiguration;

        public CreateProjectCommandHandler(ApplicationDbContext context)
        {
            _context = context;
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Project, CreateProjectViewModel>());
        }

        public async Task<CreateProjectViewModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            List<ProjectUser> projectUsers = new();
            Guid projectId = Guid.NewGuid();

            var project = new Project
            {
                Id = projectId,
                Name = request.Name,
                Description = request.Description,
                OrganizationId = request.OrganizationId,
                ProjectOwnerId = request.ProjectOwnerId,
                ProjectUsers = projectUsers
            };

            project.ProjectUsers.Add(
                new()
                {
                    Id = Guid.NewGuid(),
                    MemberId = request.UserId,
                    ProjectId = projectId
                });

            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();

            return new CreateProjectViewModel { Id = project.Id, Name = project.Name };
        }
    }
}
