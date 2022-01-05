using Fluxor;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public class OrganizationPageReducers
    {
        [ReducerMethod(typeof(FetchOrganizationPageAction))]
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
