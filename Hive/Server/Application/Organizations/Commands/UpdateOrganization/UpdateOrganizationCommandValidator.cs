using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateOrganizationCommandValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(c => c.Data.Id)
                .NotEmpty().WithMessage("An organization ID must be provided")
                .MustAsync(BeAValidOrganizationId).WithMessage("Organization does not exist");

            RuleFor(c => c.Data.Name)
                .NotEmpty().WithMessage("The organization name should not be empty")
                .MustAsync(BeUniqueName).WithMessage("An organization with this name already exists");
        }

        private async Task<bool> BeUniqueName(string newOrgName, CancellationToken arg2)
            => await _context.Organizations.AllAsync(o => o.Name != newOrgName);

        private async Task<bool> BeAValidOrganizationId(Guid organizationId, CancellationToken arg2)
            => await _context.Organizations.AnyAsync(o => o.Id == organizationId);
    }
}
