namespace ORBE_WMS.Application.DTOs;

public class ItemEstoqueDto
{
    public int Id { get; set; }
    public int ArmazemId { get; set; }
    public int DepositanteId { get; set; }
    public string CodigoProduto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public string UnidadeMedida { get; set; } = "UN";
    public string? Lote { get; set; }
    public DateTime? DataValidade { get; set; }
    public string? Localizacao { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public string DepositanteNome { get; set; } = string.Empty;
}

public class CriarItemEstoqueDto
{
    public int ArmazemId { get; set; }
    public int DepositanteId { get; set; }
    public string CodigoProduto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public string UnidadeMedida { get; set; } = "UN";
    public string? Lote { get; set; }
    public DateTime? DataValidade { get; set; }
    public string? Localizacao { get; set; }
}

public class AtualizarItemEstoqueDto
{
    public int Id { get; set; }
    public string CodigoProduto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public string UnidadeMedida { get; set; } = "UN";
    public string? Lote { get; set; }
    public DateTime? DataValidade { get; set; }
    public string? Localizacao { get; set; }
    public bool Ativo { get; set; }
}
