using Hive.Client.Services;
using Hive.Shared.Registration;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hive.Client.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public AuthStateProvider AuthProvider { get; set; }

        public RegistrationRequest RegisterRequest { get; set; } = new RegistrationRequest();

        public string Error { get; set; }

        async Task OnSubmit()
        {
            Error = null;
            try
            {
                await AuthProvider.Register(RegisterRequest);
                Navigation.NavigateTo("");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
    }
}
