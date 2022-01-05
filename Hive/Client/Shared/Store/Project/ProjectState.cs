using Fluxor;
using Hive.Shared.Projects.Queries;

namespace Hive.Client.Shared.Store.Project
{
    public record ProjectState(bool IsLoading, ProjectViewModel Project);

    public class ProjectFeatureState : Feature<ProjectState>
    {
        public override string GetName()
        {
            return nameof(ProjectState);
        }

        protected override ProjectState GetInitialState()
        {
            return new ProjectState(false, null);
        }
    }
}
