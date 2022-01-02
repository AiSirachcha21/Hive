using System;
using Microsoft.AspNetCore.Components;
using Fluxor;
using Hive.Client.Pages.OrganizationSettings.SubPages;
using Hive.Client.Shared.Store.Organizations;
using Hive.Client.Shared.Constants;
using System.Linq;

namespace Hive.Client.Pages.OrganizationSettings
{
    public partial class OrganizationSettings
    {
        const string OverviewSubpageName = "overview";
        const string UsersSubpageName = "users";

        [Parameter] public Guid OrganizationId { get; set; }
        [Parameter] public string PageRoute { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] NavigationManager Navigator { get; set; }
    }
}