using Hive.Domain;
using Hive.Server.Application.Common.Exceptions;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.CommandViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.AddUserToOrganization
{
    public record AddUserToOrganizationCommand(string UserId, Guid OrganizationId) : IRequest<Unit>;

    public class AddUserToOrganizationCommandHandler : IRequestHandler<AddUserToOrganizationCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddUserToOrganizationCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(AddUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations.FindAsync(request.OrganizationId);
            var user = await _context.Users.FindAsync(request.UserId);

            await _context.OrganizationUsers.AddAsync(new OrganizationUser
            {
                OrganizationId = organization.Id,
                Id = Guid.NewGuid(),
                MemberId = user.Id
            });

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
