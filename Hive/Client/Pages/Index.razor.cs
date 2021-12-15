using Hive.Client.Services;
using Hive.Client.Services.Common;
using Hive.Client.Shared.Constants;
using Hive.Client.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hive.Client.Pages
{
    public partial class Index
    {
        [Parameter]
        public string OrgName { get; set; }
        [Inject]
        public ActiveNavigationItem ActiveNavigationItem { get; set; }
        [Inject]
        public AuthStateProvider AuthProvider { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
