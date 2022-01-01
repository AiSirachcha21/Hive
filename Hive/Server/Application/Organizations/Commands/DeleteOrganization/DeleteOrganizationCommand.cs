using Hive.Server.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.DeleteOrganization
{
    public record DeleteOrganizationCommand(Guid Id) : IRequest<Unit>;

    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteOrganizationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var org = await _context.Organizations.FindAsync(request.Id);
            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
