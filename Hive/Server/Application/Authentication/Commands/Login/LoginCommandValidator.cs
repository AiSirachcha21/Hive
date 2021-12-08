using FluentValidation;

namespace Hive.Server.Application.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.LoginReq.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(c => c.LoginReq.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
