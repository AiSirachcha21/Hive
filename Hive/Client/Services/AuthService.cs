using Hive.Client.Services.Common;
using Hive.Client.Services.Interfaces;
using Hive.Shared.Login;
using Hive.Shared.Registration;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrentUser> GetCurrentUserInfo()
        {
            CurrentUser result = await _httpClient.GetFromJsonAsync<CurrentUser>(ApiRoutes.GetCurrentUser);
            return result;
        }

        public async Task Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync(ApiRoutes.Login, request);
            if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());

            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync(ApiRoutes.Logout, null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegistrationRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync(ApiRoutes.Register, request);
            if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
