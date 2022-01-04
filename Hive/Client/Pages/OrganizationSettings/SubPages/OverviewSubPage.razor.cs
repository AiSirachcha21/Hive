using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Hive.Client.Services.Organizations;
using Hive.Shared.Organizations.QueryViewModels;

namespace Hive.Client.Pages.OrganizationSettings.SubPages
{
    public partial class OverviewSubPage
    {
        [Inject] IOrganizationService OrganizationService { get; set; }
        [CascadingParameter] Guid OrganizationId { get; set; }

        OrganizationSettingsOverviewViewModel _organizationSettingsVm;
        string _organizationName;
        bool _isDataLoading = true;
        bool _isCheckingUniqueOrgName = false;
        bool _isUniqueOrgName = true;
        protected async override Task OnInitializedAsync()
        {
            _isDataLoading = _isDataLoading == true;
            _organizationSettingsVm = await OrganizationService.GetOrganizationSettingsOverviewAsync(OrganizationId);
            _organizationName = _organizationSettingsVm.Name;
            _isDataLoading = false;
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

        void ResetName() => _organizationName = _organizationSettingsVm.Name;
        bool IsNameEdited() => _organizationName != _organizationSettingsVm.Name;
        bool IsSavable() => IsNameEdited() && _isUniqueOrgName && !string.IsNullOrEmpty(_organizationName);
    }
}