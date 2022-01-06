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
using Hive.Shared.Projects.Queries;
using Hive.Shared.Users;
using Hive.Client.Services.Organizations;
using Hive.Domain;
using Hive.Client.Shared.Store.OrganizationSettings;
using Hive.Client.Services.Projects;
using Hive.Client.Shared.Store.Project;

namespace Hive.Client.Components.ProjectTeamList
{
    public partial class ProjectTeamListCard
    {
        [Parameter]
        public List<ProjectUserViewModel> Users { get; set; }
        [CascadingParameter]
        public Guid OrganizationId { get; set; }
        [CascadingParameter]
        public Guid ProjectId { get; set; }
        [Inject] IOrganizationService OrganizationService { get; set; }
        [Inject] IProjectService ProjectService { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }

        UserViewModel _searchedUser;

        async Task<IEnumerable<UserViewModel>> GetNetworkUsers(string searchValue)
        {
            var data = await OrganizationService.GetUserPool(OrganizationId);
            return data.Where(d => d.FirstName.Contains(searchValue) || d.LastName.Contains(searchValue)).ToArray();
        }

        async Task AddUserToProject()
        {
            var data = await ProjectService.AddUserToProjectAsync(new List<string>() { _searchedUser.Id }, ProjectId);
            if (data)
            {
                Dispatcher.Dispatch(new FetchProjectAction(ProjectId));
                _searchedUser = null;
            }
        }
    }
}