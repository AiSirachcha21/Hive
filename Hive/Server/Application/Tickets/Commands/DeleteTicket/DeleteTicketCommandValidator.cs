using FluentValidation;
using Hive.Server.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandValidator : AbstractValidator<DeleteTicketCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTicketCommandValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Ticket Id is required")
                .MustAsync(BeValidTicket).WithMessage("Ticket doesn't exist");
        }

        private async Task<bool> BeValidTicket(Guid id, CancellationToken cancellationToken)
            => await _context.Tickets.FindAsync(id) != null;
    }
}
