using Hive.Client.Services.Common;
using Hive.Shared.Projects.Commands;
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
        Task<List<ProjectViewModel>> GetUserProjectsAsync(Guid organizationId);
        Task<ProjectViewModel> GetProjectByIdAsync(Guid id);
        Task<bool> CreateProjectAsync(CreateProjectRequestModel data);
        Task<bool> AddUserToProjectAsync(List<string> userIds, Guid projectid);
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
        public async Task<List<ProjectViewModel>> GetUserProjectsAsync(Guid organizationId)
        {
            return await _http.GetFromJsonAsync<List<ProjectViewModel>>(ApiRoutes.GetUserProjects(organizationId));
        }

        /// <summary>
        /// Gets Project by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project in form of <c>ProjectViewModel</c></returns>
        public async Task<ProjectViewModel> GetProjectByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ProjectViewModel>(ApiRoutes.GetProject(id));
        }

        public async Task<bool> CreateProjectAsync(CreateProjectRequestModel data)
        {
            JsonContent content = JsonContent.Create(data);
            var result = await _http.PostAsync(ApiRoutes.CreateProject, content);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AddUserToProjectAsync(List<string> userIds, Guid projectId)
        {
            JsonContent content = JsonContent.Create(new { userIds = userIds, projectId = projectId });
            var result = await _http.PostAsync(ApiRoutes.AddUsersToProject, content);

            return result.IsSuccessStatusCode;
        }
    }
}
