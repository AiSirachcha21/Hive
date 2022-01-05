using Fluxor;
using Hive.Client.Services.Projects;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public class OrganizationPageEffects
    {
        private readonly IProjectService _projectService;

        public OrganizationPageEffects(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [EffectMethod]
        public async Task FetchOrganizationProjects(FetchOrganizationProjectsAction action, IDispatcher dispatcher)
        {
            var result = await _projectService.GetUserProjectsAsync(action.OrganizationId);
            dispatcher.Dispatch(new SetOrganizationProjectsAction(result));
        }
    }
}
