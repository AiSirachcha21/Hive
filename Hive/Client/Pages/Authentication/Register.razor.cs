using Hive.Client.Services;
using Hive.Shared.Registration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<string> Errors { get; set; }
        public bool ShowPassword { get; set; }

        void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
        }

        async Task OnSubmit()
        {
            Errors = null;
            try
            {
                await AuthProvider.Register(RegisterRequest);
                Navigation.NavigateTo("");
            }
            catch (Exception ex)
            {
                Errors = JsonConvert.DeserializeObject<List<IdentityError>>(ex.Message).Select(e => e.Description).ToList();
            }
        }
    }
}
