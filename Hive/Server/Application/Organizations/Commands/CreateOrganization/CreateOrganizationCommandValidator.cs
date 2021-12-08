using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateOrganizationCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.orgName)
                .NotEmpty().WithMessage("Organization Name is Compulsory")
                .MustAsync(NotBeDuplicateOrgName).WithMessage("Organization Name already exists. Try making the name more unique");

            RuleFor(c => c.userId)
                .NotEmpty()
                .MustAsync(HaveValidUser).WithMessage("User does not exist");
        }

        private async Task<bool> NotBeDuplicateOrgName(string organizationName, CancellationToken cancellationToken)
            => await _context.Organizations.AllAsync(o => o.Name != organizationName, cancellationToken);

        private async Task<bool> HaveValidUser(string userId, CancellationToken cancellationToken)
            => await _context.Users.AnyAsync(o => o.Id == userId, cancellationToken);
    }
}
