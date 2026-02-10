namespace MudExtended.Services.Loading;

/// <summary>
/// Service de gestion du loader global.
/// </summary>
public class LoaderService : IAsyncDisposable
{
    private int _loadingCount;
    private bool _isGlobalOverlayDisabled;
    private readonly object _lock = new();

    /// <summary>Indique si le chargement est en cours.</summary>
    public bool IsLoading => _loadingCount > 0;

    /// <summary>Indique si le loader global est d�sactiv�.</summary>
    public bool IsGlobalOverlayDisabled => _isGlobalOverlayDisabled;

    /// <summary>�v�nement d�clench� lors d'un changement d'�tat.</summary>
    public event EventHandler? OnChange;

    /// <summary>Affiche le loader.</summary>
    public void Show()
    {
        lock (_lock)
        {
            _loadingCount++;
            if (_loadingCount == 1)
            {
                NotifyStateChanged();
            }
        }
    }

    /// <summary>Masque le loader.</summary>
    public void Hide()
    {
        lock (_lock)
        {
            if (_loadingCount > 0)
            {
                _loadingCount--;
                if (_loadingCount == 0)
                {
                    NotifyStateChanged();
                }
            }
        }
    }

    /// <summary>R�initialise l'�tat du loader.</summary>
    public void Reset()
    {
        lock (_lock)
        {
            var wasLoading = _loadingCount > 0;
            _loadingCount = 0;
            if (wasLoading)
            {
                NotifyStateChanged();
            }
        }
    }

    /// <summary>D�sactive temporairement le loader global.</summary>
    public void DisableGlobalOverlay()
    {
        _isGlobalOverlayDisabled = true;
        NotifyStateChanged();
    }

    /// <summary>R�active le loader global.</summary>
    public void EnableGlobalOverlay()
    {
        _isGlobalOverlayDisabled = false;
        NotifyStateChanged();
    }

    /// <summary>
    /// Ex�cute une t�che avec affichage automatique du loader.
    /// </summary>
    /// <typeparam name="T">Type de retour.</typeparam>
    /// <param name="action">Action � ex�cuter.</param>
    /// <param name="cancellationToken">Token d'annulation.</param>
    /// <returns>R�sultat de l'action.</returns>
    public async Task<T> WithLoadingAsync<T>(
        Func<CancellationToken, Task<T>> action,
        CancellationToken cancellationToken = default)
    {
        Show();
        try
        {
            return await action(cancellationToken);
        }
        finally
        {
            Hide();
        }
    }

    /// <summary>
    /// Ex�cute une t�che avec affichage automatique du loader (sans retour).
    /// </summary>
    /// <param name="action">Action � ex�cuter.</param>
    /// <param name="cancellationToken">Token d'annulation.</param>
    public async Task WithLoadingAsync(
        Func<CancellationToken, Task> action,
        CancellationToken cancellationToken = default)
    {
        Show();
        try
        {
            await action(cancellationToken);
        }
        finally
        {
            Hide();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke(this, EventArgs.Empty);

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        Reset();
        return ValueTask.CompletedTask;
    }
}
