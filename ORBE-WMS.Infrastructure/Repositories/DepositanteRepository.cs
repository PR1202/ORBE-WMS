using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;
using ORBE_WMS.Infrastructure.Persistence;

namespace ORBE_WMS.Infrastructure.Repositories;

public class DepositanteRepository : IDepositanteRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public DepositanteRepository(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Depositante>> ObterPorArmazemAsync(int armazemId)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes
            .Include(d => d.Armazem)
            .Where(d => d.ArmazemId == armazemId)
            .OrderBy(d => d.Nome)
            .ToListAsync();
    }

    public async Task<Depositante?> ObterPorIdAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes
            .Include(d => d.Armazem)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Depositante> CriarAsync(Depositante depositante)
    {
        using var db = _factory.CreateDbContext();
        db.Depositantes.Add(depositante);
        await db.SaveChangesAsync();
        return depositante;
    }

    public async Task AtualizarAsync(Depositante depositante)
    {
        using var db = _factory.CreateDbContext();
        db.Depositantes.Update(depositante);
        await db.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        var dep = await db.Depositantes.FindAsync(id);
        if (dep is not null)
        {
            db.Depositantes.Remove(dep);
            await db.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes.AnyAsync(d => d.Id == id);
    }

    public async Task<List<Depositante>> ObterTodosComArmazemAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes
            .Include(d => d.Armazem)
            .OrderBy(d => d.Nome)
            .ToListAsync();
    }

    public async Task<List<Depositante>> ObterPorArmazensAsync(List<int> armazemIds)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes
            .Include(d => d.Armazem)
            .Where(d => armazemIds.Contains(d.ArmazemId))
            .OrderBy(d => d.Nome)
            .ToListAsync();
    }

    public async Task<int> CountAtivosAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes.CountAsync(d => d.Ativo);
    }

    public async Task<int> CountAtivosParaArmazensAsync(List<int> armazemIds)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes.CountAsync(d => d.Ativo && armazemIds.Contains(d.ArmazemId));
    }

    public async Task<int> CountInativosAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes.CountAsync(d => !d.Ativo);
    }

    public async Task<List<Depositante>> ObterAtivosPorArmazemAsync(int armazemId)
    {
        using var db = _factory.CreateDbContext();
        return await db.Depositantes
            .Where(d => d.ArmazemId == armazemId && d.Ativo)
            .OrderBy(d => d.Nome)
            .ToListAsync();
    }
}
