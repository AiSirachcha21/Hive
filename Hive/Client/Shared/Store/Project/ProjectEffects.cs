using Fluxor;
using Hive.Client.Services.Projects;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.Project
{
    public class ProjectEffects
    {
        private readonly IProjectService _projectService;

        public ProjectEffects(IProjectService projectService)
        {
            _projectService = projectService;
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
    }
}
