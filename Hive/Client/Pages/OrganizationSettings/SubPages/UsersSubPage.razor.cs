using Fluxor;
using Fluxor.Blazor.Web.Components;
using Hive.Client.Shared.Store.OrganizationSettings;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Hive.Client.Pages.OrganizationSettings.SubPages
{
    public partial class UsersSubPage
    {
        [CascadingParameter] Guid OrganizationId { get; set; }
        [Inject] IState<OrganizationSettingsState> OrganizationSettingsState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }

        bool IsLoading => OrganizationSettingsState.Value.IsLoading;
        List<OrganizationUserViewModel> Users => OrganizationSettingsState.Value.SettingsPageData.Members;

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new FetchOrganizationSettingsPageDataAction(OrganizationId));
            base.OnInitialized();
        }

    }
}