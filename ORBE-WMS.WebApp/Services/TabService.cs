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
    /// Abre uma aba de formulário. Sempre cria uma nova aba (permite duplicatas).
    /// Retorna o Id da aba criada para que o formulário possa se auto-fechar.
    /// </summary>
    public Guid OpenFormTab(string title, string icon, Type componentType, Dictionary<string, object>? parameters = null)
    {
        foreach (var tab in _tabs)
            tab.IsActive = false;

        var newTab = new TabItem
        {
            Title = title,
            Icon = icon,
            ComponentType = componentType,
            IsActive = true,
            Parameters = parameters
        };

        _tabs.Add(newTab);

        // Injeta o TabId nos parâmetros para que o componente possa se fechar
        newTab.Parameters ??= new Dictionary<string, object>();
        newTab.Parameters["TabId"] = newTab.Id;

        OnTabsChanged?.Invoke();
        return newTab.Id;
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

    /// <summary>
    /// Fecha uma aba de formulário e ativa a aba de listagem correspondente.
    /// </summary>
    public void CloseFormAndActivateListing(Guid formTabId, Type listingComponentType)
    {
        // Fechar a aba do formulário
        var formTab = _tabs.FirstOrDefault(t => t.Id == formTabId);
        if (formTab is not null)
        {
            _tabs.Remove(formTab);
        }

        // Ativar a aba da listagem
        var listTab = _tabs.FirstOrDefault(t => t.ComponentType == listingComponentType);
        if (listTab is not null)
        {
            foreach (var tab in _tabs)
                tab.IsActive = false;
            listTab.IsActive = true;
        }
        else if (_tabs.Count > 0)
        {
            // Fallback: ativar a última aba
            foreach (var tab in _tabs)
                tab.IsActive = false;
            _tabs[^1].IsActive = true;
        }

        OnTabsChanged?.Invoke();
    }
}
