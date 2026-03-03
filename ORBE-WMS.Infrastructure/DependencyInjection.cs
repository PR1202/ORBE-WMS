using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Application.Services;
using ORBE_WMS.Infrastructure.Persistence;
using ORBE_WMS.Infrastructure.Repositories;

namespace ORBE_WMS.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registra os serviços de Infrastructure (repositórios) e Application (serviços de negócio)
    /// no container de DI.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // DbContext Factory (Blazor Server friendly)
        services.AddSingleton<IDbContextFactory<ApplicationDbContext>>(sp =>
            new ScopedDbContextFactory<ApplicationDbContext>(sp));

        // Repositórios
        services.AddScoped<IArmazemRepository, ArmazemRepository>();
        services.AddScoped<IDepositanteRepository, DepositanteRepository>();
        services.AddScoped<IItemEstoqueRepository, ItemEstoqueRepository>();
        services.AddScoped<IUsuarioArmazemRepository, UsuarioArmazemRepository>();

        // Application Services
        services.AddScoped<ArmazemAppService>();
        services.AddScoped<DepositanteAppService>();
        services.AddScoped<ItemEstoqueAppService>();

        return services;
    }
}
