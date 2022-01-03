using Fluxor;
using Hive.Shared.Organizations.QueryViewModels;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.Organizations
{
    public record OrganizationState(bool IsLoading, IList<OrganizationViewModel> Organizations, OrganizationViewModel ViewingOrganization);

    public class OrganizationFeatureState : Feature<OrganizationState>
    {
        public override string GetName() => nameof(OrganizationState);

        protected override OrganizationState GetInitialState() => new(false, new List<OrganizationViewModel>(0), null);
    }
}
