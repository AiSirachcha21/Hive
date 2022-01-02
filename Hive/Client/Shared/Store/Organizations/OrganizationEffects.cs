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
        private readonly ISnackbar _snackbar;

        public OrganizationEffects(IOrganizationService organizationService, ISnackbar snackbar)
        {
            _organizationService = organizationService;
            _snackbar = snackbar;
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
            bool deleteResultSuccessful = await _organizationService.DeleteOrganizatinoAsync(action.OrganizationId);
            if (deleteResultSuccessful)
            {
                _snackbar.Add($"Successfully deleted", Severity.Success);
                dispatcher.Dispatch(new GetOrganizationsAction());
            }
            else _snackbar.Add($"Failed to delete organization.", Severity.Error);
        }

        [EffectMethod]
        public async Task CreateOrganizationEffect(CreateOrganizationAction action, IDispatcher dispatcher)
        {
            bool created = await _organizationService.AddOrganizationAsync(action.Name);
            if (created)
            {
                _snackbar.Add($"Successfully added {action.Name}", Severity.Success);
                dispatcher.Dispatch(new GetOrganizationsAction());
            }
            else _snackbar.Add($"Failed to add {action.Name}", Severity.Error);
        }
    }
}
