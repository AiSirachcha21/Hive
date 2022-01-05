using Fluxor;
using Hive.Shared.Projects.Queries;
using System;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public record OrganizationPageState(bool IsLoading, List<ProjectViewModel> Projects, Guid CurrentOrganization);

    public class OrganizationPageFeatureState : Feature<OrganizationPageState>
    {
        public override string GetName() => nameof(OrganizationPageState);

        protected override OrganizationPageState GetInitialState() => new(false, new List<ProjectViewModel>(), Guid.Empty);
    }
}
