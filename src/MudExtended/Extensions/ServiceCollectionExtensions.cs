using Microsoft.Extensions.DependencyInjection;
using MudExtended.Mappings;
using MudExtended.Models.Configuration;
using MudExtended.Services.Api;
using MudExtended.Services.Audit;
using MudExtended.Services.Export;
using MudExtended.Services.Loading;
using MudExtended.Services.Localization;
using MudExtended.Services.Permissions;

namespace MudExtended.Extensions;

/// <summary>
/// Extensions pour la configuration des services MudExtended.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Ajoute tous les services MudExtended au conteneur de dépendances.
    /// </summary>
    /// <param name="services">Collection de services.</param>
    /// <param name="configure">Action de configuration optionnelle.</param>
    /// <returns>Collection de services pour chaînage.</returns>
    public static IServiceCollection AddMudExtended(
        this IServiceCollection services,
        Action<MudExtendedOptions>? configure = null)
    {
        var options = new MudExtendedOptions();
        configure?.Invoke(options);

        // Options de configuration
        services.AddSingleton(options);

        // Localisation multilingue (EN, FR, ES)
        services.AddScoped<ILocalizationService, LocalizationService>();

        // Services métier
        services.AddScoped<IApiExecutor, ApiExecutor>();
        services.AddScoped<LoaderService>();

        // Export de données (CSV, JSON, XML, Excel, PDF)
        services.AddScoped<IDataExportService, DataExportService>();

        // Journalisation d'audit
        if (options.EnableAuditLogging)
        {
            services.AddScoped<IAuditService, InMemoryAuditService>();
        }

        // Service de permissions (nécessite AuthenticationStateProvider et IAuthorizationService)
        if (options.UsePermissionService)
        {
            services.AddScoped<IPermissionService, PermissionService>();
        }

        // Registry des mappings
        services.AddSingleton<StatusMappingRegistry>();

        return services;
    }

    /// <summary>
    /// Enregistre un provider de mapping de statut.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <typeparam name="TProvider">Type du provider.</typeparam>
    /// <param name="services">Collection de services.</param>
    /// <returns>Collection de services pour cha�nage.</returns>
    public static IServiceCollection AddStatusMappingProvider<TStatus, TProvider>(
        this IServiceCollection services)
        where TStatus : Enum
        where TProvider : class, IStatusMappingProvider<TStatus>
    {
        services.AddSingleton<IStatusMappingProvider<TStatus>, TProvider>();
        
        // Enregistrer dans le registry au d�marrage
        services.AddSingleton<IStatusMappingProviderRegistration>(sp =>
            new StatusMappingProviderRegistration<TStatus>(
                sp.GetRequiredService<StatusMappingRegistry>(),
                sp.GetRequiredService<IStatusMappingProvider<TStatus>>()));

        return services;
    }

    /// <summary>
    /// Enregistre un provider de mapping de statut avec une instance.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <param name="services">Collection de services.</param>
    /// <param name="provider">Instance du provider.</param>
    /// <returns>Collection de services pour cha�nage.</returns>
    public static IServiceCollection AddStatusMappingProvider<TStatus>(
        this IServiceCollection services,
        IStatusMappingProvider<TStatus> provider)
        where TStatus : Enum
    {
        services.AddSingleton(provider);
        
        services.AddSingleton<IStatusMappingProviderRegistration>(sp =>
            new StatusMappingProviderRegistration<TStatus>(
                sp.GetRequiredService<StatusMappingRegistry>(),
                provider));

        return services;
    }
}

/// <summary>
/// Interface marqueur pour l'enregistrement des providers.
/// </summary>
public interface IStatusMappingProviderRegistration
{
    void Register();
}

/// <summary>
/// Enregistrement d'un provider de mapping.
/// </summary>
internal sealed class StatusMappingProviderRegistration<TStatus> : IStatusMappingProviderRegistration
    where TStatus : Enum
{
    private readonly StatusMappingRegistry _registry;
    private readonly IStatusMappingProvider<TStatus> _provider;
    private bool _registered;

    public StatusMappingProviderRegistration(
        StatusMappingRegistry registry,
        IStatusMappingProvider<TStatus> provider)
    {
        _registry = registry;
        _provider = provider;
        Register();
    }

    public void Register()
    {
        if (!_registered)
        {
            _registry.Register(_provider);
            _registered = true;
        }
    }
}
