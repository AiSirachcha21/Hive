using Fluxor;
using Fluxor.Blazor.Web.Components;
using Hive.Client.Services.Organizations;
using Hive.Client.Shared.Store.OrganizationSettings;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Shared.Users;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Client.Pages.OrganizationSettings.SubPages
{
    public partial class UsersSubPage
    {
        [CascadingParameter] Guid OrganizationId { get; set; }
        [Inject] IState<OrganizationSettingsState> OrganizationSettingsState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] IOrganizationService OrganizationService { get; set; }
        UserViewModel _searchedUserId;

        bool IsLoading => OrganizationSettingsState.Value.IsLoading;
        List<OrganizationUserViewModel> Users => OrganizationSettingsState.Value.SettingsPageData.Members;

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new FetchOrganizationSettingsPageDataAction(OrganizationId));
            base.OnInitialized();
        }

        async Task<IEnumerable<UserViewModel>> GetNetworkUsers(string searchString)
        {
            var data = await OrganizationService.GetUserPool(OrganizationId);
            return data.Where(d => d.FirstName.Contains(searchString) || d.LastName.Contains(searchString)).ToArray();
        }

        async void OnAddUser()
        {
            var data = await OrganizationService.AddToOrganizationAsync(OrganizationId, _searchedUserId.Id);
            if (data)
            {
                Dispatcher.Dispatch(new FetchOrganizationSettingsPageDataAction(OrganizationId));
                _searchedUserId = null;
            }
        }

    }
}