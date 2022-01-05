using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Hive.Client.Services.Organizations;
using Hive.Shared.Organizations.QueryViewModels;
using Fluxor;
using Hive.Client.Shared.Store.OrganizationSettings;
using Hive.Client.Components.ParamConstants;
using Hive.Client.Components.RequireConfirmationDialog;
using Hive.Client.Shared.Store.Organizations.Actions;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Linq;
using Hive.Client.Shared.Constants;

namespace Hive.Client.Pages.OrganizationSettings.SubPages
{
    public partial class OverviewSubPage
    {
        [Inject] IOrganizationService OrganizationService { get; set; }
        [Inject] IState<OrganizationSettingsState> OrganizationSettingsState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] NavigationManager Navigator { get; set; }
        [CascadingParameter] Task<AuthenticationState> AuthenticationState { get; set; }
        [CascadingParameter] Guid OrganizationId { get; set; }

        string _organizationName;
        bool IsDataLoading => OrganizationSettingsState.Value.IsLoading;
        OrganizationSettingsOverviewViewModel OrganizationSettingsVm => OrganizationSettingsState.Value.SettingsPageData;

        bool _isCheckingUniqueOrgName = false;
        bool _isUniqueOrgName = true;

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new FetchOrganizationSettingsPageDataAction(OrganizationId));
            OrganizationSettingsState.StateChanged += OrganizationSettingsState_StateChanged;
            base.OnInitialized();
        }

        private void OrganizationSettingsState_StateChanged(object sender, OrganizationSettingsState e)
        {
            _organizationName = e.SettingsPageData.Name;
        }

        async Task CheckDuplicateName()
        {
            if (!IsNameEdited())
            {
                _isUniqueOrgName = true;
                return;
            }

            _isCheckingUniqueOrgName = true;
            _isUniqueOrgName = !(await OrganizationService.DoesEditedOrganizationNameExist(_organizationName));
            _isCheckingUniqueOrgName = false;
        }

        void UpdateOrganizationName()
        {
            var vm = new UpdateOrganizationRequestViewModel
            {
                Id = OrganizationId,
                Name = _organizationName,
            };
            Dispatcher.Dispatch(new UpdateOrganizationNameAction(vm));
        }

        string GetUpdateNameFieldErrorText() => string.IsNullOrEmpty(_organizationName) ? "The organization name cannot be empty" : "The organization name is not unique.";
        void ResetName() => _organizationName = OrganizationSettingsVm.Name;
        bool IsNameEdited() => _organizationName != OrganizationSettingsVm.Name;
        bool IsNameFieldEmpty => string.IsNullOrEmpty(_organizationName);
        bool IsSavable() => IsNameEdited() && _isUniqueOrgName && !string.IsNullOrEmpty(_organizationName);

        async void OnDelete(string name, Guid id)
        {
            string username = new((await AuthenticationState).User.Identity.Name.Where(s => !char.IsWhiteSpace(s)).ToArray());
            DialogParameters dialogParams = new()
            {
                { RequireConfirmationDialogParams.ConfirmButtonText, "Delete" },
                { RequireConfirmationDialogParams.DialogContent, $"You are about to delete {name}. This change is irreversable, are you sure you wish to continue ?" },
                { RequireConfirmationDialogParams.IsDelete, true },
                { RequireConfirmationDialogParams.ConfirmationPhrase, $"{username}/{name}" }
            };
            IDialogReference dialog = DialogService.Show<RequireConfirmationDialog>("Are you sure?", dialogParams);
            DialogResult dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled && (bool)dialogResult.Data)
            {
                Dispatcher.Dispatch(new DeleteOrganizationAction(id));
                Navigator.NavigateTo(Routes.UserOrganizations);
            }
        }
    }
}