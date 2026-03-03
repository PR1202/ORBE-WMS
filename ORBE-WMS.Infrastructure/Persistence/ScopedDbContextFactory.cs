using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ORBE_WMS.Infrastructure.Persistence;

/// <summary>
/// IDbContextFactory implementation that creates a new scope per context instance.
/// Needed for Blazor Server where the default scope is tied to the circuit lifetime.
/// </summary>
public class ScopedDbContextFactory<TContext> : IDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;

    public ScopedDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TContext CreateDbContext()
    {
        var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<TContext>();
    }
}
