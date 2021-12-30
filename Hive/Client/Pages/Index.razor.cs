using Fluxor;
using Hive.Client.Shared.Store.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Hive.Client.Pages
{
    public partial class Index
    {
        [Inject] NavigationManager Navigator { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] IState<OrganizationState> OrganizationState { get; set; }

        string _searchText;
        IList<OrganizationViewModel> Organizations =>
            _searchText == null
            ? OrganizationState.Value.Organizations
            : OrganizationState.Value.Organizations.Where(org => org.Name.Contains(_searchText)).ToList();
        bool OrganizationStateIsLoading => OrganizationState.Value.IsLoading;

        protected override void OnInitialized()
        {
            Navigator.NavigateTo("/organizations");
            base.OnInitialized();
            Dispatcher.Dispatch(new GetOrganizationsAction());
        }
    }
}
