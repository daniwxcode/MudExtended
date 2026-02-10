using MudBlazor;
using MudExtended.Components.Dialogs;
using MudExtended.Enums;

namespace MudExtended.Extensions;

/// <summary>
/// Extensions pour IDialogService.
/// </summary>
public static class DialogServiceExtensions
{
    #region Confirmation Dialogs

    /// <summary>
    /// Affiche un dialog de confirmation et retourne le r�sultat.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message de confirmation.</param>
    /// <param name="title">Titre du dialog.</param>
    /// <param name="type">Type de confirmation.</param>
    /// <param name="confirmText">Texte du bouton de confirmation.</param>
    /// <param name="cancelText">Texte du bouton d'annulation.</param>
    /// <returns>True si confirm�.</returns>
    public static async Task<bool> ConfirmAsync(
        this IDialogService dialogService,
        string message,
        string title = "Confirmation",
        ConfirmationType type = ConfirmationType.Info,
        string confirmText = "Confirmer",
        string cancelText = "Annuler")
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
    /// Affiche un dialog de confirmation de suppression.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="itemName">Nom de l'�l�ment � supprimer.</param>
    /// <param name="customMessage">Message personnalis� optionnel.</param>
    /// <returns>True si confirm�.</returns>
    public static Task<bool> ConfirmDeleteAsync(
        this IDialogService dialogService,
        string itemName,
        string? customMessage = null)
    {
        var message = customMessage ?? $"Voulez-vous vraiment supprimer {itemName} ? Cette action est irr�versible.";
        
        return dialogService.ConfirmAsync(
            message,
            "Confirmer la suppression",
            ConfirmationType.Danger,
            "Supprimer",
            "Annuler");
    }

    /// <summary>
    /// Affiche un dialog de confirmation d'action dangereuse.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message d'avertissement.</param>
    /// <param name="title">Titre du dialog.</param>
    /// <param name="confirmText">Texte du bouton de confirmation.</param>
    /// <returns>True si confirm�.</returns>
    public static Task<bool> ConfirmDangerAsync(
        this IDialogService dialogService,
        string message,
        string title = "Attention",
        string confirmText = "Continuer")
    {
        return dialogService.ConfirmAsync(
            message,
            title,
            ConfirmationType.Danger,
            confirmText,
            "Annuler");
    }

    /// <summary>
    /// Affiche un dialog d'avertissement avec confirmation.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message d'avertissement.</param>
    /// <param name="title">Titre du dialog.</param>
    /// <param name="confirmText">Texte du bouton de confirmation.</param>
    /// <returns>True si confirm�.</returns>
    public static Task<bool> ConfirmWarningAsync(
        this IDialogService dialogService,
        string message,
        string title = "Avertissement",
        string confirmText = "Continuer")
    {
        return dialogService.ConfirmAsync(
            message,
            title,
            ConfirmationType.Warning,
            confirmText,
            "Annuler");
    }

    #endregion

    #region Message Dialogs

    /// <summary>
    /// Affiche une bo�te de message.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    /// <param name="severity">S�v�rit� du message.</param>
    /// <param name="okText">Texte du bouton OK.</param>
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
    /// Affiche une bo�te de message d'information.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    public static Task ShowInfoAsync(
        this IDialogService dialogService,
        string message,
        string title = "Information")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Info);
    }

    /// <summary>
    /// Affiche une bo�te de message de succ�s.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    public static Task ShowSuccessAsync(
        this IDialogService dialogService,
        string message,
        string title = "Succ�s")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Success);
    }

    /// <summary>
    /// Affiche une bo�te de message d'avertissement.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    public static Task ShowWarningAsync(
        this IDialogService dialogService,
        string message,
        string title = "Avertissement")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Warning);
    }

    /// <summary>
    /// Affiche une bo�te de message d'erreur.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    public static Task ShowErrorAsync(
        this IDialogService dialogService,
        string message,
        string title = "Erreur")
    {
        return dialogService.ShowMessageAsync(message, title, MessageSeverity.Error);
    }

    /// <summary>
    /// Affiche une bo�te de message d'erreur avec d�tails.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message principal.</param>
    /// <param name="details">D�tails de l'erreur.</param>
    /// <param name="title">Titre du dialog.</param>
    public static async Task ShowErrorWithDetailsAsync(
        this IDialogService dialogService,
        string message,
        string details,
        string title = "Erreur")
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
    /// Affiche une bo�te de message avec choix Oui/Non.
    /// </summary>
    /// <param name="dialogService">Service de dialog.</param>
    /// <param name="message">Message � afficher.</param>
    /// <param name="title">Titre du dialog.</param>
    /// <param name="severity">S�v�rit� du message.</param>
    /// <returns>True si Oui, False si Non.</returns>
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
            { x => x.OkText, "Oui" },
            { x => x.CancelText, "Non" },
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
