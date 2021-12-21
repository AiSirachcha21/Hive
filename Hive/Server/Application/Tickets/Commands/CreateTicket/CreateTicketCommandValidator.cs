using FluentValidation;
using Hive.Server.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateTicketCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(dto => dto.Title)
               .NotNull().WithMessage("Ticket Title cannot be null");

            RuleFor(dto => dto.ProjectId)
                .NotNull().WithMessage("Project ID cannot be empty or null")
                .MustAsync(BeValidProjectId).WithMessage("Invalid Project ID");

            RuleFor(dto => dto.AssignedUserId)
               .MustAsync(BeValidUser).WithMessage("User does not exist")
               .When(dto => dto.AssignedUserId != null);
        }

        private async Task<bool> BeValidProjectId(Guid projectid, CancellationToken cancellationToken)
            => await _context.Projects.FindAsync(projectid) != null;

        private async Task<bool> BeValidUser(string userId, CancellationToken cancellationToken)
           => await _context.Users.FindAsync(userId) != null;
    }
}
