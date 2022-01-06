using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Hive.Client;
using Hive.Client.Shared;
using Hive.Client.Services;
using Hive.Shared.Registration;
using MudBlazor;
using Fluxor;
using Hive.Client.Shared.Constants;
using Hive.Shared.Projects.Commands;
using Hive.Client.Services.Organizations;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Client.Services.Projects;

namespace Hive.Client.Components.CreateProjectDialog
{
    public partial class CreateProjectDialog
    {
        [Parameter]
        public Guid OrganizationId { get; set; }
        [Parameter]
        public List<OrganizationEmployeeViewModel> Employees { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        [Inject]
        public IProjectService ProjectService { get; set; }
        CreateProjectRequestModel RequestContext { get; set; } = new();

        protected override void OnInitialized()
        {
            RequestContext.OrganizationId = OrganizationId;
            base.OnInitialized();
        }

        void OnCancel() => MudDialog.Cancel();

        async void OnSubmit()
        {
            if (IsSubmittable())
            {
                var result = await ProjectService.CreateProjectAsync(RequestContext);
                if (result)
                {
                    MudDialog.Close(DialogResult.Ok(true));
                }
            }
        }

        bool IsSubmittable()
        {
            return !string.IsNullOrEmpty(RequestContext.Name);
        }
    }
}