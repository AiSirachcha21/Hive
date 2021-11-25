using Hive.Server.Domain;
using Hive.Server.Infrastructure;
using Hive.Shared.Login;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        private readonly LoginRequest loginReq;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginCommand(LoginRequest loginReq, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.loginReq = loginReq;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly ApplicationDbContext _context;

            public LoginCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await request.userManager.FindByNameAsync(request.loginReq.UserName);
                if (user == null)
                {
                    return new LoginResponse { Message = "User does not exist", StatusCode = 400 };
                }

                var singInResult = await request.signInManager.CheckPasswordSignInAsync(user, request.loginReq.Password, false);
                if (!singInResult.Succeeded)
                {
                    return new LoginResponse { Message = "Invalid password", StatusCode = 400 };
                }

                await request.signInManager.SignInAsync(user, request.loginReq.RememberMe);

                return new LoginResponse { StatusCode = 200, Message = $"Welcome {user.UserName}" };
            }
        }
    }


}
