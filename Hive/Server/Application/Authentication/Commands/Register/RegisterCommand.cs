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
                user.UserName = request.regReq.Email;

                var result = await request.userManager.CreateAsync(user, request.regReq.Password);
                

                if (!result.Succeeded)
                {
                    dto.Error = new ErrorResponse { Message = "Failed to create user", ErrorCode = 400 };
                    return dto;
                }

                var addToRolesResult = await request.userManager.AddToRoleAsync(user, UserRoles.SystemAdmin);

                if (!addToRolesResult.Succeeded)
                {
                    dto.Error = new ErrorResponse { Message = "There was an issue when creating your user profile. Please try again later.", ErrorCode = 500 };
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
