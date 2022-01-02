using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Users.Commands.AddRoleToUser;
using Hive.Server.Application.Users.Commands.RemoveRoleFromUser;
using Hive.Server.Infrastructure;
using Hive.Shared;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(Guid ProjectId, string Name, string Description, string ProjectOwnerId) : IRequest<Unit>;

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateProjectCommandHandler(ApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _context.Projects.FindAsync(request.ProjectId);

            if (project != null && project.ProjectOwnerId != request.ProjectOwnerId)
            {
                if (project.ProjectOwnerId != null)
                    await _mediator.Send(new RemoveRoleFromUserCommand(project.ProjectOwnerId, UserRoles.ProjectOwner));

                if (request.ProjectOwnerId != null)
                    await _mediator.Send(new AddRoleToUserCommand(request.ProjectOwnerId, UserRoles.ProjectOwner));
            }

            project = _mapper.Map(request, project);
            project.LastModfied = DateTime.Now;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
