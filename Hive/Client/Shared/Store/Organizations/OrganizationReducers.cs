using Fluxor;
using Hive.Client.Shared.Store.Organizations.ActionResults;
using Hive.Client.Shared.Store.Organizations.Actions;

namespace Hive.Client.Shared.Store.Organizations
{
    public static class OrganizationReducers
    {
        [ReducerMethod(typeof(FetchDataAction))]
        public static OrganizationState FetchOrganizationData(OrganizationState state)
        {
            return state with
            {
                IsLoading = true,
                Organizations = null
            };
        }

        [ReducerMethod]
        public static OrganizationState ReduceGetOrganizationsResultAction(OrganizationState state, GetOrganizationsResult action) => state with
        {
            IsLoading = false,
            Organizations = action.Organizations
        };
    }
}
