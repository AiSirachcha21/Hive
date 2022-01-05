using System;
using System.Collections.Generic;
using Fluxor;
using Hive.Client.Shared.Constants;
using Hive.Client.Shared.Store.Project;
using Hive.Shared.Projects.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Hive.Client.Pages
{
    public partial class ProjectPage
    {
        [Parameter]
        public Guid ProjectId { get; set; }
        [Inject]
        public IState<ProjectState> ProjectState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        ProjectViewModel Project => ProjectState.Value.Project;

        readonly List<BreadcrumbItem> _breadcrumbItems = new();

        protected override void OnInitialized()
        {
            Dispatcher.Dispatch(new FetchProjectAction(ProjectId));
            base.OnInitialized();
        }
    }
}