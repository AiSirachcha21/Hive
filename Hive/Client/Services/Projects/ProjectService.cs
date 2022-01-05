using Hive.Client.Services.Common;
using Hive.Shared.Projects.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Services.Projects
{
    public interface IProjectService
    {
        Task<List<ProjectDisplayViewModel>> GetUserProjectsAsync(Guid organizationId);
    }

    public class ProjectService : IProjectService
    {
        private readonly HttpClient _http;

        public ProjectService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Gets projects for the current user that belong to the organization being viewed. The UserID is supplied by the UserClaimsPrinciple in the Server.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns>List of Projects</returns>
        public async Task<List<ProjectDisplayViewModel>> GetUserProjectsAsync(Guid organizationId)
        {
            return await _http.GetFromJsonAsync<List<ProjectDisplayViewModel>>(ApiRoutes.GetUserProjects(organizationId));
        }
    }
}
