using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Interfaces;

public interface IArmazemRepository
{
    Task<List<Armazem>> ObterTodosAsync();
    Task<Armazem?> ObterPorIdAsync(int id);
    Task<List<Armazem>> ObterPorUsuarioAsync(string usuarioId);
    Task<Armazem> CriarAsync(Armazem armazem);
    Task AtualizarAsync(Armazem armazem);
    Task RemoverAsync(int id);
    Task<bool> ExisteAsync(int id);
}
