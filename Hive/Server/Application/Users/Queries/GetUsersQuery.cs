using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hive.Server.Infrastructure;
using Hive.Shared.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Users.Queries
{
    public record GetUsersQuery() : IRequest<List<UserViewModel>>;
    public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, List<UserViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
