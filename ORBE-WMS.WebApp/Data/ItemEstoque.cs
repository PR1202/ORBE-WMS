using System.ComponentModel.DataAnnotations;

namespace ORBE_WMS.WebApp.Data;

public class ItemEstoque
{
    public int Id { get; set; }

    public int ArmazemId { get; set; }

    public int DepositanteId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CodigoProduto { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Descricao { get; set; } = string.Empty;

    public decimal Quantidade { get; set; }

    [Required]
    [MaxLength(20)]
    public string UnidadeMedida { get; set; } = "UN";

    [MaxLength(100)]
    public string? Lote { get; set; }

    public DateTime? DataValidade { get; set; }

    [MaxLength(100)]
    public string? Localizacao { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Navegação
    public Armazem Armazem { get; set; } = null!;
    public Depositante Depositante { get; set; } = null!;
}
