using Hive.Client.Services.Common;
using Hive.Client.Shared.Constants;
using Hive.Client.Shared.Entities;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Parameter]
        public bool DrawerOpen { get; set; }
        [Parameter]
        public EventCallback OnExpansionToggleClicked { get; set; }
        [Parameter]
        public Guid ActiveItemId { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        public ActiveNavigationItem ActiveNavItem { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public HttpClient Http { get; set; }
        private ISnackbar Snackbar { get; set; }
        public List<OrganizationViewModel> Organizations { get; set; } = new List<OrganizationViewModel>();

        protected async Task ExpansionToggleClicked()
        {
            await OnExpansionToggleClicked.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetOrganizations();
        }

        public async Task SetOrganizations()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<List<OrganizationViewModel>>(ApiRoutes.GetOrganizations);
                if (result != null)
                {
                    Organizations.AddRange(result);

                    var defaultOrganization = Organizations[0];
                    var uriIdentifier = Navigation.Uri.Split('/').Last();

                    // Sets ActiveNavItem State
                    await ActiveNavItem.Set(new ActiveNavigationItem { Id = defaultOrganization.Id, Name = defaultOrganization.Name });
                    ActiveNavItem.OnChange += StateHasChanged;
                    Navigation.NavigateTo(Routes.IndexOfOrganization(defaultOrganization.Name));
                }
            }
            catch (Exception)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add("No organizations. Create one ?", Severity.Info, config =>
                {
                    config.RequireInteraction = true;
                    config.ShowCloseIcon = true;
                    config.CloseAfterNavigation = true;
                    config.Action = "Create";
                    config.Onclick = snackbar =>
                    {
                        Navigation.NavigateTo(Routes.CreateProject);
                        Snackbar.Clear();
                        return Task.CompletedTask;
                    };
                });
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!(await AuthenticationState).User.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo("/login");
            }

            //ActiveItemId = Regex.IsMatch(Navigation.Uri, pattern: _organizationIndexRegex.ToString())
            //            ? Guid.Parse(Navigation.Uri.Split('/').Last())
            //            : Organizations[0].Id;
            OrganizationViewModel org = null;
            var uriIdentifier = Navigation.Uri.Split('/').Last();

            if (uriIdentifier == (Organizations.Where(o => o.Name == uriIdentifier).First().Name ?? null))
            {
                Console.WriteLine(uriIdentifier);
                Console.WriteLine(Organizations.Where(o => o.Name == uriIdentifier).First().Name ?? null);

                Console.WriteLine(uriIdentifier == (Organizations.Where(o => o.Name == uriIdentifier).First().Name ?? null));
            }


            if (uriIdentifier.GetType() == typeof(string))
            {
                org = Organizations.Where(x => x.Name == uriIdentifier).First() ?? null;
                ActiveItemId = org.Id;
            }
            else
            {
                ActiveItemId = Organizations[0].Id;
            }

            await ActiveNavItem.Set(new ActiveNavigationItem { Id = ActiveItemId, Name = Organizations.Where(o => o.Id == ActiveItemId).First().Name ?? null });
            ActiveNavItem.OnChange += StateHasChanged;



        }

        void ToggleDrawer() => DrawerOpen = !DrawerOpen;

        private bool ShouldCollapse() => Navigation.Uri != Navigation.BaseUri;

        private void NavigateToOrganizationPage(string organizationName)
        {
            Navigation.NavigateTo(Routes.IndexOfOrganization(organizationName));
        }

        public void Dispose()
        {
            ActiveNavItem.OnChange += StateHasChanged;
        }
    }
}
