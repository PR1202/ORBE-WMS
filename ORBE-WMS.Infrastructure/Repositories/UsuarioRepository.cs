using Microsoft.EntityFrameworkCore;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;
using ORBE_WMS.Infrastructure.Persistence;

namespace ORBE_WMS.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public UsuarioRepository(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<ApplicationUser>> ObterTodosComArmazensAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Users
            .Include(u => u.Armazens)
                .ThenInclude(ua => ua.Armazem)
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<ApplicationUser?> ObterPorIdAsync(string id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<string?> ObterNomeAsync(string id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Users
            .Where(u => u.Id == id)
            .Select(u => u.Nome)
            .FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync()
    {
        using var db = _factory.CreateDbContext();
        return await db.Users.CountAsync();
    }
}
