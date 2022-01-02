using Hive.Shared.Organizations.QueryViewModels;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.Organizations.Actions
{
    public record SetOrganizationsAction(IList<OrganizationViewModel> Organizations);
}
