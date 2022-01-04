using Fluxor;
using Hive.Shared.Organizations.QueryViewModels;

namespace Hive.Client.Shared.Store.OrganizationSettings
{
    public record OrganizationSettingsState(bool IsLoading, OrganizationSettingsOverviewViewModel SettingsPageData);

    public class OrganizationSettingsFeatureState : Feature<OrganizationSettingsState>
    {
        public override string GetName()
        {
            return nameof(OrganizationSettingsState);
        }

        protected override OrganizationSettingsState GetInitialState()
        {
            return new OrganizationSettingsState(false, null);
        }
    }
}