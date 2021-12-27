using Hive.Domain;
using Hive.Server.Application.Authentication.Commands.Login;
using Hive.Server.Infrastructure;
using Hive.Shared;
using Hive.Shared.Login;
using Hive.Shared.Registration;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Authentication.Commands.Register
{
    public record RegisterCommand : IRequest<RegisterCommandViewModel>
    {
        private readonly RegistrationRequest _regReq;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterCommand(RegistrationRequest regReq, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _regReq = regReq;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandViewModel>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _mediator;

            public RegisterCommandHandler(ApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<RegisterCommandViewModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = new();
                RegisterCommandViewModel dto = new();
                user.UserName = request._regReq.Email;
                user.Email = request._regReq.Email;
                user.FirstName = request._regReq.FirstName;
                user.LastName = request._regReq.LastName;

                var result = await request._userManager.CreateAsync(user, request._regReq.Password);


                if (!result.Succeeded)
                {
                    dto.Errors = result.Errors.ToList();
                    return dto;
                }

                var addToRolesResult = await request._userManager.AddToRoleAsync(user, UserRoles.SystemAdmin);
                var attachIdToClaimsResult = await request._userManager.AddClaimAsync(user, new Claim("id", user.Id));

                if (!addToRolesResult.Succeeded || !attachIdToClaimsResult.Succeeded)
                {
                    dto.Errors = new List<IdentityError> { new IdentityError { Code = "500", Description = "There was a problem when adding your profile" } };
                    return dto;
                }

                await _context.SaveChangesAsync(cancellationToken);

                LoginRequest loginRequest = new() { UserName = request._regReq.Email, Password = request._regReq.Password, RememberMe = false };
                LoginResponse loginResult = await _mediator.Send(new LoginCommand(loginRequest, request._userManager, request._signInManager), cancellationToken);

                dto.Data = loginResult;

                return dto;
            }
        }
    }

}
