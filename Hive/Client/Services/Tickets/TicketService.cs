using Hive.Client.Services.Common;
using Hive.Shared.Tickets.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hive.Client.Services.Tickets
{
    public interface ITicketService
    {
        Task<List<TicketViewModel>> GetTicketsByProjectIdAsync(Guid projectId);
    }

    public class TicketService : ITicketService
    {
        private readonly HttpClient _http;

        public TicketService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TicketViewModel>> GetTicketsByProjectIdAsync(Guid projectId)
        {
            var result = await _http.GetAsync(ApiRoutes.GetTicketsByProjectId(projectId));
            if (result.IsSuccessStatusCode)
            {
                return new List<TicketViewModel>();
            }

            return await result.Content.ReadFromJsonAsync<List<TicketViewModel>>();
        }
    }
}
