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
    }
}
