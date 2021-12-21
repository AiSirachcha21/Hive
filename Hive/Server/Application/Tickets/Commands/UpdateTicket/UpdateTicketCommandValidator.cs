using FluentValidation;
using Hive.Server.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateTicketCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(dto => dto.Id)
                .NotNull().WithMessage("Ticket ID cannot be null")
                .MustAsync(BeValidTicket).WithMessage("Ticket does not exist");

            RuleFor(dto => dto.Title)
                .NotNull().WithMessage("Ticket Title cannot be null");

            RuleFor(dto => dto.Status)
                .NotNull().WithMessage("Ticket Status cannot be null");

            RuleFor(dto => dto.AssignedUserId)
                .MustAsync(BeValidUser).WithMessage("User does not exist")
                .When(dto => dto.AssignedUserId != null);
        }

        private async Task<bool> BeValidTicket(Guid ticketId, CancellationToken cancellationToken)
            => await _context.Tickets.FindAsync(ticketId) != null;
        private async Task<bool> BeValidUser(string userId, CancellationToken cancellationToken)
            => await _context.Users.FindAsync(userId) != null;

    }
}
