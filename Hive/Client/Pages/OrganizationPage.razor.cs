using Fluxor;
using Hive.Client.Shared.Constants;
using Hive.Client.Shared.Store.OrganizationPage;
using Hive.Client.Shared.Store.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using Hive.Shared.Organizations.QueryViewModels;
using Hive.Shared.Projects.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hive.Client.Pages
{
    public partial class OrganizationPage
    {
        [Parameter]
        public Guid OrganizationId { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<OrganizationPageState> OrganizationPageState { get; set; }
        [Inject]
        public IState<OrganizationState> OrganizationState { get; set; }

        bool IsLoading => OrganizationPageState.Value.IsLoading;
        List<ProjectViewModel> Projects => OrganizationPageState.Value.Projects;
        OrganizationViewModel CurrentOrg => OrganizationState.Value.Organizations.Where(o => o.Id == OrganizationId).FirstOrDefault();
        List<BreadcrumbItem> _breadcrumbItems;

        protected override void OnInitialized()
        {
            _breadcrumbItems = new List<BreadcrumbItem>()
            {
                new BreadcrumbItem("Organizations","/organizations"),
                new BreadcrumbItem("Projects",Routes.OrganizationPage(OrganizationId), true),
            };
            if (!OrganizationState.Value.Organizations.Any())
            {
                Dispatcher.Dispatch(new GetOrganizationsAction());
            }
            Dispatcher.Dispatch(new FetchOrganizationPageAction(OrganizationId));
            base.OnInitialized();
        }
    }
}