using Fluxor;
using Hive.Client.Components.AddTicketDialog;
using Hive.Client.Services.Tickets;
using Hive.Client.Shared.Store.Project;
using Hive.Domain;
using Hive.Shared.Projects.Queries;
using Hive.Shared.Tickets.Commands;
using Hive.Shared.Tickets.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        List<ProjectUserViewModel> ProjectMembers => Project.Members;

        List<Tuple<string, TicketStatus>> Columns = new()
        {
            new("New", TicketStatus.NotStarted),
            new("In Progress", TicketStatus.Active),
            new("Testing", TicketStatus.Testing),
            new("Completed", TicketStatus.Completed),
        };

        TicketStatus SelectedStatus { get; set; }

        protected override void OnInitialized()
        {
            FetchProjectTickets();
            Dispatcher.Dispatch(new FetchProjectAction(ProjectId));
            base.OnInitialized();
        }

        void FetchProjectTickets()
        {
            Dispatcher.Dispatch(new FetchProjectTicketsAction(ProjectId));

        }

        private async void OpenTicketDialog()
        {
            DialogParameters dialogParams = new()
            {
                { "Users", Project.Members },
                { "ProjectId", ProjectId }
            };
            var dialog = DialogService.Show<AddTicketDialog>(null, dialogParams);
            var dialogResult = await dialog.Result;

            if (!dialogResult.Cancelled)
            {
                FetchProjectTickets();
            }
        }
    }
}