using AutoMapper;
using Hive.Domain;
using Hive.Server.Application.Common.Exceptions;
using Hive.Server.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.AddUserToProject
{
    public record AddUserToProjectCommand(List<string> UserIds, Guid ProjectId) : IRequest<Unit>;

    public class AddUserToProjectCommandHandler : IRequestHandler<AddUserToProjectCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddUserToProjectCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);

            List<ProjectUser> users = users = _mapper.Map<List<string>, List<ProjectUser>>(request.UserIds);
            users.ForEach(u => u.ProjectId = request.ProjectId);

            await _context.ProjectUsers.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
