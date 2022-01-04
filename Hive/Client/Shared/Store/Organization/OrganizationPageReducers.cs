using Fluxor;

namespace Hive.Client.Shared.Store.Organization
{
    public class OrganizationPageReducers
    {
        [ReducerMethod]
        public static OrganizationPageState SetCurrentOrganizationId(OrganizationPageState state, SetCurrentOrganizationId action) => state with
        {
            CurrentOrganization = action.OrganizationId,
        };
    }
}
