using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ORBE_WMS.WebApp.Data;

public static class SeedData
{
    public const string AdminRoleName = "Admin";
    public const string AdminEmail = "admin@orbe.com";
    public const string AdminPassword = "Admin@123";
    public const string AdminNome = "Administrador";

    /// <summary>
    /// Garante que o role Admin e o usuário admin padrão existam no banco.
    /// Deve ser chamado no startup da aplicação.
    /// </summary>
    public static async Task InicializarAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Aplica migrations pendentes
        await context.Database.MigrateAsync();

        // Criar role Admin se não existir
        if (!await roleManager.RoleExistsAsync(AdminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
        }

        // Criar usuário admin se não existir
        var adminUser = await userManager.FindByEmailAsync(AdminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                Nome = AdminNome,
                Ativo = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, AdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Falha ao criar usuário admin: {errors}");
            }
        }
        else
        {
            // Garante que o admin existente tem o role Admin
            if (!await userManager.IsInRoleAsync(adminUser, AdminRoleName))
            {
                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            }
        }
    }
}
