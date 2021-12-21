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
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateProjectViewModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = _mapper.Map<CreateProjectCommand, Project>(request);
            ProjectUser initialUser = new()
            {
                Id = Guid.NewGuid(),
                MemberId = request.UserId,
                ProjectId = project.Id
            };

            List<ProjectUser> projectUsers = new() { initialUser };
            project.ProjectUsers = projectUsers;


            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateProjectViewModel>(project);
        }
    }
}
