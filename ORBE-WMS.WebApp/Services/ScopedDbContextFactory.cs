using Microsoft.EntityFrameworkCore;

namespace ORBE_WMS.WebApp.Services;

/// <summary>
/// Custom IDbContextFactory that creates a new DI scope for each DbContext,
/// allowing concurrent database access from multiple tab components.
/// Works with Aspire's AddSqlServerDbContext which handles connection configuration.
/// </summary>
public class ScopedDbContextFactory<TContext> : IDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly IServiceProvider _rootProvider;

    public ScopedDbContextFactory(IServiceProvider rootProvider)
    {
        _rootProvider = rootProvider;
    }

    public TContext CreateDbContext()
    {
        var scope = _rootProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<TContext>();
    }
}
