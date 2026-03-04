using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<List<ApplicationUser>> ObterTodosComArmazensAsync();
    Task<ApplicationUser?> ObterPorIdAsync(string id);
    Task<string?> ObterNomeAsync(string id);
    Task<int> CountAsync();
}
