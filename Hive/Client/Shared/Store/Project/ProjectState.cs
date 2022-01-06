using Fluxor;
using Hive.Shared.Projects.Queries;
using Hive.Shared.Tickets.Queries;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.Project
{
    public record ProjectState(bool IsLoading, ProjectViewModel Project, List<TicketViewModel> ProjectTickets);

    public class ProjectFeatureState : Feature<ProjectState>
    {
        public override string GetName()
        {
            return nameof(ProjectState);
        }

        protected override ProjectState GetInitialState()
        {
            return new ProjectState(false, null, null);
        }
    }
}
