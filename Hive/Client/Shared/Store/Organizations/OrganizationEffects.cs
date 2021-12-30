using Fluxor;
using Hive.Client.Services.Organizations;
using Hive.Client.Shared.Store.Organizations.ActionResults;
using Hive.Client.Shared.Store.Organizations.Actions;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.Organizations.Effects
{
    public class OrganizationEffects
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationEffects(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [EffectMethod]
        public async Task HandleFetchOrganizationsAction(GetOrganizationsAction action, IDispatcher dispatcher)
        {
            var orgs = await _organizationService.GetOrganizationsAsync();
            dispatcher.Dispatch(new GetOrganizationsResult(orgs));
        }
    }
}
