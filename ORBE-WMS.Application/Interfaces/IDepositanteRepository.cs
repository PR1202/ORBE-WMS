using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Interfaces;

public interface IDepositanteRepository
{
    Task<List<Depositante>> ObterPorArmazemAsync(int armazemId);
    Task<Depositante?> ObterPorIdAsync(int id);
    Task<Depositante> CriarAsync(Depositante depositante);
    Task AtualizarAsync(Depositante depositante);
    Task RemoverAsync(int id);
    Task<bool> ExisteAsync(int id);
}
