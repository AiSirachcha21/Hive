using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Hive.Client.Services.Organizations;
using Hive.Shared.Organizations.QueryViewModels;
using Fluxor;
using Hive.Client.Shared.Store.OrganizationSettings;

namespace Hive.Client.Pages.OrganizationSettings.SubPages
{
    public partial class OverviewSubPage
    {
        [Inject] IOrganizationService OrganizationService { get; set; }
        [Inject] IState<OrganizationSettingsState> OrganizationSettingsState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
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
    }
}