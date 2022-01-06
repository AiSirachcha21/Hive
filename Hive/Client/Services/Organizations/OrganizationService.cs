using Hive.Client.Services.Common;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Shared.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Services.Organizations
{
    public interface IOrganizationService
    {
        Task<List<OrganizationViewModel>> GetOrganizationsAsync();
        Task<bool> AddOrganizationAsync(string name);
        Task<bool> DeleteOrganizatinoAsync(Guid organizationId);
        Task<OrganizationSettingsOverviewViewModel> GetOrganizationSettingsOverviewAsync(Guid organizationId);
        Task<bool> DoesEditedOrganizationNameExist(string name);
        Task<bool> UpdateOrganizationAsync(UpdateOrganizationRequestViewModel data);
        Task<List<UserViewModel>> GetUserPool(Guid organizationId);
        Task<bool> AddToOrganizationAsync(Guid organizationId, string userId);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _http;

        public OrganizationService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<OrganizationViewModel>> GetOrganizationsAsync()
        {
            List<OrganizationViewModel> result = await _http.GetFromJsonAsync<List<OrganizationViewModel>>(ApiRoutes.GetOrganizations);
            return result;
        }
        public async Task<bool> AddOrganizationAsync(string name)
        {
            JsonContent content = JsonContent.Create(name);
            HttpResponseMessage result = await _http.PostAsync(ApiRoutes.CreateOrganization, content);

            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrganizatinoAsync(Guid organizationId)
        {
            HttpResponseMessage result = await _http.DeleteAsync(ApiRoutes.DeleteOrganization(organizationId));

            return result.IsSuccessStatusCode;
        }

        public async Task<OrganizationSettingsOverviewViewModel> GetOrganizationSettingsOverviewAsync(Guid organizationId)
        {
            OrganizationSettingsOverviewViewModel result = await _http.GetFromJsonAsync<OrganizationSettingsOverviewViewModel>(ApiRoutes.GetOrganizationSettingsView(organizationId));
            return result ?? null;
        }

        public async Task<bool> DoesEditedOrganizationNameExist(string name)
        {
            return await _http.GetFromJsonAsync<bool>(ApiRoutes.CheckForDuplicateOrganization(name));
        }

        public async Task<bool> UpdateOrganizationAsync(UpdateOrganizationRequestViewModel data)
        {
            JsonContent content = JsonContent.Create(data);
            var result = await _http.PutAsync(ApiRoutes.UpdateOrganization, content);

            return result.IsSuccessStatusCode;
        }

        public async Task<List<UserViewModel>> GetUserPool(Guid organizationId)
        {
            var result = await _http.GetAsync(ApiRoutes.GetUserPool(organizationId));
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new List<UserViewModel>();
            }

            return await result.Content.ReadFromJsonAsync<List<UserViewModel>>();
        }

        public async Task<bool> AddToOrganizationAsync(Guid organizationId, string userId)
        {
            JsonContent content = JsonContent.Create(new { userId = userId, organizationId = organizationId });
            var result = await _http.PostAsync(ApiRoutes.AddToOrganization, content);

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
