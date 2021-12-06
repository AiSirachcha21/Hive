using Hive.Client.Services;
using Hive.Client.Shared.Constants;
using Hive.Shared.Login;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hive.Client.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public AuthStateProvider AuthProvider { get; set; }

        public string Error { get; set; }
        public LoginRequest LoginRequest { get; set; } = new LoginRequest();


        private async Task OnSubmit()
        {
            Error = null;
            try
            {
                await AuthProvider.Login(LoginRequest);
                Navigation.NavigateTo(Routes.Index);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
    }
}
