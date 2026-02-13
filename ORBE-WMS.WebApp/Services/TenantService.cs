namespace ORBE_WMS.WebApp.Services;

/// <summary>
/// Serviço scoped que mantém o armazém ativo na sessão do usuário.
/// Utilizado para filtrar dados por tenant em toda a aplicação.
/// </summary>
public class TenantService
{
    /// <summary>
    /// Id do armazém ativo selecionado pelo usuário.
    /// Null quando nenhum armazém foi selecionado (ex: Admin sem contexto de armazém).
    /// </summary>
    public int? ArmazemIdAtivo { get; private set; }

    /// <summary>
    /// Nome do armazém ativo para exibição na UI.
    /// </summary>
    public string? ArmazemNome { get; private set; }

    /// <summary>
    /// Define o armazém ativo para a sessão atual.
    /// </summary>
    public void DefinirArmazem(int armazemId, string nome)
    {
        ArmazemIdAtivo = armazemId;
        ArmazemNome = nome;
    }

    /// <summary>
    /// Limpa o armazém ativo (volta ao contexto global).
    /// </summary>
    public void LimparArmazem()
    {
        ArmazemIdAtivo = null;
        ArmazemNome = null;
    }
}
