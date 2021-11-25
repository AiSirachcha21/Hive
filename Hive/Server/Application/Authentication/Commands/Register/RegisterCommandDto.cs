using Hive.Shared.Errors;
using Hive.Shared.Login;

namespace Hive.Server.Application.Authentication.Commands.Register
{
    public class RegisterCommandDto
    {
        public ErrorResponse Error { get; set; }
        public LoginResponse Data { get; set; }
    }
}
