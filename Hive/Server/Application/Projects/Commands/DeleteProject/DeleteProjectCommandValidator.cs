using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteProjectCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.projectId)
                .NotEmpty().WithMessage("Project ID cannot be null")
                .MustAsync(BeValidProject).WithMessage("Project does not exist");
        }

        private async Task<bool> BeValidProject(Guid projectId, CancellationToken cancellationToken)
            => await _context.Projects.AnyAsync(p => p.Id == projectId);
    }
}
