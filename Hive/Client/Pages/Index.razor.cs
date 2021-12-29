using Fluxor;
using Hive.Client.Services;
using Hive.Client.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Hive.Client.Pages
{
    public partial class Index
    {
        [Parameter] public string OrgName { get; set; }
        [Inject] public ActiveNavigationItem ActiveNavigationItem { get; set; }
        [Inject] public AuthStateProvider AuthProvider { get; set; }
        [Inject] public NavigationManager Navigator { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
