using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommandValidator : AbstractValidator<DeleteOrganizationCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteOrganizationCommandValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is requrired").MustAsync(BeValidOrganizationId).WithMessage("Organization with ID doens't exist.");
        }

        private async Task<bool> BeValidOrganizationId(Guid organizationId, CancellationToken arg2)
            => await _context.Organizations.AnyAsync(o => o.Id == organizationId);
    }
}
