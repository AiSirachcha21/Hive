using Hive.Shared.Projects.Queries;
using Hive.Shared.Tickets.Queries;
using System;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.Project
{
    public record FetchProjectAction(Guid ProjectId);
    public record SetProjectAction(ProjectViewModel Project);
    public record FetchProjectTicketsAction(Guid ProjectId);
    public record SetProjectTicketsAction(List<TicketViewModel> Tickets);
}
