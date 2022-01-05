using Fluxor;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public class OrganizationPageReducers
    {
        [ReducerMethod]
        public static OrganizationPageState SetCurrentOrganizationId(OrganizationPageState state, SetCurrentOrganizationIdAction action) => state with
        {
            CurrentOrganization = action.OrganizationId,
        };

        [ReducerMethod(typeof(FetchOrganizationProjectsAction))]
        public static OrganizationPageState FetchOrganizationProjects(OrganizationPageState state)
        {
            return state with
            {
                IsLoading = true,
            };
        }

        [ReducerMethod]
        public static OrganizationPageState SetOrganizationProjects(OrganizationPageState state, SetOrganizationProjectsAction action)
        {
            return state with
            {
                IsLoading = false,
                Projects = action.Projects
            };
        }
    }
}
