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
        public bool DrawerOpen { get; set; } = true;

        protected override async Task OnParametersSetAsync()
        {
            if(!(await AuthenticationState).User.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo("/login");
            }
        }

        void ToggleDrawer() => DrawerOpen = !DrawerOpen;

        async Task Logout()
        {
            await AuthProvider.Logout();
            Navigation.NavigateTo("/login");
        }
    }
}
