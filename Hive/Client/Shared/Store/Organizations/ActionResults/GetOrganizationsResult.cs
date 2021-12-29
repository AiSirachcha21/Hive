using Hive.Shared.Organizations.QueryViewModels;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.Organizations.ActionResults
{
    public record GetOrganizationsResult(IList<OrganizationViewModel> Organizations);
}
