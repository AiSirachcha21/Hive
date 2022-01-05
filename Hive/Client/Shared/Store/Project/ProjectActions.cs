using Hive.Shared.Projects.Queries;
using System;

namespace Hive.Client.Shared.Store.Project
{
    public record FetchProjectAction(Guid ProjectId);
    public record SetProjectAction(ProjectViewModel Project);
}
