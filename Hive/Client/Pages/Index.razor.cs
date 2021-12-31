using Fluxor;
using Hive.Client.Components.AddOrganizationDialog;
using Hive.Client.Shared.Store.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using Hive.Shared.Organizations.QueryViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Client.Pages
{
    public enum OrganizationListViewMode
    {
        List,
        Grid
    }

    public partial class Index
    {
        [Inject] IDialogService DialogService { get; set; }
        [Inject] NavigationManager Navigator { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] IState<OrganizationState> OrganizationState { get; set; }

        string _searchText;
        OrganizationListViewMode _gridViewModel = OrganizationListViewMode.Grid;


        IList<OrganizationViewModel> Organizations =>
            _searchText == null
            ? OrganizationState.Value.Organizations
            : OrganizationState.Value.Organizations.Where(org => org.Name.Contains(_searchText)).ToList();
        bool OrganizationStateIsLoading => OrganizationState.Value.IsLoading;

        protected override void OnInitialized()
        {
            Navigator.NavigateTo("/organizations");
            base.OnInitialized();
            ReloadOrganizationData();
        }

        async Task ToggleAddOrganizationDialogAsync()
        {
            DialogParameters dialogParams = new()
            {
                { "OnSubmitComplete", () => ReloadOrganizationData() }
            };
            var dialog = DialogService.Show<AddOrganizationDialog>(null, dialogParams);
            var dialogResult = await dialog.Result;
        }

        void ReloadOrganizationData() => Dispatcher.Dispatch(new GetOrganizationsAction());
        void ToggleGridListView(OrganizationListViewMode mode)
        {
            _gridViewModel = mode switch
            {
                OrganizationListViewMode.List => OrganizationListViewMode.List,
                _ => OrganizationListViewMode.Grid,
            };
        }
    }
}
