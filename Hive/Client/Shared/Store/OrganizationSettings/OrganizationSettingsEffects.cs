using Fluxor;
using Hive.Client.Services.Organizations;
using MudBlazor;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.OrganizationSettings
{
    public class OrganizationSettingsEffects
    {
        private readonly IOrganizationService _organizationService;
        private readonly ISnackbar _snackbar;

        public OrganizationSettingsEffects(IOrganizationService organizationService, ISnackbar snackbar)
        {
            _organizationService = organizationService;
            _snackbar = snackbar;
        }

        [EffectMethod]
        public async Task HandleFetchOrganizationSettingsOverviewPageData(FetchOrganizationSettingsPageDataAction action, IDispatcher dispatcher)
        {
            var data = await _organizationService.GetOrganizationSettingsOverviewAsync(action.OrganizationId);
            dispatcher.Dispatch(new SetOrganizationSettingsPageDataAction(data));
        }

        [EffectMethod]
        public async Task HandleUpdateOrganizationAction(UpdateOrganizationNameAction action, IDispatcher dispatcher)
        {
            bool didUpdate = await _organizationService.UpdateOrganizationAsync(action.data);
            if (didUpdate)
            {
                _snackbar.Add("Successfully updated organization", Severity.Success);
                dispatcher.Dispatch(new FetchOrganizationSettingsPageDataAction(action.data.Id));
            }
        }
    }
}
