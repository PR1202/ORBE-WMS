namespace ORBE_WMS.WebApp.Services;

/// <summary>
/// Serviço scoped que mantém o armazém e depositante ativos na sessão do usuário.
/// Também gerencia o estado das abas por contexto (armazém + depositante).
/// </summary>
public class TenantService
{
    // ── Contexto ativo ──────────────────────────────────────────

    public int? ArmazemIdAtivo { get; private set; }
    public string? ArmazemNome { get; private set; }
    public int? DepositanteIdAtivo { get; private set; }
    public string? DepositanteNome { get; private set; }

    /// <summary>
    /// Chave do contexto atual no formato "ArmazemId:DepositanteId".
    /// Retorna null quando nenhum contexto está definido.
    /// </summary>
    public string? ContextoKey =>
        ArmazemIdAtivo.HasValue && DepositanteIdAtivo.HasValue
            ? $"{ArmazemIdAtivo}:{DepositanteIdAtivo}"
            : null;

    // ── Estado de abas por contexto ─────────────────────────────

    /// <summary>
    /// Dicionário que mantém as abas salvas para cada contexto (armazém:depositante).
    /// Chave: "ArmazemId:DepositanteId", Valor: lista de abas serializadas.
    /// </summary>
    private readonly Dictionary<string, List<SavedTabState>> _tabStateByContext = new();

    // ── Eventos ─────────────────────────────────────────────────

    /// <summary>
    /// Disparado quando o contexto (armazém + depositante) é alterado.
    /// </summary>
    public event Action? OnContextChanged;

    // ── Métodos de contexto ─────────────────────────────────────

    /// <summary>
    /// Define o armazém e depositante ativos para a sessão atual.
    /// Dispara OnContextChanged para que os componentes recarreguem.
    /// </summary>
    public void DefinirContexto(int armazemId, string armazemNome, int depositanteId, string depositanteNome)
    {
        ArmazemIdAtivo = armazemId;
        ArmazemNome = armazemNome;
        DepositanteIdAtivo = depositanteId;
        DepositanteNome = depositanteNome;

        OnContextChanged?.Invoke();
    }

    /// <summary>
    /// Limpa o contexto ativo (volta ao estado sem seleção).
    /// </summary>
    public void LimparContexto()
    {
        ArmazemIdAtivo = null;
        ArmazemNome = null;
        DepositanteIdAtivo = null;
        DepositanteNome = null;

        OnContextChanged?.Invoke();
    }

    // ── Métodos de abas por contexto ────────────────────────────

    /// <summary>
    /// Salva o estado atual das abas para o contexto informado.
    /// </summary>
    public void SalvarAbasDoContexto(string contextoKey, List<SavedTabState> tabs)
    {
        _tabStateByContext[contextoKey] = tabs;
    }

    /// <summary>
    /// Restaura as abas gravadas para o contexto informado.
    /// Retorna null se não houver histórico salvo.
    /// </summary>
    public List<SavedTabState>? RestaurarAbasDoContexto(string contextoKey)
    {
        return _tabStateByContext.TryGetValue(contextoKey, out var tabs) ? tabs : null;
    }

    // ── DTO para estado de aba ──────────────────────────────────

    public class SavedTabState
    {
        public string Key { get; set; } = string.Empty;
        public bool Pinned { get; set; }
        public bool Active { get; set; }
    }
}
