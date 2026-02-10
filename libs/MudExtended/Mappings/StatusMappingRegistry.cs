using System.Collections.Concurrent;

namespace MudExtended.Mappings;

/// <summary>
/// Registry centralis� pour les providers de mapping de statut.
/// </summary>
public class StatusMappingRegistry
{
    private readonly ConcurrentDictionary<Type, object> _providers = new();

    /// <summary>
    /// Enregistre un provider de mapping pour un type de statut.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <param name="provider">Provider de mapping.</param>
    public void Register<TStatus>(IStatusMappingProvider<TStatus> provider)
        where TStatus : Enum
    {
        ArgumentNullException.ThrowIfNull(provider);
        _providers[typeof(TStatus)] = provider;
    }

    /// <summary>
    /// Obtient le provider de mapping pour un type de statut.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <returns>Provider de mapping.</returns>
    /// <exception cref="InvalidOperationException">Si aucun provider n'est enregistr�.</exception>
    public IStatusMappingProvider<TStatus> Get<TStatus>()
        where TStatus : Enum
    {
        if (_providers.TryGetValue(typeof(TStatus), out var provider))
        {
            return (IStatusMappingProvider<TStatus>)provider;
        }

        throw new InvalidOperationException(
            $"Aucun provider de mapping enregistr� pour le type {typeof(TStatus).Name}. " +
            $"Utilisez StatusMappingRegistry.Register<{typeof(TStatus).Name}>() pour enregistrer un provider.");
    }

    /// <summary>
    /// Tente d'obtenir le provider de mapping pour un type de statut.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <param name="provider">Provider trouv� ou null.</param>
    /// <returns>True si trouv�.</returns>
    public bool TryGet<TStatus>(out IStatusMappingProvider<TStatus>? provider)
        where TStatus : Enum
    {
        if (_providers.TryGetValue(typeof(TStatus), out var obj))
        {
            provider = (IStatusMappingProvider<TStatus>)obj;
            return true;
        }

        provider = null;
        return false;
    }

    /// <summary>
    /// V�rifie si un provider est enregistr� pour un type.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <returns>True si enregistr�.</returns>
    public bool IsRegistered<TStatus>()
        where TStatus : Enum
    {
        return _providers.ContainsKey(typeof(TStatus));
    }

    /// <summary>
    /// Supprime le provider pour un type de statut.
    /// </summary>
    /// <typeparam name="TStatus">Type d'�num�ration du statut.</typeparam>
    /// <returns>True si supprim�.</returns>
    public bool Unregister<TStatus>()
        where TStatus : Enum
    {
        return _providers.TryRemove(typeof(TStatus), out _);
    }

    /// <summary>
    /// Supprime tous les providers enregistr�s.
    /// </summary>
    public void Clear()
    {
        _providers.Clear();
    }
}
