using Fluxor;
using Hive.Client.Services.Organizations;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.OrganizationSettings
{
    public class OrganizationSettingsEffects
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationSettingsEffects(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [EffectMethod]
        public async Task HandleFetchOrganizationSettingsOverviewPageData(FetchOrganizationSettingsPageDataAction action, IDispatcher dispatcher)
        {
            var data = await _organizationService.GetOrganizationSettingsOverviewAsync(action.OrganizationId);
            dispatcher.Dispatch(new SetOrganizationSettingsPageDataAction(data));
        }
    }
}
