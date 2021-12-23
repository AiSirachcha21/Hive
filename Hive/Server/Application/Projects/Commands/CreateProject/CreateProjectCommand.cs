using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Common.Exceptions;
using Hive.Server.Application.Users.Commands.AddRoleToUser;
using Hive.Server.Infrastructure;
using Hive.Shared;
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
        private readonly IMediator _mediator;

        public CreateProjectCommandHandler(ApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
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

            /// Add Project before adding Role to user because the DbContext.SaveChanges call is made inside the AddRoleToUser command.
            await _context.Projects.AddAsync(project, cancellationToken);

            /// Call AddToRoleCommand OR SaveChangesAsync call. If the first is called then the SaveChanges call doesn't need to be made
            /// since it's being called during the Mediator call.
            if (!string.IsNullOrEmpty(request.ProjectOwnerId))
            {
                AddRoleToUserCommand command = new(request.ProjectOwnerId, UserRoles.ProjectOwner);
                bool addToRoleResult = await _mediator.Send(command);

                if (!addToRoleResult) throw new ValidationException(new string[] { "Failed to create user role" });
            }
            else await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateProjectViewModel>(project);
        }
    }
}
