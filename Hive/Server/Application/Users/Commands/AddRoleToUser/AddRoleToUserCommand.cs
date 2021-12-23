using Hive.Domain;
using Hive.Server.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Users.Commands.AddRoleToUser
{
    public record AddRoleToUserCommand(string UserId, string Role) : IRequest<bool>;

    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddRoleToUserCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            var identityResult = await _userManager.AddToRoleAsync(user, request.Role);
            return identityResult.Succeeded;
        }
    }


}
