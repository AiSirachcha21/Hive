using Hive.Shared.Errors;
using Hive.Shared.Login;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Hive.Server.Application.Authentication.Commands.Register
{
    public class RegisterCommandViewModel
    {
        public IList<IdentityError> Errors { get; set; }
        public LoginResponse Data { get; set; }
    }
}
