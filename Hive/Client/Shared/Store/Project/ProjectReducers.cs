using Fluxor;

namespace Hive.Client.Shared.Store.Project
{
    public class ProjectReducers
    {
        [ReducerMethod]
        public static ProjectState FetchProject(ProjectState state, FetchProjectAction action)
        {
            return state with
            {
                IsLoading = true
            };
        }

        [ReducerMethod]
        public static ProjectState SetProject(ProjectState state, SetProjectAction action)
        {
            return state with
            {
                IsLoading = false,
                Project = action.Project
            };
        }

        [ReducerMethod(typeof(FetchProjectTicketsAction))]
        public static ProjectState FetchProjectTickets(ProjectState state)
        {
            return state with
            {
                IsLoading = true
            };
        }

        [ReducerMethod]
        public static ProjectState FetchProjectTickets(ProjectState state, SetProjectTicketsAction action)
        {
            return state with
            {
                IsLoading = false,
                ProjectTickets = action.Tickets
            };
        }
    }
}
