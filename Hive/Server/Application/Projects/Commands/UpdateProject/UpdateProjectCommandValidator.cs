using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateProjectCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.ProjectId)
                .NotEmpty().WithMessage("Project ID is not empty")
                .MustAsync(BeValidProjectId).WithMessage("Project doesn't exist");

            RuleFor(c => c.ProjectOwnerId)
                .MustAsync(BeNullOrValidUserId).WithMessage("ID must be null or a valid user ID");
        }

        private async Task<bool> BeNullOrValidUserId(string projectOwnerId, CancellationToken cancellationToken)
            => await _context.Users.AnyAsync(u => u.Id == projectOwnerId) || projectOwnerId == null;

        private async Task<bool> BeValidProjectId(Guid projectId, CancellationToken cancellationToken)
            => await _context.Projects.AnyAsync(p => p.Id == projectId);
    }
}
