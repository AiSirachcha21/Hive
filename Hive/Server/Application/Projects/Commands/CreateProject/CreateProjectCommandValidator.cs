using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateProjectCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(c => c.OrganizationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Organization ID is required").WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .MustAsync(BeValidOrgId).WithMessage("Invalid Organization ID").WithErrorCode(StatusCodes.Status400BadRequest.ToString());
        }

        private async Task<bool> BeValidOrgId(Guid orgId, CancellationToken cancellationToken)
            => await _context.Organizations.FindAsync(orgId) != null;
    }
}
