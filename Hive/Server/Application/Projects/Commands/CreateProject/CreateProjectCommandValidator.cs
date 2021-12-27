using FluentValidation;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        private readonly ApplicationDbContext _context;
        private Guid _organizationId;

        public CreateProjectCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Project name cannot be empty")
                .MustAsync(NotExist).WithMessage("Project with this name already exists. Try using a new name or make this name more specific.");

            RuleFor(c => c.OrganizationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Organization ID is required").WithErrorCode(StatusCodes.Status400BadRequest.ToString())
                .MustAsync(BeValidOrgId).WithMessage("Invalid Organization ID").WithErrorCode(StatusCodes.Status400BadRequest.ToString());

            RuleFor(c => c.ProjectOwnerId)
                .MustAsync(BeValidOrganizationUser).When(c => c.ProjectOwnerId != null).WithMessage("User is not part of this organization");
        }

        private async Task<bool> BeValidOrganizationUser(string projectOwnerId, CancellationToken cancellationToken)
            => await _context.OrganizationUsers.AnyAsync(ou => projectOwnerId == ou.MemberId && _organizationId == ou.OrganizationId);

        private Task<bool> NotExist(string projectName, CancellationToken cancellationToken)
            => Task.FromResult(_context.Projects.All(p => p.Name.ToLower() != projectName.ToLower()));

        private async Task<bool> BeValidOrgId(Guid orgId, CancellationToken cancellationToken)
        {
            Organization organization = await _context.Organizations.FindAsync(orgId);
            _organizationId = organization.Id;

            return organization != null;
        }
    }
}
