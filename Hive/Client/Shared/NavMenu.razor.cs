using Fluxor;
using Hive.Client.Shared.Constants;
using Hive.Client.Shared.Entities;
using Hive.Client.Shared.Store.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hive.Client.Shared
{
    public partial class NavMenu
    {
        [Parameter] public bool DrawerOpen { get; set; }
        [Parameter] public EventCallback OnExpansionToggleClicked { get; set; }
        [Parameter] public Guid ActiveItemId { get; set; }
        [CascadingParameter] public Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject] public ActiveNavigationItem ActiveNavItem { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public HttpClient Http { get; set; }
        [Inject] IState<OrganizationState> OrganizationState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }

        protected async Task ExpansionToggleClicked()
        {
            await OnExpansionToggleClicked.InvokeAsync();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new FetchDataAction());
        }

        private bool ShouldCollapse() => Navigation.Uri != Navigation.BaseUri;

        private void NavigateToOrganizationPage(string organizationName)
        {
            Navigation.NavigateTo(Routes.IndexOfOrganization(organizationName));
        }
    }
}
