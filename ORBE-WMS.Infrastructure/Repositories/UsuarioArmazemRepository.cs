using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;
using ORBE_WMS.Infrastructure.Persistence;

namespace ORBE_WMS.Infrastructure.Repositories;

public class UsuarioArmazemRepository : IUsuarioArmazemRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public UsuarioArmazemRepository(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<UsuarioArmazem>> ObterPorUsuarioAsync(string usuarioId)
    {
        using var db = _factory.CreateDbContext();
        return await db.UsuarioArmazens
            .Include(ua => ua.Armazem)
            .Where(ua => ua.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<List<UsuarioArmazem>> ObterPorArmazemAsync(int armazemId)
    {
        using var db = _factory.CreateDbContext();
        return await db.UsuarioArmazens
            .Include(ua => ua.Usuario)
            .Where(ua => ua.ArmazemId == armazemId)
            .ToListAsync();
    }

    public async Task AdicionarAsync(UsuarioArmazem usuarioArmazem)
    {
        using var db = _factory.CreateDbContext();
        db.UsuarioArmazens.Add(usuarioArmazem);
        await db.SaveChangesAsync();
    }

    public async Task RemoverAsync(string usuarioId, int armazemId)
    {
        using var db = _factory.CreateDbContext();
        var ua = await db.UsuarioArmazens.FindAsync(usuarioId, armazemId);
        if (ua is not null)
        {
            db.UsuarioArmazens.Remove(ua);
            await db.SaveChangesAsync();
        }
    }

    public async Task RemoverTodosPorArmazemAsync(int armazemId)
    {
        using var db = _factory.CreateDbContext();
        var items = await db.UsuarioArmazens
            .Where(ua => ua.ArmazemId == armazemId)
            .ToListAsync();
        db.UsuarioArmazens.RemoveRange(items);
        await db.SaveChangesAsync();
    }

    public async Task RemoverTodosPorUsuarioAsync(string usuarioId)
    {
        using var db = _factory.CreateDbContext();
        var items = await db.UsuarioArmazens
            .Where(ua => ua.UsuarioId == usuarioId)
            .ToListAsync();
        db.UsuarioArmazens.RemoveRange(items);
        await db.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(string usuarioId, int armazemId)
    {
        using var db = _factory.CreateDbContext();
        return await db.UsuarioArmazens
            .AnyAsync(ua => ua.UsuarioId == usuarioId && ua.ArmazemId == armazemId);
    }
}
