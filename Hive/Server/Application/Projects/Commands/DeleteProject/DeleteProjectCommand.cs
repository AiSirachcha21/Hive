using Hive.Domain;
using Hive.Server.Application.Common.Exceptions;
using Hive.Server.Application.Users.Commands.RemoveRoleFromUser;
using Hive.Server.Infrastructure;
using Hive.Shared;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(Guid projectId) : IRequest<Unit>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DeleteProjectCommandHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _context.Projects.FindAsync(request.projectId);

            if (project.ProjectOwnerId != null)
            {
                bool removeRoleFromUserSucceeded = await _mediator.Send(new RemoveRoleFromUserCommand(project.ProjectOwnerId, UserRoles.ProjectOwner));

                if (!removeRoleFromUserSucceeded)
                    throw new ValidationException(new string[] { "User Role doesn't exist or there was a issue when attempting to delete it." });
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
