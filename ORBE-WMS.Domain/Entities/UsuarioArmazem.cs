using System.ComponentModel.DataAnnotations;

namespace ORBE_WMS.Domain.Entities;

/// <summary>
/// Tabela de junção entre ApplicationUser e Armazem.
/// Define qual papel o usuário possui em cada armazém.
/// </summary>
public class UsuarioArmazem
{
    public string UsuarioId { get; set; } = string.Empty;

    public int ArmazemId { get; set; }

    /// <summary>
    /// Papel do usuário neste armazém: "Gerente" ou "Operador".
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    // Navegação
    public ApplicationUser Usuario { get; set; } = null!;
    public Armazem Armazem { get; set; } = null!;
}
