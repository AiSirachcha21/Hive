using FluentValidation;
using Hive.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Projects.Commands.AddUserToProject
{
    public class AddUserToProjectCommandValidator : AbstractValidator<AddUserToProjectCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddUserToProjectCommandValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(c => c.ProjectId)
                .NotEmpty().WithMessage("Project ID should not be empty")
                .MustAsync(BeValidProjectId).WithMessage("Project ID is not valid");

            RuleFor(c => c.UserIds)
                .NotEmpty().WithMessage("User ID should not be empty")
                .MustAsync(BeValidListOfUserIds).WithMessage("User ID is not valid");
        }

        private async Task<bool> BeValidListOfUserIds(List<string> userIds, CancellationToken cancellationToken)
        {
            var validIds = await _context.Users.Select(u => u.Id).ToListAsync();
            return userIds.All(id => validIds.Contains(id));
        }

        private async Task<bool> BeValidProjectId(Guid projectId, CancellationToken cancellationToken)
            => await _context.Projects.AnyAsync(p => p.Id == projectId);
    }
}
