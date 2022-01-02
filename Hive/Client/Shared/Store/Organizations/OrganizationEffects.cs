using Fluxor;
using Hive.Client.Services.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using MudBlazor;
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

        [EffectMethod(typeof(GetOrganizationsAction))]
        public async Task HandleFetchOrganizationsAction(IDispatcher dispatcher)
        {
            var orgs = await _organizationService.GetOrganizationsAsync();
            dispatcher.Dispatch(new SetOrganizationsAction(orgs));
        }

        [EffectMethod]
        public async Task DeleteOrganizationEffect(DeleteOrganizationAction action, IDispatcher dispatcher)
        {
            var deleteResultSuccessful = await _organizationService.DeleteOrganizatinoAsync(action.OrganizationId);
            if (deleteResultSuccessful)
            {
                action.Snackbar.Add($"Successfully deleted", Severity.Success);
                dispatcher.Dispatch(new GetOrganizationsAction());
            }
        }
    }
}
