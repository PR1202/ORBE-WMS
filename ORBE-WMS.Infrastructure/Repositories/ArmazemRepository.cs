using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;
using ORBE_WMS.Infrastructure.Persistence;

namespace ORBE_WMS.Infrastructure.Repositories;

public class ArmazemRepository : IArmazemRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public ArmazemRepository(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Armazem>> ObterTodosAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Armazens
            .Include(a => a.Depositantes)
            .Include(a => a.Usuarios)
            .OrderBy(a => a.Nome)
            .ToListAsync();
    }

    public async Task<Armazem?> ObterPorIdAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Armazens
            .Include(a => a.Depositantes)
            .Include(a => a.Usuarios)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Armazem>> ObterPorUsuarioAsync(string usuarioId)
    {
        using var db = _factory.CreateDbContext();
        return await db.UsuarioArmazens
            .Where(ua => ua.UsuarioId == usuarioId)
            .Include(ua => ua.Armazem)
                .ThenInclude(a => a.Depositantes)
            .Include(ua => ua.Armazem)
                .ThenInclude(a => a.Usuarios)
            .Select(ua => ua.Armazem)
            .OrderBy(a => a.Nome)
            .ToListAsync();
    }

    public async Task<Armazem> CriarAsync(Armazem armazem)
    {
        using var db = _factory.CreateDbContext();
        db.Armazens.Add(armazem);
        await db.SaveChangesAsync();
        return armazem;
    }

    public async Task AtualizarAsync(Armazem armazem)
    {
        using var db = _factory.CreateDbContext();
        db.Armazens.Update(armazem);
        await db.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        var armazem = await db.Armazens.FindAsync(id);
        if (armazem is not null)
        {
            db.Armazens.Remove(armazem);
            await db.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Armazens.AnyAsync(a => a.Id == id);
    }
}
