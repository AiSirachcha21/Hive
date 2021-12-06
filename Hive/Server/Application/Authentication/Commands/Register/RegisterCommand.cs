using Hive.Server.Application.Authentication.Commands.Login;
using Hive.Server.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared;
using Hive.Shared.Errors;
using Hive.Shared.Login;
using Hive.Shared.Registration;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Authentication.Commands.Register
{
    public record RegisterCommand : IRequest<RegisterCommandDto>
    {
        private readonly RegistrationRequest regReq;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public RegisterCommand(RegistrationRequest regReq, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.regReq = regReq;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandDto>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMediator _mediator;

            public RegisterCommandHandler(ApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<RegisterCommandDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = new ApplicationUser();
                RegisterCommandDto dto = new RegisterCommandDto();
                user.UserName = user.Email = request.regReq.Email;
                user.FirstName = request.regReq.FirstName;
                user.LastName = request.regReq.LastName;

                var result = await request.userManager.CreateAsync(user, request.regReq.Password);


                if (!result.Succeeded)
                {
                    dto.Errors = result.Errors.ToList();
                    return dto;
                }

                var addToRolesResult = await request.userManager.AddToRoleAsync(user, UserRoles.SystemAdmin);
                var attachIdToClaimsResult = await request.userManager.AddClaimAsync(user, new Claim("id", user.Id));

                if (!addToRolesResult.Succeeded || !attachIdToClaimsResult.Succeeded)
                {
                    dto.Errors = new List<IdentityError> { new IdentityError { Code = "500", Description = "There was a problem when adding your profile" } };
                    return dto;
                }

                await _context.SaveChangesAsync();

                LoginRequest loginRequest = new() { UserName = request.regReq.Email, Password = request.regReq.Password, RememberMe = false };
                LoginResponse loginResult = await _mediator.Send(new LoginCommand(loginRequest, request.userManager, request.signInManager));

                dto.Data = loginResult;

                return dto;
            }
        }
    }

}
