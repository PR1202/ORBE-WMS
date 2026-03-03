using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Interfaces;

public interface IItemEstoqueRepository
{
    Task<List<ItemEstoque>> ObterPorArmazemEDepositanteAsync(int armazemId, int depositanteId);
    Task<List<ItemEstoque>> ObterPorArmazemAsync(int armazemId);
    Task<ItemEstoque?> ObterPorIdAsync(int id);
    Task<ItemEstoque> CriarAsync(ItemEstoque item);
    Task AtualizarAsync(ItemEstoque item);
    Task RemoverAsync(int id);
    Task<bool> ExisteAsync(int id);
}
