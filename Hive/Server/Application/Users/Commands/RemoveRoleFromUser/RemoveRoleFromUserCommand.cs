using Hive.Domain;
using Hive.Server.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Users.Commands.RemoveRoleFromUser
{
    public record RemoveRoleFromUserCommand(string UserId, string Role) : IRequest<bool>;

    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RemoveRoleFromUserCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            var result = await _userManager.RemoveFromRoleAsync(user, request.Role);
            await _context.SaveChangesAsync();

            return result.Succeeded;
        }
    }
}
