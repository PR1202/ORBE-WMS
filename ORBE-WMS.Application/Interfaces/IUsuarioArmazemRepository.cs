using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Interfaces;

public interface IUsuarioArmazemRepository
{
    Task<List<UsuarioArmazem>> ObterPorUsuarioAsync(string usuarioId);
    Task<List<UsuarioArmazem>> ObterPorArmazemAsync(int armazemId);
    Task AdicionarAsync(UsuarioArmazem usuarioArmazem);
    Task RemoverAsync(string usuarioId, int armazemId);
    Task RemoverTodosPorArmazemAsync(int armazemId);
    Task RemoverTodosPorUsuarioAsync(string usuarioId);
    Task<bool> ExisteAsync(string usuarioId, int armazemId);
    Task<List<int>> ObterArmazemIdsPorUsuarioAsync(string usuarioId);
    Task SincronizarVinculosAsync(string usuarioId, List<UsuarioArmazem> novosVinculos);
    Task<List<string>> ObterRolesPorUsuarioAsync(string usuarioId);
}
