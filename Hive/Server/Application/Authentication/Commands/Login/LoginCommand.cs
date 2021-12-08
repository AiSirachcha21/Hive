using Hive.Domain;
using Hive.Shared.Login;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Server.Application.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest LoginReq { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }

        public LoginCommand(LoginRequest loginReq, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            LoginReq = loginReq;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                LoginResponse errorResponse = new() { Message = "Invalid Username/Password", StatusCode = 400 };

                ApplicationUser user = await request.UserManager.FindByNameAsync(request.LoginReq.UserName);
                if (user == null)
                {
                    return errorResponse;
                }

                var singInResult = await request.SignInManager.CheckPasswordSignInAsync(user, request.LoginReq.Password, false);
                if (!singInResult.Succeeded)
                {
                    return errorResponse;
                }

                await request.SignInManager.SignInAsync(user, request.LoginReq.RememberMe);

                return new LoginResponse { StatusCode = 200, Message = $"Welcome {user.UserName}" };
            }
        }
    }


}
