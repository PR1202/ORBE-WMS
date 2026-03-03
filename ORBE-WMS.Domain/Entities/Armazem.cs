using System.ComponentModel.DataAnnotations;

namespace ORBE_WMS.Domain.Entities;

public class Armazem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(18)]
    public string? CNPJ { get; set; }

    [MaxLength(500)]
    public string? Endereco { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Navegação
    public ICollection<Depositante> Depositantes { get; set; } = [];
    public ICollection<UsuarioArmazem> Usuarios { get; set; } = [];
    public ICollection<ItemEstoque> ItensEstoque { get; set; } = [];
}
