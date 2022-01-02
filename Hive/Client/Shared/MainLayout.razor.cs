using Hive.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Hive.Client.Shared
{
    public partial class MainLayout
    {
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public AuthStateProvider AuthProvider { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!(await AuthenticationState).User.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo("/login");
            }
        }
        

    }
}
