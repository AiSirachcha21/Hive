using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Users.Commands.AddRoleToUser
{
    public class AddRoleToUserCommandValidator : AbstractValidator<AddRoleToUserCommand>
    {
        private ApplicationDbContext _context;

        public AddRoleToUserCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .MustAsync(BeValidUserId).WithMessage("User with that ID does not exist.");

            RuleFor(c => c.Role)
                .NotEmpty().WithMessage("Role is required")
                .MustAsync(BeValidUserRole).WithMessage("Role doesn't exist");

            RuleFor(c => c)
                .MustAsync(BeUniqueUserRole).WithMessage("User with Role already exists");
        }

        private async Task<bool> BeUniqueUserRole(AddRoleToUserCommand command, CancellationToken cancellationToken)
        {
            var commandRoleId = (await _context.Roles.FirstAsync(r => r.Name == command.Role)).Id;
            return !(await _context.UserRoles.AnyAsync(ur => ur.UserId == command.UserId && ur.RoleId == commandRoleId));
        }

        private async Task<bool> BeValidUserRole(string role, CancellationToken cancellationToken)
            => await _context.Roles.AnyAsync(r => r.Name == role);

        private async Task<bool> BeValidUserId(string id, CancellationToken cancellationToken)
            => await _context.Users.AnyAsync(u => u.Id == id);
    }
}
