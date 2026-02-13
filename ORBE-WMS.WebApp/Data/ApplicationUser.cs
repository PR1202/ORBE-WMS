using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ORBE_WMS.WebApp.Data;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    // Navegação: armazéns aos quais o usuário tem acesso
    public ICollection<UsuarioArmazem> Armazens { get; set; } = [];
}

