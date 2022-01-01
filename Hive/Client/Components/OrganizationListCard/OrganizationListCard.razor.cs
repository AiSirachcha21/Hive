using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Fluxor;
using Hive.Client.Components.ParamConstants;
using Hive.Client.Components.RequireConfirmationDialog;
using Hive.Client.Services.Organizations;
using Hive.Client.Shared.Store.Organizations.Actions;
using Hive.Shared.Organizations.QueryViewModels;

namespace Hive.Client.Components.OrganizationListCard
{
    public partial class OrganizationListCard
    {
        [Parameter] public OrganizationViewModel Organization { get; set; }
        [Parameter] public bool DisableInteraction { get; set; } = false;
        [CascadingParameter] Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject] IDispatcher Dispatcher { get; set; }
        [Inject] IOrganizationService OrganizationService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        async void OnDelete(OrganizationViewModel organization)
        {
            string username = new((await AuthenticationState).User.Identity.Name.Where(s => !Char.IsWhiteSpace(s)).ToArray());
            DialogParameters dialogParams = new()
            { { RequireConfirmationDialogParams.ConfirmButtonText, "Delete" }, { RequireConfirmationDialogParams.DialogContent, $"You are about to delete {organization.Name}. This change is irreversable, are you sure you wish to continue ?" }, { RequireConfirmationDialogParams.IsDelete, true }, { RequireConfirmationDialogParams.ConfirmationPhrase, $"{username}/{organization.Name}" } };
            var dialog = DialogService.Show<RequireConfirmationDialog.RequireConfirmationDialog>("Are you sure?", dialogParams);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled && (bool)dialogResult.Data)
            {
                var result = await OrganizationService.DeleteOrganizatinoAsync(organization.Id);
                if (result)
                {
                    Snackbar.Add($"Successfully deleted {organization.Name} and its related projects", Severity.Success);
                    Dispatcher.Dispatch(new GetOrganizationsAction());
                }
            }
        }
    }
}