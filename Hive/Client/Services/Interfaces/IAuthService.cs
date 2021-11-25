using Hive.Shared.Login;
using Hive.Shared.Registration;
using System.Threading.Tasks;

namespace Hive.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginRequest request);
        Task Register(RegistrationRequest request);
        Task Logout();
        Task<CurrentUser> GetCurrentUserInfo();
    }
}
