using Microsoft.AspNetCore.Components;
using MudBlazor;
using Hive.Shared.Tickets.Queries;
using Hive.Shared.Tickets.Commands;
using System;
using Hive.Client.Services.Tickets;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hive.Shared.Projects.Queries;

namespace Hive.Client.Components.AddTicketDialog
{
    public partial class AddTicketDialog
    {
        [Parameter]
        public List<ProjectUserViewModel> Users { get; set; }
        [Parameter]
        public Guid ProjectId { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        CreateTicketRequest RequestContext { get; set; } = new CreateTicketRequest();
        [Inject]
        public ITicketService TicketService { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override void OnInitialized()
        {
            RequestContext.ProjectId = ProjectId;
            base.OnInitialized();
        }

        async void Submit()
        {
            var newTicket = await TicketService.AddTicketAsync(RequestContext);
            Snackbar.Add("Success!", Severity.Success);
            MudDialog.Close(DialogResult.Ok(newTicket));
        }

        void CancelAsync() => MudDialog.Cancel();
    }
}