using System.ComponentModel.DataAnnotations;

namespace ORBE_WMS.WebApp.Data;

public class Depositante
{
    public int Id { get; set; }

    public int ArmazemId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(18)]
    public string? CNPJ { get; set; }

    [MaxLength(100)]
    public string? CodigoExterno { get; set; }

    [MaxLength(500)]
    public string? Endereco { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Navegação
    public Armazem Armazem { get; set; } = null!;
}
