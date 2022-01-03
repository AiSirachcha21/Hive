using System;
using Microsoft.AspNetCore.Components;
using Fluxor;
using Hive.Client.Shared.Store.Organizations.Actions;
using MudBlazor;

namespace Hive.Client.Pages.OrganizationSettings
{
    public partial class OrganizationSettings
    {
        const string _overviewSubpageName = "overview";
        const string _usersSubpageName = "users";
        MudTheme _theme = new();

        [Parameter] public Guid OrganizationId { get; set; }
        [Parameter] public string PageRoute { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }

        // TODO: Create State in OrganizationState to hold info about current Org
        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new UpdateViewedOrganizationAction(OrganizationId));
            base.OnInitialized();
        }

    }
}