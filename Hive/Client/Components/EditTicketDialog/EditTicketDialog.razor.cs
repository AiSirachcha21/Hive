using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hive.Client.Services.Tickets;
using Hive.Shared.Projects.Queries;
using Hive.Shared.Tickets.Commands;
using Hive.Shared.Tickets.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Hive.Client.Components.EditTicketDialog
{
    public partial class EditTicketDialog
    {
        [Parameter]
        public TicketViewModel Ticket { get; set; }
        [Parameter]
        public List<ProjectUserViewModel> Members { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        UpdateTicketRequest RequestContext { get; set; } = new UpdateTicketRequest();
        [Inject]
        public ITicketService TicketService { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            MudDialog.SetOptions(new DialogOptions()
            {
                FullScreen = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Large
            });
            RequestContext = new UpdateTicketRequest()
            {
                Id = Ticket.Id,
                Description = Ticket.Description,
                AssignedUserId = string.IsNullOrEmpty(Ticket.AssignedUserId) ? string.Empty : Ticket.AssignedUserId,
                Title = Ticket.Title,
                Status = Ticket.Status,
            };

            base.OnInitialized();
        }

        async void OnDelete()
        {
            bool? result = await DialogService.ShowMessageBox("Warning", "This action cannot be undone.", yesText: "Delete", cancelText: "Cancel");

            if (result.HasValue && result.Value)
            {
                var deleteSuccessful = await TicketService.DeleteTicketAsync(Ticket.Id);
                if (deleteSuccessful)
                {
                    MudDialog.Close(DialogResult.Ok("Deleted"));
                }
            }
        }

        async Task OnSubmitAsync()
        {
            if (IsModelAndContextEqual())
            {
                MudDialog.Close(DialogResult.Ok(false));
            }
            else
            {
                var result = await TicketService.UpdateTicketAsync(RequestContext);
                MudDialog.Close(DialogResult.Ok(result));
            }
        }
        void OnCancel() => MudDialog.Cancel();

        bool IsModelAndContextEqual()
        {
            return Ticket.Status == RequestContext.Status &&
                Ticket.Description == RequestContext.Description &&
                Ticket.Title == RequestContext.Title &&
                Ticket.AssignedUserId == RequestContext.AssignedUserId;
        }
    }
}