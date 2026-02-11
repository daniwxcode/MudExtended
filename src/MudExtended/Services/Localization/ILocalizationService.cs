using MudExtended.Models.Localization;

namespace MudExtended.Services.Localization;

/// <summary>
/// Localization service for multilingual support.
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// Current language.
    /// </summary>
    SupportedLanguage CurrentLanguage { get; }

    /// <summary>
    /// Get localized string.
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <returns>Localized string</returns>
    string GetString(string key);

    /// <summary>
    /// Get localized string with formatting.
    /// </summary>
    /// <param name="key">Resource key</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted localized string</returns>
    string GetString(string key, params object[] args);

    /// <summary>
    /// Change current language.
    /// </summary>
    /// <param name="language">Target language</param>
    /// <returns>Async operation</returns>
    Task SetLanguageAsync(SupportedLanguage language);

    /// <summary>
    /// Language change event.
    /// </summary>
    event EventHandler<SupportedLanguage>? LanguageChanged;
}
