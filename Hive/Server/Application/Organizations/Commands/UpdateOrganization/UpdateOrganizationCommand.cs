using AutoMapper;
using Hive.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Organizations.QueryViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Organizations.Commands.UpdateOrganization
{
    public record UpdateOrganizationCommand(UpdateOrganizationRequestViewModel Data) : IRequest<Unit>;

    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UpdateOrganizationCommandHandler(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var currentOrg = await _context.Organizations.FindAsync(request.Data.Id);
            _context.Organizations.Update(_mapper.Map(request.Data, currentOrg));
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
