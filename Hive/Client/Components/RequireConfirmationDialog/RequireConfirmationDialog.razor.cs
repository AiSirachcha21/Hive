using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace Hive.Client.Components.RequireConfirmationDialog
{
    public partial class RequireConfirmationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Action Submit { get; set; }
        [Parameter] public string DialogContent { get; set; }
        [Parameter] public string ConfirmButtonText { get; set; }
        [Parameter] public bool IsDelete { get; set; } = false;
        [Parameter] public string ConfirmationPhrase { get; set; } = null;

        string _confirmationTextFieldValue;
        bool _isSubmitDisabled = true;

        void OnSubmit() => MudDialog.Close(DialogResult.Ok<bool>(true));
        void OnCancel() => MudDialog.Cancel();
    }
}