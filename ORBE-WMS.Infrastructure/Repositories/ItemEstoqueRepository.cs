using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;
using ORBE_WMS.Infrastructure.Persistence;

namespace ORBE_WMS.Infrastructure.Repositories;

public class ItemEstoqueRepository : IItemEstoqueRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public ItemEstoqueRepository(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<ItemEstoque>> ObterPorArmazemEDepositanteAsync(int armazemId, int depositanteId)
    {
        using var db = _factory.CreateDbContext();
        return await db.ItensEstoque
            .Include(i => i.Depositante)
            .Where(i => i.ArmazemId == armazemId && i.DepositanteId == depositanteId)
            .OrderBy(i => i.CodigoProduto)
            .ToListAsync();
    }

    public async Task<List<ItemEstoque>> ObterPorArmazemAsync(int armazemId)
    {
        using var db = _factory.CreateDbContext();
        return await db.ItensEstoque
            .Include(i => i.Depositante)
            .Where(i => i.ArmazemId == armazemId)
            .OrderBy(i => i.CodigoProduto)
            .ToListAsync();
    }

    public async Task<ItemEstoque?> ObterPorIdAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.ItensEstoque
            .Include(i => i.Depositante)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<ItemEstoque> CriarAsync(ItemEstoque item)
    {
        using var db = _factory.CreateDbContext();
        db.ItensEstoque.Add(item);
        await db.SaveChangesAsync();
        return item;
    }

    public async Task AtualizarAsync(ItemEstoque item)
    {
        using var db = _factory.CreateDbContext();
        db.ItensEstoque.Update(item);
        await db.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        var item = await db.ItensEstoque.FindAsync(id);
        if (item is not null)
        {
            db.ItensEstoque.Remove(item);
            await db.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.ItensEstoque.AnyAsync(i => i.Id == id);
    }
}
