using FluentValidation;

namespace Hive.Server.Application.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c=>c.LoginReq.UserName).NotEmpty();
            RuleFor(c=>c.LoginReq.Password).NotEmpty();
            RuleFor(c=>c.SignInManager).NotEmpty();
            RuleFor(c=>c.UserManager).NotEmpty();
        }
    }
}
