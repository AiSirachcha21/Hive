using Fluxor;
using Hive.Client.Services.Projects;
using Hive.Client.Services.Tickets;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.Project
{
    public class ProjectEffects
    {
        private readonly IProjectService _projectService;
        private readonly ITicketService _ticketService;

        public ProjectEffects(IProjectService projectService, ITicketService ticketService)
        {
            _projectService = projectService;
            _ticketService = ticketService;
        }

        [EffectMethod]
        public async Task FetchProjectEffect(FetchProjectAction action, IDispatcher dispatcher)
        {
            var result = await _projectService.GetProjectByIdAsync(action.ProjectId);
            if (result != null)
            {
                dispatcher.Dispatch(new SetProjectAction(result));
            }
        }

        [EffectMethod]
        public async Task FetchProjectTicketsEffect(FetchProjectTicketsAction action, IDispatcher dispatcher)
        {
            var result = await _ticketService.GetTicketsByProjectIdAsync(action.ProjectId);
            dispatcher.Dispatch(new SetProjectTicketsAction(result));
        }
    }
}
