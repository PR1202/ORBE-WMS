namespace ORBE_WMS.WebApp.Services;

public class TabService
{
    private readonly List<TabItem> _tabs = [];

    public IReadOnlyList<TabItem> Tabs => _tabs.AsReadOnly();
    public TabItem? ActiveTab => _tabs.FirstOrDefault(t => t.IsActive);

    public event Action? OnTabsChanged;

    /// <summary>
    /// Remove todas as abas (usado para restaurar do sessionStorage).
    /// </summary>
    public void ClearAll()
    {
        _tabs.Clear();
    }

    /// <summary>
    /// Abre uma nova aba ou reativa uma já existente do mesmo tipo.
    /// </summary>
    public void OpenTab(string title, string icon, Type componentType)
    {
        var existing = _tabs.FirstOrDefault(t => t.ComponentType == componentType);
        if (existing is not null)
        {
            ActivateTab(existing.Id);
            return;
        }

        // Desativa todas
        foreach (var tab in _tabs)
            tab.IsActive = false;

        var newTab = new TabItem
        {
            Title = title,
            Icon = icon,
            ComponentType = componentType,
            IsActive = true
        };

        _tabs.Add(newTab);
        OnTabsChanged?.Invoke();
    }

    /// <summary>
    /// Abre a aba Dashboard como fixa (pinned). Chamado na inicialização.
    /// </summary>
    public void OpenPinnedTab(string title, string icon, Type componentType)
    {
        var existing = _tabs.FirstOrDefault(t => t.ComponentType == componentType);
        if (existing is not null)
        {
            ActivateTab(existing.Id);
            return;
        }

        foreach (var tab in _tabs)
            tab.IsActive = false;

        var pinned = new TabItem
        {
            Title = title,
            Icon = icon,
            ComponentType = componentType,
            IsActive = true,
            IsPinned = true
        };

        _tabs.Insert(0, pinned);
        OnTabsChanged?.Invoke();
    }

    /// <summary>
    /// Ativa (exibe) uma aba pelo Id.
    /// </summary>
    public void ActivateTab(Guid tabId)
    {
        foreach (var tab in _tabs)
            tab.IsActive = tab.Id == tabId;

        OnTabsChanged?.Invoke();
    }

    /// <summary>
    /// Fecha uma aba. Abas pinned não podem ser fechadas.
    /// Ao fechar, ativa a aba anterior (ou a primeira disponível).
    /// </summary>
    public void CloseTab(Guid tabId)
    {
        var tab = _tabs.FirstOrDefault(t => t.Id == tabId);
        if (tab is null || tab.IsPinned) return;

        var index = _tabs.IndexOf(tab);
        var wasActive = tab.IsActive;

        _tabs.Remove(tab);

        if (wasActive && _tabs.Count > 0)
        {
            // Ativa a aba anterior, ou a primeira se não houver anterior
            var newIndex = Math.Min(index, _tabs.Count - 1);
            _tabs[newIndex].IsActive = true;
        }

        OnTabsChanged?.Invoke();
    }
}
