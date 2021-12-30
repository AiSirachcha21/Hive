using Hive.Client.Services.Common;
using Hive.Shared.Organizations.QueryViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Services.Organizations
{
    public interface IOrganizationService
    {
        Task<List<OrganizationViewModel>> GetOrganizationsAsync();
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
            var result = await _http.GetFromJsonAsync<List<OrganizationViewModel>>(ApiRoutes.GetOrganizations);
            return result;
        }
    }
}
