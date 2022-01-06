using Fluxor;
using Hive.Client.Components.AddTicketDialog;
using Hive.Client.Components.EditTicketDialog;
using Hive.Client.Services.Tickets;
using Hive.Client.Shared.Store.Project;
using Hive.Domain;
using Hive.Shared.Projects.Queries;
using Hive.Shared.Tickets.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;

namespace Hive.Client.Pages
{
    public partial class ProjectBoard
    {
        [Parameter]
        public Guid ProjectId { get; set; }
        [Inject]
        public IState<ProjectState> ProjectState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public ITicketService TicketService { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        bool ProjectStateIsLoading => ProjectState.Value.IsLoading;
        List<TicketViewModel> Tickets => ProjectState.Value.ProjectTickets;
        ProjectViewModel Project => ProjectState.Value.Project;

        readonly List<Tuple<string, TicketStatus>> _columns = new()
        {
            new("New", TicketStatus.NotStarted),
            new("In Progress", TicketStatus.Active),
            new("Testing", TicketStatus.Testing),
            new("Completed", TicketStatus.Completed),
        };

        protected override void OnInitialized()
        {
            FetchProjectTickets();
            Dispatcher.Dispatch(new FetchProjectAction(ProjectId));
            base.OnInitialized();
        }

        void FetchProjectTickets() => Dispatcher.Dispatch(new FetchProjectTicketsAction(ProjectId));

        private async void OpenTicketDialog()
        {
            DialogParameters dialogParams = new()
            {
                { "Users", Project.Members },
                { "ProjectId", ProjectId }
            };
            var dialog = DialogService.Show<AddTicketDialog>("Add Ticket", dialogParams);
            var dialogResult = await dialog.Result;

            if (!dialogResult.Cancelled)
            {
                FetchProjectTickets();
            }
        }

        async void OpenEditTicketDialog(TicketViewModel ticket)
        {
            DialogParameters dialogParams = new()
            {
                { "Ticket", ticket },
                { "Members", Project.Members }
            };

            var dialog = DialogService.Show<EditTicketDialog>(null, dialogParams);
            var dialogResult = await dialog.Result;

            if (!dialogResult.Cancelled && dialogResult.Data.GetType() == typeof(bool))
            {
                if ((bool)dialogResult.Data)
                {
                    Snackbar.Add("Updated", Severity.Success);
                    FetchProjectTickets();
                }
            }
            else if (!dialogResult.Cancelled && dialogResult.Data.GetType() == typeof(string))
            {
                if ((string)dialogResult.Data == "Deleted")
                {
                    Snackbar.Add("Deleted", Severity.Success);
                    FetchProjectTickets();
                }
            }
        }
    }
}