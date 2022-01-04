using Hive.Server.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Queries.CheckForDuplicateOrganziationName
{
    public record CheckForDuplicateOrganizationNameCommand(string Name) : IRequest<bool>;

    public record CheckForDuplicateOrganizationNameCommandHandler : IRequestHandler<CheckForDuplicateOrganizationNameCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public CheckForDuplicateOrganizationNameCommandHandler(ApplicationDbContext context)
            => _context = context;

        public async Task<bool> Handle(CheckForDuplicateOrganizationNameCommand request, CancellationToken cancellationToken)
            => await _context.Organizations.AnyAsync(o => o.Name == request.Name);
    }
}
