using MudBlazor;
using MudExtended.Components.Dialogs;
using MudExtended.Enums;

namespace MudExtended.Extensions;

/// <summary>
/// Extensions for IDialogService.
/// </summary>
public static class DialogServiceExtensions
{
    #region Confirmation Dialogs

    /// <summary>
    /// Shows a confirmation dialog and returns the result.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Confirmation message.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="type">Confirmation type.</param>
    /// <param name="confirmText">Confirm button text.</param>
    /// <param name="cancelText">Cancel button text.</param>
    /// <returns>True if confirmed.</returns>
    public static async Task<bool> ConfirmAsync(
        this IDialogService dialogService,
        string message,
        string title = "Confirmation",
        ConfirmationType type = ConfirmationType.Info,
        string confirmText = "Confirm",
        string cancelText = "Cancel")
    {
        var parameters = new DialogParameters<ConfirmationDialog>
        {
            { x => x.Title, title },
            { x => x.Message, message },
            { x => x.Type, type },
            { x => x.ConfirmText, confirmText },
            { x => x.CancelText, cancelText }
        };

        var options = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = false,
            CloseOnEscapeKey = true,
            BackdropClick = false
        };

        var dialog = await dialogService.ShowAsync<ConfirmationDialog>(string.Empty, parameters, options);
        var result = await dialog.Result;

        return result is { Canceled: false };
    }

    /// <summary>
    /// Shows a delete confirmation dialog.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="itemName">Name of the item to delete.</param>
    /// <param name="customMessage">Optional custom message.</param>
    /// <returns>True if confirmed.</returns>
    public static Task<bool> ConfirmDeleteAsync(
        this IDialogService dialogService,
        string itemName,
        string? customMessage = null)
    {
        var message = customMessage ?? $"Are you sure you want to delete {itemName}? This action cannot be undone.";
        
        return dialogService.ConfirmAsync(
            message,
            "Confirm Deletion",
            ConfirmationType.Danger,
            "Delete",
            "Cancel");
    }

    /// <summary>
    /// Shows a danger confirmation dialog.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Warning message.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="confirmText">Confirm button text.</param>
    /// <returns>True if confirmed.</returns>
    public static Task<bool> ConfirmDangerAsync(
        this IDialogService dialogService,
        string message,
        string title = "Warning",
        string confirmText = "Continue")
    {
        return dialogService.ConfirmAsync(
            message,
            title,
            ConfirmationType.Danger,
            confirmText,
            "Cancel");
    }

    /// <summary>
    /// Shows a warning confirmation dialog.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Warning message.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="confirmText">Confirm button text.</param>
    /// <returns>True if confirmed.</returns>
    public static Task<bool> ConfirmWarningAsync(
        this IDialogService dialogService,
        string message,
        string title = "Warning",
        string confirmText = "Continue")
    {
        return dialogService.ConfirmAsync(
            message,
            title,
            ConfirmationType.Warning,
            confirmText,
            "Cancel");
    }

    #endregion

    #region Message Dialogs

    /// <summary>
    /// Shows a message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="severity">Message severity.</param>
    /// <param name="okText">OK button text.</param>
    public static async Task ShowMessageAsync(
        this IDialogService dialogService,
        string message,
        string title = "Message",
        MessageSeverity severity = MessageSeverity.Info,
        string okText = "OK")
    {
        var parameters = new DialogParameters<MessageDialog>
        {
            { x => x.Title, title },
            { x => x.Message, message },
            { x => x.Severity, severity },
            { x => x.OkText, okText },
            { x => x.ShowCancelButton, false }
        };

        var options = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = false,
            CloseOnEscapeKey = true,
            BackdropClick = false
        };

        var dialog = await dialogService.ShowAsync<MessageDialog>(string.Empty, parameters, options);
        await dialog.Result;
    }

    /// <summary>
    /// Shows an information message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    public static Task ShowInfoAsync(
        this IDialogService dialogService,
        string message,
        string title = "Information")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Info);
    }

    /// <summary>
    /// Shows a success message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    public static Task ShowSuccessAsync(
        this IDialogService dialogService,
        string message,
        string title = "Success")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Success);
    }

    /// <summary>
    /// Shows a warning message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    public static Task ShowWarningAsync(
        this IDialogService dialogService,
        string message,
        string title = "Warning")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Warning);
    }

    /// <summary>
    /// Shows an error message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    public static Task ShowErrorAsync(
        this IDialogService dialogService,
        string message,
        string title = "Error")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Error);
    }

    /// <summary>
    /// Shows an error message box with details.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Main message.</param>
    /// <param name="details">Error details.</param>
    /// <param name="title">Dialog title.</param>
    public static async Task ShowErrorWithDetailsAsync(
        this IDialogService dialogService,
        string message,
        string details,
        string title = "Error")
    {
        var parameters = new DialogParameters<MessageDialog>
        {
            { x => x.Title, title },
            { x => x.Message, message },
            { x => x.SecondaryMessage, details },
            { x => x.Severity, MessageSeverity.Error },
            { x => x.OkText, "OK" },
            { x => x.ShowCancelButton, false }
        };

        var options = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = false,
            CloseOnEscapeKey = true,
            BackdropClick = false
        };

        var dialog = await dialogService.ShowAsync<MessageDialog>(string.Empty, parameters, options);
        await dialog.Result;
    }

    /// <summary>
    /// Shows a Yes/No message box.
    /// </summary>
    /// <param name="dialogService">Dialog service.</param>
    /// <param name="message">Message to display.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="severity">Message severity.</param>
    /// <returns>True if Yes, False if No.</returns>
    public static async Task<bool> ShowYesNoAsync(
        this IDialogService dialogService,
        string message,
        string title = "Question",
        MessageSeverity severity = MessageSeverity.Info)
    {
        var parameters = new DialogParameters<MessageDialog>
        {
            { x => x.Title, title },
            { x => x.Message, message },
            { x => x.Severity, severity },
            { x => x.OkText, "Yes" },
            { x => x.CancelText, "No" },
            { x => x.ShowCancelButton, true }
        };

        var options = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = false,
            CloseOnEscapeKey = true,
            BackdropClick = false
        };

        var dialog = await dialogService.ShowAsync<MessageDialog>(string.Empty, parameters, options);
        var result = await dialog.Result;

        return result is { Canceled: false };
    }

    #endregion
}
