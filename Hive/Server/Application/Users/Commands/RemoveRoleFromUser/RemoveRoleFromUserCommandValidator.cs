using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Users.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandValidator : AbstractValidator<RemoveRoleFromUserCommand>
    {
        private readonly ApplicationDbContext _context;

        public RemoveRoleFromUserCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c)
                .MustAsync(BeAUserRole).WithMessage("User with Role does not exist");
        }

        private async Task<bool> BeAUserRole(RemoveRoleFromUserCommand command, CancellationToken cancellationToken)
        {
            var commandRoleId = (await _context.Roles.FirstAsync(r => r.Name == command.Role)).Id;
            return await _context.UserRoles.AnyAsync(ur => ur.UserId == command.UserId & ur.RoleId == commandRoleId);
        }
    }
}
