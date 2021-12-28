using Hive.Domain;
using Hive.Shared.Login;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Authentication.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery(ClaimsPrincipal UserClaims) : IRequest<CurrentUser>;

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetCurrentUserQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CurrentUser> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var UserClaimsPrinciple = request.UserClaims;
            var user = await _userManager.GetUserAsync(UserClaimsPrinciple);
            return new CurrentUser
            {
                IsAuthenticated = UserClaimsPrinciple.Identity.IsAuthenticated,
                UserName = UserClaimsPrinciple.Identity.Name,
                Claims = UserClaimsPrinciple.Claims.ToDictionary(c => c.Type, c => c.Value),
                DisplayName = $"{user.FirstName} {user.LastName.First()}"
            };
        }
    }
}
