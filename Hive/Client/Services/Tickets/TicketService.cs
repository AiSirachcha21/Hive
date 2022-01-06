using Hive.Client.Services.Common;
using Hive.Shared.Tickets.Commands;
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
        Task<TicketViewModel> AddTicketAsync(CreateTicketRequest request);
        Task<bool> DeleteTicketAsync(Guid ticketId);
        Task<bool> UpdateTicketAsync(UpdateTicketRequest request);
    }

    public class TicketService : ITicketService
    {
        private readonly HttpClient _http;

        public TicketService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TicketViewModel> AddTicketAsync(CreateTicketRequest request)
        {
            JsonContent content = JsonContent.Create(request);
            HttpResponseMessage response = await _http.PostAsync(ApiRoutes.CreateTicket, content);
            return await response.Content.ReadFromJsonAsync<TicketViewModel>();
        }

        public async Task<bool> DeleteTicketAsync(Guid ticketId)
        {
            HttpResponseMessage response = await _http.DeleteAsync(ApiRoutes.DeleteTicket(ticketId));
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TicketViewModel>> GetTicketsByProjectIdAsync(Guid projectId)
        {
            var result = await _http.GetAsync(ApiRoutes.GetTicketsByProjectId(projectId));
            if (!result.IsSuccessStatusCode)
            {
                return new List<TicketViewModel>();
            }

            return await result.Content.ReadFromJsonAsync<List<TicketViewModel>>();
        }

        public async Task<bool> UpdateTicketAsync(UpdateTicketRequest request)
        {
            var result = await _http.PutAsync(ApiRoutes.UpdateTicket, JsonContent.Create(request));

            return result.IsSuccessStatusCode;
        }
    }
}
