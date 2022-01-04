using Hive.Client.Services.Common;
using Hive.Shared.Organizations.QueryViewModels;
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
    }
}
